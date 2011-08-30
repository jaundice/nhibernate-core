using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NHibernateClient.Transform
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class AliasToEntityMapResultTransformer : IResultTransformer
    {
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            IDictionary result = new Dictionary<object, object>();
            for (int i = 0; i < tuple.Length; i++)
            {
                string alias = aliases[i];
                if (alias != null)
                {
                    // TODO: Incredibly dodgy!! what if the user defines an alias ending in "_"
                    result[alias] = tuple[i];
                }
            }

            return result;
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }
    }
}