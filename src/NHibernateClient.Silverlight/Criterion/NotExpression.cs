using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//using NHibernate.Dialect;
//using NHibernate.Engine;
//using NHibernate.SqlCommand;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// An <see cref="ICriterion"/> that negates another <see cref="ICriterion"/>.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class NotExpression : AbstractCriterion
    {
        [DataMember(Name = "Criterion")]
        internal ICriterion _criterion;

        /// <summary>
        /// Initialize a new instance of the <see cref="NotExpression" /> class for an
        /// <see cref="ICriterion"/>
        /// </summary>
        /// <param name="criterion">The <see cref="ICriterion"/> to negate.</param>
        public NotExpression(ICriterion criterion)
        {
            _criterion = criterion;
        }

        //public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    //TODO: set default capacity
        //    SqlStringBuilder builder = new SqlStringBuilder();
        //    builder.Add("not (");
        //    builder.Add(_criterion.ToSqlString(criteria, criteriaQuery, enabledFilters));
        //    builder.Add(")");

        //    return builder.ToSqlString();
        //}

        //public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return _criterion.GetTypedValues(criteria, criteriaQuery);
        //}

        public override string ToString()
        {
            return string.Format("not ({0})", _criterion.ToString());
        }

        public override IProjection[] GetProjections()
        {
            return null;
        }
    }
}