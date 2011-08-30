using System;
using System.Runtime.Serialization;

//using NHibernate.SqlCommand;

namespace NHibernateClient.Criterion
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class ExistsSubqueryExpression : SubqueryExpression
    {
        //protected override SqlString ToLeftSqlString(ICriteria criteria, ICriteriaQuery outerQuery)
        //{
        //    return SqlString.Empty;
        //}

        internal ExistsSubqueryExpression(String quantifier, DetachedCriteria dc) : base(null, quantifier, dc)
        {
        }
    }
}