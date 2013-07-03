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
    }
}