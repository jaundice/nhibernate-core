using System;
using System.Collections;
using System.Runtime.Serialization;

namespace NHibernateClient.Transform
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class RootEntityResultTransformer : IResultTransformer
    {
        public object TransformTuple(object[] tuple, string[] aliases)
        {
            return tuple[tuple.Length - 1];
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }
    }
}