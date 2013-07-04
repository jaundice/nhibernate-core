using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NHibernateClient.SqlCommand;
using NHibernateClient.Impl;

namespace NHibernateClient.Criterion.Lambda
{
    public class QueryOverOrderBuilder<TRoot, TSubType> :
        QueryOverOrderBuilderBase<QueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public QueryOverOrderBuilder(QueryOver<TRoot, TSubType> root, Expression<Func<TSubType, object>> path)
            : base(root, path)
        {
        }

        public QueryOverOrderBuilder(QueryOver<TRoot, TSubType> root, Expression<Func<object>> path, bool isAlias)
            : base(root, path, isAlias)
        {
        }
    }

    public class IQueryOverOrderBuilder<TRoot, TSubType> :
        QueryOverOrderBuilderBase<IQueryOver<TRoot, TSubType>, TRoot, TSubType>
    {
        public IQueryOverOrderBuilder(IQueryOver<TRoot, TSubType> root, Expression<Func<TSubType, object>> path)
            : base(root, path)
        {
        }

        public IQueryOverOrderBuilder(IQueryOver<TRoot, TSubType> root, Expression<Func<object>> path, bool isAlias)
            : base(root, path, isAlias)
        {
        }
    }

    public class QueryOverOrderBuilderBase<TReturn, TRoot, TSubType> where TReturn : IQueryOver<TRoot, TSubType>
    {
        protected TReturn root;
        protected LambdaExpression path;
        protected bool isAlias;

        protected QueryOverOrderBuilderBase(TReturn root, Expression<Func<TSubType, object>> path)
        {
            this.root = root;
            this.path = path;
            this.isAlias = false;
        }

        protected QueryOverOrderBuilderBase(TReturn root, Expression<Func<object>> path, bool isAlias)
        {
            this.root = root;
            this.path = path;
            this.isAlias = isAlias;
        }

        public TReturn Asc
        {
            get
            {
                this.root.UnderlyingCriteria.AddOrder(ExpressionProcessor.ProcessOrder(path, Order.Asc, isAlias));
                return this.root;
            }
        }

        public TReturn Desc
        {
            get
            {
                this.root.UnderlyingCriteria.AddOrder(ExpressionProcessor.ProcessOrder(path, Order.Desc, isAlias));
                return this.root;
            }
        }
    }
}