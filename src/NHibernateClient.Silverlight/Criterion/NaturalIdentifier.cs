using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using NHibernateClient.Criterion.Lambda;
using NHibernateClient.Impl;

//using NHibernate.Engine;
//using NHibernate.Impl;
//using NHibernate.SqlCommand;

namespace NHibernateClient.Criterion
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class NaturalIdentifier : ICriterion
    {
        [DataMember(Name="Conjunction")] internal Junction conjunction = new Conjunction();

        //public SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery,
        //                             IDictionary<string, IFilter> enabledFilters)
        //{
        //    return conjunction.ToSqlString(criteria, criteriaQuery, enabledFilters);
        //}

        //public TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return conjunction.GetTypedValues(criteria, criteriaQuery);
        //}

        public IProjection[] GetProjections()
        {
            return conjunction.GetProjections();
        }

        public NaturalIdentifier Set(string property, object value)
        {
            conjunction.Add(Restrictions.Eq(property, value));
            return this;
        }

        public LambdaNaturalIdentifierBuilder Set<T>(Expression<Func<T, object>> expression)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return new LambdaNaturalIdentifierBuilder(this, property);
        }

        public LambdaNaturalIdentifierBuilder Set(Expression<Func<object>> expression)
        {
            string property = ExpressionProcessor.FindMemberExpression(expression.Body);
            return new LambdaNaturalIdentifierBuilder(this, property);
        }
    }
}