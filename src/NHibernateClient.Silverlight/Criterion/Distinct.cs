using System;
using System.Runtime.Serialization;

//using NHibernate.SqlCommand;
//using NHibernate.Type;

namespace NHibernateClient.Criterion
{
    using System.Collections.Generic;

    //using Engine;

    [Serializable]
    [DataContract(IsReference = true)]
    public class Distinct : IProjection
    {
        [DataMember(Name = "Projection")]
        internal IProjection projection;

        public Distinct(IProjection proj)
        {
            this.projection = proj;
        }

        //public virtual SqlString ToSqlString(ICriteria criteria, int position, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    return new SqlString("distinct ")
        //        .Append(projection.ToSqlString(criteria, position, criteriaQuery,enabledFilters));
        //}

        //public virtual SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    return projection.ToGroupSqlString(criteria, criteriaQuery,enabledFilters);
        //}

        //public virtual IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return projection.GetTypes(criteria, criteriaQuery);
        //}

        //public virtual IType[] GetTypes(String alias, ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return projection.GetTypes(alias, criteria, criteriaQuery);
        //}

        public virtual string[] GetColumnAliases(int loc)
        {
            return projection.GetColumnAliases(loc);
        }

        public virtual string[] GetColumnAliases(string alias, int loc)
        {
            return projection.GetColumnAliases(alias, loc);
        }

        public virtual string[] Aliases
        {
            get { return projection.Aliases; }
        }

        public virtual bool IsGrouped
        {
            get { return projection.IsGrouped; }
        }

        public bool IsAggregate
        {
            get { return projection.IsAggregate; }
        }

        ///// <summary>
        ///// Gets the typed values for parameters in this projection
        ///// </summary>
        ///// <param name="criteria">The criteria.</param>
        ///// <param name="criteriaQuery">The criteria query.</param>
        ///// <returns></returns>
        //public TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return projection.GetTypedValues(criteria, criteriaQuery);
        //}

        public override string ToString()
        {
            return "distinct " + projection.ToString();
        }
    }
}