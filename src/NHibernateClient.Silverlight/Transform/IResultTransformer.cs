using System;
using System.Collections;

namespace NHibernateClient.Transform
{
    /// <summary>
    /// Implementors define a strategy for transforming criteria query
    /// results into the actual application-visible query result list.
    /// </summary>
    /// <seealso cref="ICriteria.SetResultTransformer(IResultTransformer)" />
    public interface IResultTransformer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tuple"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        object TransformTuple(object[] tuple, string[] aliases);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        IList TransformList(IList collection);
    }
}