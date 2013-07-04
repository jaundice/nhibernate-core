﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using NHibernateClient.Criterion;
using NHibernateClient.Engine;
using NHibernateClient.Impl;
using NHibernateClient.SqlCommand;
using NHibernateClient.Transform;
using NHibernateClient.Util;

namespace NHibernateClient.Impl
{
    /// <summary>
    /// Implementation of the <see cref="ICriteria"/> interface
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class CriteriaImpl : ICriteria
    {
        internal System.Type persistentClass;
        [DataMember(Name = "Criteria")]
        internal List<CriterionEntry> criteria = new List<CriterionEntry>();
        [DataMember(Name = "OrderEntries")]
        internal List<OrderEntry> orderEntries = new List<OrderEntry>(10);

        [DataMember(Name = "FetchModes")]
        internal Dictionary<string, FetchMode> fetchModes =
            new Dictionary<string, FetchMode>();

        [DataMember(Name = "LockModes")]
        internal Dictionary<string, LockMode> lockModes =
            new Dictionary<string, LockMode>();

        [DataMember(Name = "MaxResults")]
        internal int maxResults = RowSelection.NoValue;
        [DataMember(Name = "FirstResult")]
        internal int firstResult;
        [DataMember(Name = "Timeout")]
        internal int timeout = RowSelection.NoValue;
        [DataMember(Name = "FetchSize")]
        internal int fetchSize = RowSelection.NoValue;

        [DataMember(Name = "ResultTransformer")]
        internal IResultTransformer resultTransformer =
            CriteriaSpecification.RootEntity;

        [DataMember(Name = "Cacheable")]
        internal bool cacheable;
        [DataMember(Name = "CacheRegion")]
        internal string cacheRegion;

        [DataMember(Name = "Comment")]
        internal string comment;

        [DataMember(Name = "SubCriteria")]
        internal List<Subcriteria> subcriteriaList = new List<Subcriteria>();

        [DataMember(Name = "RootAlias")]
        internal string rootAlias;

        [DataMember(Name = "SubCriteriaByPath")]
        internal Dictionary<string, ICriteria> subcriteriaByPath =
            new Dictionary<string, ICriteria>();

        [DataMember(Name = "SubCriteriaByAlias")]
        internal Dictionary<string, ICriteria> subcriteriaByAlias =
            new Dictionary<string, ICriteria>();

        [DataMember(Name = "EntityOrClassName")]
        internal string entityOrClassName;

        // Projection Fields
        [DataMember(Name = "Projection")]
        internal IProjection projection;
        [DataMember(Name = "ProjectionCriteria")]
        internal ICriteria projectionCriteria;

        internal CriteriaImpl()
            : this("tobesetbydeserializer", CriteriaSpecification.RootAlias)
        {
            //serialization only
        }

        public CriteriaImpl(System.Type persistentClass)
            : this(persistentClass.FullName, CriteriaSpecification.RootAlias)
        {
            this.persistentClass = persistentClass;
        }

        public CriteriaImpl(System.Type persistentClass, string alias)
            : this(persistentClass.FullName, alias)
        {
            this.persistentClass = persistentClass;
        }

        public CriteriaImpl(string entityOrClassName)
            : this(entityOrClassName, CriteriaSpecification.RootAlias)
        {
        }

        public CriteriaImpl(string entityOrClassName, string alias)
        {
            this.entityOrClassName = entityOrClassName;
            cacheable = false;
            rootAlias = alias;
            subcriteriaByAlias[alias ?? ""] = this;
        }

        //public ISessionImplementor Session
        //{
        //    get { return session; }
        //    set { session = value; }
        //}

        public string EntityOrClassName
        {
            get { return entityOrClassName; }
        }

        public IDictionary<string, LockMode> LockModes
        {
            get { return lockModes; }
        }

        public ICriteria ProjectionCriteria
        {
            get { return projectionCriteria; }
        }

        public bool LookupByNaturalKey
        {
            get
            {
                if (projection != null)
                {
                    return false;
                }
                if (subcriteriaList.Count > 0)
                {
                    return false;
                }
                if (criteria.Count != 1)
                {
                    return false;
                }

                CriterionEntry ce = criteria[0];
                return ce.Criterion is NaturalIdentifier;
            }
        }

        public string Alias
        {
            get { return rootAlias; }
        }

        public IProjection Projection
        {
            get { return projection; }
        }

        public FetchMode GetFetchMode(string path)
        {
            FetchMode result;
            if (!fetchModes.TryGetValue(path, out result))
            {
                result = FetchMode.Default;
            }
            return result;
        }

        public IResultTransformer ResultTransformer
        {
            get { return resultTransformer; }
        }

        public int MaxResults
        {
            get { return maxResults; }
        }

        public int FirstResult
        {
            get { return firstResult; }
        }

        public int FetchSize
        {
            get { return fetchSize; }
        }

        public int Timeout
        {
            get { return timeout; }
        }

        public bool Cacheable
        {
            get { return cacheable; }
        }

        public string CacheRegion
        {
            get { return cacheRegion; }
        }

        public string Comment
        {
            get { return comment; }
        }

        //protected internal void Before()
        //{
        //    if (flushMode.HasValue)
        //    {
        //        sessionFlushMode = Session.FlushMode;
        //        Session.FlushMode = flushMode.Value;
        //    }
        //    if (cacheMode.HasValue)
        //    {
        //        sessionCacheMode = Session.CacheMode;
        //        Session.CacheMode = cacheMode.Value;
        //    }
        //}

        //protected internal void After()
        //{
        //    if (sessionFlushMode.HasValue)
        //    {
        //        Session.FlushMode = sessionFlushMode.Value;
        //        sessionFlushMode = null;
        //    }
        //    if (sessionCacheMode.HasValue)
        //    {
        //        Session.CacheMode = sessionCacheMode.Value;
        //        sessionCacheMode = null;
        //    }
        //}

        public ICriteria SetMaxResults(int maxResults)
        {
            this.maxResults = maxResults;
            return this;
        }

        public ICriteria SetFirstResult(int firstResult)
        {
            this.firstResult = firstResult;
            return this;
        }

        public ICriteria SetTimeout(int timeout)
        {
            this.timeout = timeout;
            return this;
        }

        public ICriteria SetFetchSize(int fetchSize)
        {
            this.fetchSize = fetchSize;
            return this;
        }

        public ICriteria Add(ICriterion expression)
        {
            Add(this, expression);
            return this;
        }

        public IList List()
        {
            List<object> results = new List<object>();
            List(results);
            return results;
        }

        public void List(IList results)
        {
            throw new NotImplementedException();
            //Before();
            //try
            //{
            //    session.List(this, results);
            //}
            //finally
            //{
            //    After();
            //}
        }

        public IList<T> List<T>()
        {
            throw new NotImplementedException();
            //List<T> results = new List<T>();
            //List(results);
            //return results;
        }

        public T UniqueResult<T>()
        {
            object result = UniqueResult();
            if (result == null && typeof(T).IsValueType)
            {
                return default(T);
            }
            else
            {
                return (T)result;
            }
        }

        public void ClearOrders()
        {
            orderEntries.Clear();
        }

        public IEnumerable<CriterionEntry> IterateExpressionEntries()
        {
            return criteria;
        }

        public IEnumerable<OrderEntry> IterateOrderings()
        {
            return orderEntries;
        }

        public IEnumerable<Subcriteria> IterateSubcriteria()
        {
            return subcriteriaList;
        }

        public override string ToString()
        {
            bool first = true;
            StringBuilder builder = new StringBuilder();
            foreach (CriterionEntry criterionEntry in criteria)
            {
                if (!first)
                {
                    builder.Append(" and ");
                }
                builder.Append(criterionEntry.ToString());
                first = false;
            }
            if (orderEntries.Count != 0)
            {
                builder.Append(Environment.NewLine);
            }
            first = true;
            foreach (OrderEntry orderEntry in orderEntries)
            {
                if (!first)
                {
                    builder.Append(" and ");
                }
                builder.Append(orderEntry.ToString());
                first = false;
            }
            return builder.ToString();
        }

        public ICriteria AddOrder(Order ordering)
        {
            orderEntries.Add(new OrderEntry(ordering, this));
            return this;
        }

        public ICriteria SetFetchMode(string associationPath, FetchMode mode)
        {
            fetchModes[associationPath] = mode;
            return this;
        }

        public ICriteria CreateAlias(string associationPath, string alias)
        {
            CreateAlias(associationPath, alias, JoinType.InnerJoin);
            return this;
        }

        public ICriteria CreateAlias(string associationPath, string alias, JoinType joinType)
        {
            new Subcriteria(this, this, associationPath, alias, joinType);
            return this;
        }

        public ICriteria CreateAlias(string associationPath, string alias, JoinType joinType, ICriterion withClause)
        {
            new Subcriteria(this, this, associationPath, alias, joinType, withClause);
            return this;
        }

        public ICriteria Add(ICriteria criteriaInst, ICriterion expression)
        {
            criteria.Add(new CriterionEntry(expression, criteriaInst));
            return this;
        }

        public ICriteria CreateCriteria(string associationPath)
        {
            return CreateCriteria(associationPath, JoinType.InnerJoin);
        }

        public ICriteria CreateCriteria(string associationPath, JoinType joinType)
        {
            return new Subcriteria(this, this, associationPath, joinType);
        }

        public ICriteria CreateCriteria(string associationPath, string alias)
        {
            return CreateCriteria(associationPath, alias, JoinType.InnerJoin);
        }

        public ICriteria CreateCriteria(string associationPath, string alias, JoinType joinType)
        {
            return new Subcriteria(this, this, associationPath, alias, joinType);
        }

        public ICriteria CreateCriteria(string associationPath, string alias, JoinType joinType, ICriterion withClause)
        {
            return new Subcriteria(this, this, associationPath, alias, joinType, withClause);
        }

        //public IFutureValue<T> FutureValue<T>()
        //{
        //    if (!session.Factory.ConnectionProvider.Driver.SupportsMultipleQueries)
        //    {
        //        return new FutureValue<T>(List<T>);
        //    }

        //    session.FutureCriteriaBatch.Add<T>(this);
        //    return session.FutureCriteriaBatch.GetFutureValue<T>();
        //}

        //public IEnumerable<T> Future<T>()
        //{
        //    if (!session.Factory.ConnectionProvider.Driver.SupportsMultipleQueries)
        //    {
        //        return List<T>();
        //    }

        //    session.FutureCriteriaBatch.Add<T>(this);
        //    return session.FutureCriteriaBatch.GetEnumerator<T>();
        //}

        public object UniqueResult()
        {
            return AbstractQueryImpl.UniqueElement(List());
        }

        public ICriteria SetLockMode(LockMode lockMode)
        {
            return SetLockMode(CriteriaSpecification.RootAlias, lockMode);
        }

        public ICriteria SetLockMode(string alias, LockMode lockMode)
        {
            lockModes[alias] = lockMode;
            return this;
        }

        public ICriteria SetResultTransformer(IResultTransformer tupleMapper)
        {
            resultTransformer = tupleMapper;
            return this;
        }

        public ICriteria SetCacheable(bool cacheable)
        {
            this.cacheable = cacheable;
            return this;
        }

        public ICriteria SetCacheRegion(string cacheRegion)
        {
            this.cacheRegion = cacheRegion.Trim();
            return this;
        }

        public ICriteria SetComment(string comment)
        {
            this.comment = comment;
            return this;
        }

        //public ICriteria SetFlushMode(FlushMode flushMode)
        //{
        //    this.flushMode = flushMode;
        //    return this;
        //}

        public ICriteria SetProjection(params IProjection[] projections)
        {
            if (projections == null)
                throw new ArgumentNullException("projections");
            if (projections.Length == 0)
                throw new ArgumentException("projections must contain a least one projection");

            if (projections.Length == 1)
            {
                projection = projections[0];
            }
            else
            {
                var projectionList = new ProjectionList();
                foreach (var childProjection in projections)
                {
                    projectionList.Add(childProjection);
                }
                projection = projectionList;
            }

            if (projection != null)
            {
                projectionCriteria = this;
                SetResultTransformer(CriteriaSpecification.Projection);
            }

            return this;
        }

        ///// <summary> Override the cache mode for this particular query. </summary>
        ///// <param name="cacheMode">The cache mode to use. </param>
        ///// <returns> this (for method chaining) </returns>
        //public ICriteria SetCacheMode(CacheMode cacheMode)
        //{
        //    this.cacheMode = cacheMode;
        //    return this;
        //}

        public object Clone()
        {
            CriteriaImpl clone;
            if (persistentClass != null)
            {
                clone = new CriteriaImpl(persistentClass, Alias);
            }
            else
            {
                clone = new CriteriaImpl(entityOrClassName, Alias);
            }
            CloneSubcriteria(clone);
            foreach (KeyValuePair<string, FetchMode> de in fetchModes)
            {
                clone.fetchModes.Add(de.Key, de.Value);
            }
            foreach (KeyValuePair<string, LockMode> de in lockModes)
            {
                clone.lockModes.Add(de.Key, de.Value);
            }
            clone.maxResults = maxResults;
            clone.firstResult = firstResult;
            clone.timeout = timeout;
            clone.fetchSize = fetchSize;
            clone.cacheable = cacheable;
            clone.cacheRegion = cacheRegion;
            clone.SetProjection(projection);
            CloneProjectCrtieria(clone);
            clone.SetResultTransformer(resultTransformer);
            clone.comment = comment;

            return clone;
        }

        private void CloneProjectCrtieria(CriteriaImpl clone)
        {
            if (projectionCriteria != null)
            {
                if (projectionCriteria == this)
                {
                    clone.projectionCriteria = clone;
                }
                else
                {
                    ICriteria clonedProjectionCriteria = (ICriteria)projectionCriteria.Clone();
                    clone.projectionCriteria = clonedProjectionCriteria;
                }
            }
        }

        private void CloneSubcriteria(CriteriaImpl clone)
        {
            //we need to preserve the parent criteria, we rely on the ordering when creating the 
            //subcriterias initially here, so we don't need to make more than a single pass
            Dictionary<ICriteria, ICriteria> newParents = new Dictionary<ICriteria, ICriteria>();
            newParents[this] = clone;

            foreach (Subcriteria subcriteria in IterateSubcriteria())
            {
                ICriteria currentParent;
                if (!newParents.TryGetValue(subcriteria.Parent, out currentParent))
                {
                    throw new AssertionFailure(
                        "Could not find parent for subcriteria in the previous subcriteria. If you see this error, it is a bug");
                }
                Subcriteria clonedSubCriteria =
                    new Subcriteria(clone, currentParent, subcriteria.Path, subcriteria.Alias, subcriteria.JoinType);
                clonedSubCriteria.SetLockMode(subcriteria.LockMode);
                newParents[subcriteria] = clonedSubCriteria;
            }

            // remap the orders
            foreach (OrderEntry orderEntry in IterateOrderings())
            {
                ICriteria currentParent;
                if (!newParents.TryGetValue(orderEntry.Criteria, out currentParent))
                {
                    throw new AssertionFailure(
                        "Could not find parent for order in the previous criteria. If you see this error, it is a bug");
                }
                currentParent.AddOrder(orderEntry.Order);
            }

            // remap the restrictions to appropriate criterias
            foreach (CriterionEntry criterionEntry in criteria)
            {
                ICriteria currentParent;
                if (!newParents.TryGetValue(criterionEntry.Criteria, out currentParent))
                {
                    throw new AssertionFailure(
                        "Could not find parent for restriction in the previous criteria. If you see this error, it is a bug.");
                }

                currentParent.Add(criterionEntry.Criterion);
            }
        }

        public ICriteria GetCriteriaByPath(string path)
        {
            ICriteria result;
            subcriteriaByPath.TryGetValue(path, out result);
            return result;
        }

        public ICriteria GetCriteriaByAlias(string alias)
        {
            ICriteria result;
            subcriteriaByAlias.TryGetValue(alias, out result);
            return result;
        }

        [Serializable]
        [DataContract(IsReference = true)]
        public sealed class Subcriteria : ICriteria
        {
            // Added to simulate Java-style inner class
            [DataMember(Name = "Root")]
            internal CriteriaImpl root;
            [DataMember(Name = "Parent")]
            internal ICriteria parent;
            [DataMember(Name = "Alias")]
            internal string alias;
            [DataMember(Name = "Path")]
            internal string path;
            [DataMember(Name = "LockMode")]
            internal LockMode lockMode;
            [DataMember(Name = "JoinType")]
            internal JoinType joinType;
            [DataMember(Name = "WithClause")]
            internal ICriterion withClause;

            internal Subcriteria(CriteriaImpl root, ICriteria parent, string path, string alias, JoinType joinType,
                                 ICriterion withClause)
            {
                this.root = root;
                this.parent = parent;
                this.alias = alias;
                this.path = path;
                this.joinType = joinType;
                this.withClause = withClause;

                root.subcriteriaList.Add(this);

                root.subcriteriaByPath[path] = this;
                if (alias != null)
                {
                    root.subcriteriaByAlias[alias] = this;
                }
            }

            internal Subcriteria(CriteriaImpl root, ICriteria parent, string path, string alias, JoinType joinType)
                : this(root, parent, path, alias, joinType, null)
            {
            }

            internal Subcriteria(CriteriaImpl root, ICriteria parent, string path, JoinType joinType)
                : this(root, parent, path, null, joinType)
            {
            }

            public ICriterion WithClause
            {
                get { return withClause; }
            }

            public string Path
            {
                get { return path; }
            }

            public ICriteria Parent
            {
                get { return parent; }
            }

            public JoinType JoinType
            {
                get { return joinType; }
            }

            public string Alias
            {
                get { return alias; }
                set
                {
                    root.subcriteriaByAlias.Remove(alias);
                    alias = value;
                    root.subcriteriaByAlias[alias] = this;
                }
            }

            public LockMode LockMode
            {
                get { return lockMode; }
            }

            public ICriteria SetLockMode(LockMode lockMode)
            {
                this.lockMode = lockMode;
                return this;
            }

            public ICriteria Add(ICriterion expression)
            {
                root.Add(this, expression);
                return this;
            }

            public ICriteria AddOrder(Order order)
            {
                root.orderEntries.Add(new OrderEntry(order, this));
                return this;
            }

            public ICriteria CreateAlias(string associationPath, string alias)
            {
                return CreateAlias(associationPath, alias, JoinType.InnerJoin);
            }

            public ICriteria CreateAlias(string associationPath, string alias, JoinType joinType)
            {
                new Subcriteria(root, this, associationPath, alias, joinType);
                return this;
            }

            public ICriteria CreateAlias(string associationPath, string alias, JoinType joinType, ICriterion withClause)
            {
                new Subcriteria(root, this, associationPath, alias, joinType, withClause);
                return this;
            }

            public ICriteria CreateCriteria(string associationPath)
            {
                return CreateCriteria(associationPath, JoinType.InnerJoin);
            }

            public ICriteria CreateCriteria(string associationPath, JoinType joinType)
            {
                return new Subcriteria(root, this, associationPath, joinType);
            }

            public ICriteria CreateCriteria(string associationPath, string alias)
            {
                return CreateCriteria(associationPath, alias, JoinType.InnerJoin);
            }

            public ICriteria CreateCriteria(string associationPath, string alias, JoinType joinType)
            {
                return new Subcriteria(root, this, associationPath, alias, joinType);
            }

            public ICriteria CreateCriteria(string associationPath, string alias, JoinType joinType,
                                            ICriterion withClause)
            {
                return new Subcriteria(root, this, associationPath, alias, joinType, withClause);
            }

            public ICriteria SetCacheable(bool cacheable)
            {
                root.SetCacheable(cacheable);
                return this;
            }

            public ICriteria SetCacheRegion(string cacheRegion)
            {
                root.SetCacheRegion(cacheRegion);
                return this;
            }

            //public IList List()
            //{
            //    return root.List();
            //}

            //public IFutureValue<T> FutureValue<T>()
            //{
            //    return root.FutureValue<T>();
            //}

            //public IEnumerable<T> Future<T>()
            //{
            //    return root.Future<T>();
            //}

            //public void List(IList results)
            //{
            //    root.List(results);
            //}

            //public IList<T> List<T>()
            //{
            //    return root.List<T>();
            //}

            public T UniqueResult<T>()
            {
                object result = UniqueResult();
                if (result == null && typeof(T).IsValueType)
                {
                    throw new InvalidCastException(
                        "UniqueResult<T>() cannot cast null result to value type. Call UniqueResult<T?>() instead");
                }
                else
                {
                    return (T)result;
                }
            }

            public void ClearOrders()
            {
                root.ClearOrders();
            }

            public object UniqueResult()
            {
                return root.UniqueResult();
            }

            public ICriteria SetFetchMode(string associationPath, FetchMode mode)
            {
                root.SetFetchMode(StringHelper.Qualify(path, associationPath), mode);
                return this;
            }

            //public ICriteria SetFlushMode(FlushMode flushMode)
            //{
            //    root.SetFlushMode(flushMode);
            //    return this;
            //}

            ///// <summary> Override the cache mode for this particular query. </summary>
            ///// <param name="cacheMode">The cache mode to use. </param>
            ///// <returns> this (for method chaining) </returns>
            //public ICriteria SetCacheMode(CacheMode cacheMode)
            //{
            //    root.SetCacheMode(cacheMode);
            //    return this;
            //}

            public ICriteria SetFirstResult(int firstResult)
            {
                root.SetFirstResult(firstResult);
                return this;
            }

            public ICriteria SetMaxResults(int maxResults)
            {
                root.SetMaxResults(maxResults);
                return this;
            }

            public ICriteria SetTimeout(int timeout)
            {
                root.SetTimeout(timeout);
                return this;
            }

            public ICriteria SetFetchSize(int fetchSize)
            {
                root.SetFetchSize(fetchSize);
                return this;
            }

            public ICriteria SetLockMode(string alias, LockMode lockMode)
            {
                root.SetLockMode(alias, lockMode);
                return this;
            }

            public ICriteria SetResultTransformer(IResultTransformer resultProcessor)
            {
                root.SetResultTransformer(resultProcessor);
                return this;
            }

            public ICriteria SetComment(string comment)
            {
                root.SetComment(comment);
                return this;
            }

            public ICriteria SetProjection(params IProjection[] projections)
            {
                root.SetProjection(projections);
                return this;
            }

            public ICriteria GetCriteriaByPath(string path)
            {
                return root.GetCriteriaByPath(path);
            }

            public ICriteria GetCriteriaByAlias(string alias)
            {
                return root.GetCriteriaByAlias(alias);
            }

            public System.Type GetRootEntityTypeIfAvailable()
            {
                return root.GetRootEntityTypeIfAvailable();
            }

            /// <summary>
            /// The Clone is supported only by a root criteria.
            /// </summary>
            /// <returns>The clone of the root criteria.</returns>
            public object Clone()
            {
                // implemented only for compatibility with CriteriaTransformer
                return root.Clone();
            }
        }

        [Serializable]
        [DataContract(IsReference = true)]
        public sealed class CriterionEntry
        {
            [DataMember(Name = "Criterion")]
            internal ICriterion criterion;
            [DataMember(Name = "Criteria")]
            internal ICriteria criteria;

            internal CriterionEntry(ICriterion criterion, ICriteria criteria)
            {
                this.criterion = criterion;
                this.criteria = criteria;
            }

            public ICriterion Criterion
            {
                get { return criterion; }
            }

            public ICriteria Criteria
            {
                get { return criteria; }
            }

            public override string ToString()
            {
                return criterion.ToString();
            }
        }

        [Serializable]
        [DataContract(IsReference = true)]
        public sealed class OrderEntry
        {
            [DataMember(Name = "Order")]
            internal Order order;
            [DataMember(Name = "Criteria")]
            internal ICriteria criteria;

            internal OrderEntry(Order order, ICriteria criteria)
            {
                this.order = order;
                this.criteria = criteria;
            }

            public Order Order
            {
                get { return order; }
            }

            public ICriteria Criteria
            {
                get { return criteria; }
            }

            public override string ToString()
            {
                return order.ToString();
            }
        }

        public System.Type GetRootEntityTypeIfAvailable()
        {
            if (persistentClass != null)
                return persistentClass;
            throw new HibernateException(
                "Cannot provide root entity type because this criteria was initialized with an entity name.");
        }

        [DataMember(Name = "PersistantType")]
        public TypeWrapper PersistantType
        {
            get { return new TypeWrapper { Type = persistentClass }; }
            set { persistentClass = value.Type; }
        }
    }

    internal class AssertionFailure : Exception
    {
        public AssertionFailure(string s)
            : base(s)
        {
        }
    }
}