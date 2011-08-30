using System;
using System.Collections;
using System.Runtime.Serialization;

namespace NHibernateClient.Transform
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class PassThroughResultTransformer : IResultTransformer
    {
        #region IResultTransformer Members

        public object TransformTuple(object[] tuple, string[] aliases)
        {
            return tuple.Length == 1 ? tuple[0] : tuple;
        }

        public IList TransformList(IList collection)
        {
            return collection;
        }

        #endregion
    }
}