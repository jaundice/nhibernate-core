using System;
using NHibernate.SqlCommand;

namespace NHibernate.Criterion
{
	[Serializable]
	public class ExistsSubqueryExpression : SubqueryExpression
	{
        /// <summary>
        /// jd:used to deserialize from NHibernateClient
        /// </summary>
        internal ExistsSubqueryExpression():base()
        {
            
        }

		protected override SqlString ToLeftSqlString(ICriteria criteria, ICriteriaQuery outerQuery)
		{
			return SqlString.Empty;
		}

		internal ExistsSubqueryExpression(String quantifier, DetachedCriteria dc) : base(null, quantifier, dc)
		{
		}
	}
}