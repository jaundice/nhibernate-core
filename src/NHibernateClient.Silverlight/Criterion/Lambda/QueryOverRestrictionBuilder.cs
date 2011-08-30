using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using NHibernateClient.Impl;
using NHibernateClient.SqlCommand;

namespace NHibernateClient.Criterion.Lambda
{
    [DataContract(IsReference = true)]
    public class QueryOverRestrictionBuilder<TRoot, TSubType> :
        QueryOverRestrictionBuilderBase<QueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public QueryOverRestrictionBuilder(QueryOver<TRoot, TSubType> root, string propertyName)
            : base(root, propertyName)
        {
        }

        public QueryOverRestrictionBuilder<TRoot, TSubType> Not
        {
            get
            {
                isNot = !isNot;
                return this;
            }
        }
    }

    [DataContract(IsReference = true)]
    public class IQueryOverRestrictionBuilder<TRoot, TSubType> :
        QueryOverRestrictionBuilderBase<IQueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public IQueryOverRestrictionBuilder(IQueryOver<TRoot, TSubType> root, string propertyName)
            : base(root, propertyName)
        {
        }

        public IQueryOverRestrictionBuilder<TRoot, TSubType> Not
        {
            get
            {
                isNot = !isNot;
                return this;
            }
        }
    }

    [DataContract(IsReference = true)]
    public class QueryOverRestrictionBuilderBase<TReturn, TRoot, TSubType>
        where TReturn : IQueryOver<TRoot, TSubType>
    {
        public class LambdaBetweenBuilder
        {
            [DataMember] internal TReturn root;
            [DataMember] internal string propertyName;
            [DataMember] internal bool isNot;
            [DataMember] internal object lo;

            public LambdaBetweenBuilder(TReturn root, string propertyName, bool isNot, object lo)
            {
                this.root = root;
                this.propertyName = propertyName;
                this.isNot = isNot;
                this.lo = lo;
            }

            private TReturn Add(ICriterion criterion)
            {
                if (isNot)
                    criterion = Restrictions.Not(criterion);

                return (TReturn) root.And(criterion);
            }

            public TReturn And(object hi)
            {
                return Add(Restrictions.Between(propertyName, lo, hi));
            }
        }

        [DataMember] internal TReturn root;
        [DataMember] internal string propertyName;
        [DataMember] protected internal bool isNot;

        /// <summary>
        /// Constructed with property name
        /// </summary>
        public QueryOverRestrictionBuilderBase(TReturn root, string propertyName)
        {
            this.root = root;
            this.propertyName = propertyName;
        }

        private TReturn Add(ICriterion criterion)
        {
            if (isNot)
                criterion = Restrictions.Not(criterion);

            return (TReturn) root.And(criterion);
        }

        /// <summary>
        /// Apply a "between" constraint to the named property
        /// </summary>
        public LambdaBetweenBuilder IsBetween(object lo)
        {
            return new LambdaBetweenBuilder(root, propertyName, isNot, lo);
        }

        /// <summary>
        /// Apply an "in" constraint to the named property
        /// </summary>
        public TReturn IsIn(ICollection values)
        {
            return Add(Restrictions.In(propertyName, values));
        }

        /// <summary>
        /// Apply an "in" constraint to the named property
        /// </summary>
        public TReturn IsIn(object[] values)
        {
            return Add(Restrictions.In(propertyName, values));
        }

        /// <summary>
        /// Apply an "in" constraint to the named property
        /// </summary>
        public TReturn IsInG<T>(ICollection<T> values)
        {
            return Add(Restrictions.InG(propertyName, values));
        }

        /// <summary>
        /// A case-insensitive "like", similar to Postgres "ilike" operator
        /// </summary>
        public TReturn IsInsensitiveLike(object value)
        {
            return Add(Restrictions.InsensitiveLike(propertyName, value));
        }

        /// <summary>
        /// A case-insensitive "like", similar to Postgres "ilike" operator
        /// </summary>
        public TReturn IsInsensitiveLike(string value, MatchMode matchMode)
        {
            return Add(Restrictions.InsensitiveLike(propertyName, value, matchMode));
        }

        /// <summary>
        /// Apply an "is empty" constraint to the named property
        /// </summary>
        public TReturn IsEmpty
        {
            get { return Add(Restrictions.IsEmpty(propertyName)); }
        }

        /// <summary>
        /// Apply a "not is empty" constraint to the named property
        /// </summary>
        public TReturn IsNotEmpty
        {
            get { return Add(Restrictions.IsNotEmpty(propertyName)); }
        }

        /// <summary>
        /// Apply an "is null" constraint to the named property
        /// </summary>
        public TReturn IsNull
        {
            get { return Add(Restrictions.IsNull(propertyName)); }
        }

        /// <summary>
        /// Apply an "not is null" constraint to the named property
        /// </summary>
        public TReturn IsNotNull
        {
            get { return Add(Restrictions.IsNotNull(propertyName)); }
        }

        /// <summary>
        /// Apply a "like" constraint to the named property
        /// </summary>
        public TReturn IsLike(object value)
        {
            return Add(Restrictions.Like(propertyName, value));
        }

        /// <summary>
        /// Apply a "like" constraint to the named property
        /// </summary>
        public TReturn IsLike(string value, MatchMode matchMode)
        {
            return Add(Restrictions.Like(propertyName, value, matchMode));
        }

        /// <summary>
        /// Apply a "like" constraint to the named property
        /// </summary>
        public TReturn IsLike(string value, MatchMode matchMode, char? escapeChar)
        {
            return Add(Restrictions.Like(propertyName, value, matchMode, escapeChar));
        }
    }
}