using System;
using System.Runtime.Serialization;

//using NHibernate.SqlCommand;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// A comparison between a property value in the outer query and the
    ///  result of a subquery
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class SelectSubqueryExpression : SubqueryExpression
    {
        internal SelectSubqueryExpression(DetachedCriteria dc)
            : base(null, null, dc)
        {
        }

        //protected override SqlString ToLeftSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return SqlString.Empty;
        //}
    }
}