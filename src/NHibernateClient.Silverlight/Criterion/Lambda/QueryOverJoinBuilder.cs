using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernateClient.Impl;
using NHibernateClient.SqlCommand;

namespace NHibernateClient.Criterion.Lambda
{
    public class QueryOverJoinBuilder<TRoot, TSubType> :
        QueryOverJoinBuilderBase<QueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public QueryOverJoinBuilder(QueryOver<TRoot, TSubType> root, JoinType joinType) : base(root, joinType)
        {
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path,
                                                    Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public QueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }
    }

    public class IQueryOverJoinBuilder<TRoot, TSubType> :
        QueryOverJoinBuilderBase<IQueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public IQueryOverJoinBuilder(IQueryOver<TRoot, TSubType> root, JoinType joinType) : base(root, joinType)
        {
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, U>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<U>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path)
        {
            return root.JoinQueryOver<U>(path, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<TSubType, IEnumerable<U>>> path,
                                                     Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }

        public IQueryOver<TRoot, U> JoinQueryOver<U>(Expression<Func<IEnumerable<U>>> path, Expression<Func<U>> alias)
        {
            return root.JoinQueryOver<U>(path, alias, joinType);
        }
    }

    public class QueryOverJoinBuilderBase<TReturn, TRoot, TSubType> where TReturn : IQueryOver<TRoot, TSubType>
    {
        protected TReturn root;
        protected JoinType joinType;

        public QueryOverJoinBuilderBase(TReturn root, JoinType joinType)
        {
            this.root = root;
            this.joinType = joinType;
        }

        public TReturn JoinAlias(Expression<Func<TSubType, object>> path, Expression<Func<object>> alias)
        {
            return (TReturn) root.JoinAlias(path, alias, joinType);
        }

        public TReturn JoinAlias(Expression<Func<object>> path, Expression<Func<object>> alias)
        {
            return (TReturn) root.JoinAlias(path, alias, joinType);
        }
    }
}