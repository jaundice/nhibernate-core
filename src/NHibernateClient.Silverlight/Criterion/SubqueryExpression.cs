using System;
using System.Collections.Generic;
//using NHibernate.Engine;
using System.Runtime.Serialization;
using NHibernateClient.Impl;

//using NHibernate.Loader.Criteria;
//using NHibernate.Persister.Entity;
//using NHibernate.SqlCommand;
//using NHibernate.Type;

namespace NHibernateClient.Criterion
{
    [Serializable]
    [DataContract(IsReference = true)]
    public abstract class SubqueryExpression : AbstractCriterion
    {
        [DataMember(Name = "CriteriaImpl")]
        internal CriteriaImpl criteriaImpl;
        [DataMember(Name = "Quantifier")]
        internal String quantifier;
        [DataMember(Name = "PrefixOp")]
        internal bool prefixOp;
        [DataMember(Name = "Op")]
        internal String op;
        //private QueryParameters parameters;
        //private IType[] types;

        //[NonSerialized] private CriteriaQueryTranslator innerQuery;

        protected SubqueryExpression(String op, String quantifier, DetachedCriteria dc)
            : this(op, quantifier, dc, true)
        {
        }

        protected SubqueryExpression(String op, String quantifier, DetachedCriteria dc, bool prefixOp)
        {
            criteriaImpl = dc.GetCriteriaImpl();
            this.quantifier = quantifier;
            this.prefixOp = prefixOp;
            this.op = op;
        }

        //public IType[] GetTypes()
        //{
        //    return types;
        //}

        //protected abstract SqlString ToLeftSqlString(ICriteria criteria, ICriteriaQuery outerQuery);

        //public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery,
        //                                      IDictionary<string, IFilter> enabledFilters)
        //{
        //    InitializeInnerQueryAndParameters(criteriaQuery);

        //    if (innerQuery.HasProjection == false)
        //    {
        //        throw new QueryException("Cannot use subqueries on a criteria without a projection.");
        //    }

        //    ISessionFactoryImplementor factory = criteriaQuery.Factory;

        //    IOuterJoinLoadable persister = (IOuterJoinLoadable)factory.GetEntityPersister(criteriaImpl.EntityOrClassName);

        //    //buffer needs to be before CriteriaJoinWalker for sake of parameter order
        //    SqlStringBuilder buf = new SqlStringBuilder().Add(ToLeftSqlString(criteria, criteriaQuery));

        //    //patch to generate joins on subqueries
        //    //stolen from CriteriaLoader
        //    CriteriaJoinWalker walker =
        //        new CriteriaJoinWalker(persister, innerQuery, factory, criteriaImpl, criteriaImpl.EntityOrClassName, enabledFilters);

        //    SqlString sql = walker.SqlString;

        //    if (criteriaImpl.FirstResult != 0 || criteriaImpl.MaxResults != RowSelection.NoValue)
        //    {
        //        int maxResults = (criteriaImpl.MaxResults != RowSelection.NoValue) ? criteriaImpl.MaxResults : int.MaxValue;
        //        int? offsetParameterIndex = criteriaQuery.CreatePagingParameter(criteriaImpl.FirstResult);
        //        int? limitParameterIndex = criteriaQuery.CreatePagingParameter(maxResults);
        //        sql = factory.Dialect.GetLimitString(sql, criteriaImpl.FirstResult, criteriaImpl.MaxResults, offsetParameterIndex, limitParameterIndex);
        //    }

        //    if (op != null)
        //    {
        //        buf.Add(" ").Add(op).Add(" ");
        //    }

        //    if (quantifier != null && prefixOp)
        //    {
        //        buf.Add(quantifier).Add(" ");
        //    }

        //    buf.Add("(").Add(sql).Add(")");

        //    if(quantifier!=null && prefixOp==false)
        //    {
        //        buf.Add(" ").Add(quantifier);
        //    }

        //    return buf.ToSqlString();
        //}

        public override string ToString()
        {
            if (prefixOp)
                return string.Format("{0} {1} ({2})", op, quantifier, criteriaImpl);
            return string.Format("{0} ({1}) {2}", op, criteriaImpl, quantifier);
        }

        //public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    InitializeInnerQueryAndParameters(criteriaQuery);
        //    IType[] paramTypes = parameters.PositionalParameterTypes;
        //    Object[] values = parameters.PositionalParameterValues;
        //    TypedValue[] tv = new TypedValue[paramTypes.Length];
        //    for (int i = 0; i < paramTypes.Length; i++)
        //    {
        //        tv[i] = new TypedValue(paramTypes[i], values[i], EntityMode.Poco);
        //    }
        //    return tv;
        //}

        public override IProjection[] GetProjections()
        {
            return null;
        }

        //public void InitializeInnerQueryAndParameters(ICriteriaQuery criteriaQuery)
        //{
        //    ISessionFactoryImplementor factory = criteriaQuery.Factory;
        //    innerQuery =
        //        new CriteriaQueryTranslator(factory, criteriaImpl, //implicit polymorphism not supported (would need a union) 
        //                                    criteriaImpl.EntityOrClassName, criteriaQuery.GenerateSQLAlias(), criteriaQuery);
        //    if (innerQuery.HasProjection)
        //    {
        //        parameters = innerQuery.GetQueryParameters();
        //        types = innerQuery.ProjectedTypes;
        //    }
        //    else
        //    {
        //        types = null;
        //    }
        //}

        public ICriteria Criteria
        {
            // NH-1146
            get { return criteriaImpl; }
        }
    }
}