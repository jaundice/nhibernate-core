using System.Collections.Generic;
using NHibernateClient.Criterion;
using NHibernateClient.Impl;

namespace NHibernateClient.Analysis
{
    public static class CriteriaAnalysisEx
    {
        public static CriteriaImpl GetCriteriaImpl(this DetachedCriteria crit)
        {
            return crit.impl;
        }

        public static CriteriaImpl GetCriteriaImpl(this QueryOver crit)
        {
            return crit.impl;
        }

        public static IEnumerable<ICriterion> GetCriterion(this Junction junction)
        {
            return junction.criteria;
        }

        public static SubQueryDescriptor GetSubQueryDescription(this SubqueryExpression expr)
        {
            var a = new SubQueryDescriptor
                    {
                        CriteriaImpl = expr.criteriaImpl,
                        Op = expr.op,
                        PrefixOp = expr.prefixOp,
                        Quantifier = expr.quantifier
                    };
            return a;
        }

        public static PropertySubQueryDescriptor GetPropertySubQueryDescription(this PropertySubqueryExpression expr)
        {
            SubQueryDescriptor b = expr.GetSubQueryDescription();
            return new PropertySubQueryDescriptor(b)
                   {
                       PropertyName = expr.propertyName
                   };
        }

        public static SimpleSubQueryDescriptor GetSimpleSubQueryDescription(this SimpleSubqueryExpression expr)
        {
            SubQueryDescriptor b = expr.GetSubQueryDescription();
            return new SimpleSubQueryDescriptor(b)
                   {
                       Value = expr.value
                   };
        }
    }

    public class PropertySubQueryDescriptor : SubQueryDescriptor
    {
        public PropertySubQueryDescriptor(SubQueryDescriptor sqdBase)
            : base(sqdBase) {}

        public string PropertyName { get; set; }
    }


    public class SimpleSubQueryDescriptor : SubQueryDescriptor
    {
        public SimpleSubQueryDescriptor(SubQueryDescriptor sqdBase)
            : base(sqdBase) {}

        public object Value { get; set; }
    }

    public class SubQueryDescriptor
    {
        public SubQueryDescriptor() {}

        public SubQueryDescriptor(SubQueryDescriptor sqdBase)
        {
            CriteriaImpl = sqdBase.CriteriaImpl;
            Op = sqdBase.Op;
            PrefixOp = sqdBase.PrefixOp;
            Quantifier = sqdBase.Quantifier;
        }

        public CriteriaImpl CriteriaImpl { get; set; }

        public string Op { get; set; }

        public bool PrefixOp { get; set; }

        public string Quantifier { get; set; }
    }
}