using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using NHibernate.Util;
using NHibernateClient.Impl;
using NHQueryOver = NHibernate.Criterion.QueryOver;
using ClientQueryOver = NHibernateClient.Criterion.QueryOver;
using CriteriaImpl = NHibernate.Impl.CriteriaImpl;

namespace NHibernateClient.Conversion
{
    //TODO: this class used to have static factory fields however the conversion would break for second and subsequent converts
    // changing to instance properties seesm to fix it. Investigate and cache properly. possibly due to ConcurrentDictionary?

    public class Converter
    {
        private const BindingFlags ReflectionFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        private readonly ConcurrentDictionary<Type, Type> _typeMap = new ConcurrentDictionary<Type, Type>();

        private readonly ConcurrentDictionary<Type, Func<object, object>> _factories =
            new ConcurrentDictionary<Type, Func<object, object>>();

        private readonly ConcurrentDictionary<Type, Action<object, object>> _specialMappings =
            new ConcurrentDictionary<Type, Action<object, object>>();

        public Converter()
        {
            _factories.TryAdd(typeof(TypeWrapper), a => ((TypeWrapper)a).Type);
            _factories.TryAdd(typeof(string), a => a is string ? a.ToString() : string.Empty);
            _specialMappings.TryAdd(typeof(CriteriaImpl), (a, b) =>
            {
                var target = (CriteriaImpl)a;
                var source = (Impl.CriteriaImpl)b;
                target.GetType()
                      .GetField("persistentClass", ReflectionFlags)
                      .SetValue(target, source.PersistantType.Type);
            });
        }

        public object Convert(object input)
        {
            var objectMap = new Dictionary<object, object>();

            return Convert(objectMap, input);
        }

        private object Convert(Dictionary<object, object> map, object input)
        {
            Debug.WriteLine("Converting : " + (input == null ? "nothing" : input.GetType().ToString()));

            object mapped = GetMappedObject(map, input);
            return mapped;
        }

        private void SetValues(Dictionary<object, object> map, object input, object mapped)
        {
            if (input == null || mapped == null)
                return;


            Dictionary<string, FieldInfo> srcFieldInfos =
                input.GetType()
                    .GetFields(ReflectionFlags)
                    .Where(a => a.GetCustomAttributes(typeof(DataMemberAttribute), true).Any())
                    .ToDictionary(a => a.Name, a => a);

            Dictionary<string, FieldInfo> mappedFieldInfos =
                mapped.GetType()
                    .GetFields(ReflectionFlags)
                    .Where(a => srcFieldInfos.ContainsKey(a.Name))
                    .ToDictionary(a => a.Name, a => a);

            if (mappedFieldInfos.Count != srcFieldInfos.Count)
                throw new FieldMappingCountMismatchException("unable to map fields for type: " + mapped.GetType().FullName);


            foreach (var mappedFieldInfo in mappedFieldInfos)
            {
                mappedFieldInfo.Value.SetValue(mapped, Convert(map, srcFieldInfos[mappedFieldInfo.Key].GetValue(input)));
            }

            Action<object, object> specialMap;
            if (_specialMappings.TryGetValue(mapped.GetType(), out specialMap))
                specialMap(mapped, input);
        }

        private object GetMappedObject(Dictionary<object, object> map, object from)
        {
            if (from == null)
                return null;

            if (from is string || from is DateTime || from is Guid || from.GetType().IsPrimitive)
                return from;

            return BuildObject(map, from);
        }

        private object BuildObject(Dictionary<object, object> map, object from)
        {
            if (map.ContainsKey(from))
                return map[from];

            Type srcType = from.GetType();
            Type newType;

            Func<object, object> instantiator;

            if (!_typeMap.TryGetValue(srcType, out newType))
            {
                newType = GetMappedType(srcType, out instantiator, map);
                _typeMap.TryAdd(srcType, newType);
                _factories.TryAdd(newType, instantiator);
            }

            if (_factories.TryGetValue(newType, out instantiator))
            {
                object obj = instantiator(from);
                map.Add(from, obj);
                SetValues(map, from, obj);
                return obj;
            }

            throw new Exception("arghh");
        }

        private Type GetMappedType(Type srcType, out Func<object, object> instantiator, Dictionary<object, object> map)
        {

            string name = srcType.FullName.Replace(typeof(NHibernateClient.IQuery).Namespace,
                                                   typeof(NHibernate.IQuery).Namespace);

            Type t = ReflectHelper.ClassForFullName(name);

            if (_factories.ContainsKey(t))
            {
                Func<object, object> initializer = _factories[t];
                instantiator = a => initializer(a);

                return t;
            }

            if (t.IsEnum)
            {
                instantiator = b => Enum.Parse(t, b.ToString());
                return t;
            }

            if (typeof(IDictionary).IsAssignableFrom(t))
            {
                instantiator = a =>
                {
                    var dict = (IDictionary)Activator.CreateInstance(t);
                    var copyFrom = (IDictionary)a;
                    foreach (object obj in copyFrom.Keys)
                    {
                        dict.Add(Convert(map, obj), Convert(map, copyFrom[obj]));
                    }
                    return dict;
                };
                return t;
            }



            if (typeof(IList).IsAssignableFrom(t))
            {
                instantiator = a =>
                {
                    var input = (IList)a;
                    IList newCollection;
                    if (!t.IsArray)
                    {
                        newCollection = (IList)Activator.CreateInstance(t);
                        foreach (object item in input)
                        {
                            newCollection.Add(Convert(map, item));
                        }
                    }
                    else
                    {
                        newCollection = Array.CreateInstance(t.GetElementType(), input.Count);
                        for (int i = 0; i < input.Count; i++)
                        {
                            newCollection[i] = Convert(map, input[i]);
                        }
                    }
                    return newCollection;
                };

                return t;
            }


            ConstructorInfo ci = t.GetConstructors(ReflectionFlags).OrderBy(a => a.GetParameters().Length).First();

            if (ci == null)
                throw new MissingMethodException("Constructor not found on: " + t.FullName);


            ParameterInfo[] parameterInfos = ci.GetParameters();
            var parameters = new object[parameterInfos.Length];

            if (parameters.Length > 0)
                throw new Exception("need to create default internal constructor on type: " + ci.ReflectedType.AssemblyQualifiedName);

            //for (int i = 0; i < parameterInfos.Length; i++)
            //{
            //    ParameterInfo pi = parameterInfos[i];
            //    parameters[i] = pi.ParameterType.IsClass ||
            //                    pi.ParameterType.IsInterface
            //                        ? Clone(
            //                            map.Values.Where(a => pi.ParameterType.IsAssignableFrom(a.GetType())).
            //                                FirstOrDefault())
            //                        : Activator.CreateInstance(pi.ParameterType);
            //}


            instantiator = a => ci.Invoke(parameters);
            return t;
        }
    }

    public class FieldMappingCountMismatchException : Exception
    {
        public FieldMappingCountMismatchException(string message)
            : base(message)
        {

        }
    }
}