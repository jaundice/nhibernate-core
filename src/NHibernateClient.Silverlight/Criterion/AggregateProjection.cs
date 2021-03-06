using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//using NHibernate.SqlCommand;
//using NHibernate.Type;
//using NHibernate.Util;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// An Aggregation
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class AggregateProjection : SimpleProjection
    {
        [DataMember(Name="Aggregate")] protected internal string aggregate;
        [DataMember(Name="Projection")] protected internal IProjection projection;
        [DataMember(Name="PropertyName")] protected internal string propertyName;

        protected internal AggregateProjection(string aggregate, string propertyName)
        {
            this.propertyName = propertyName;
            this.aggregate = aggregate;
        }

        protected internal AggregateProjection(string aggregate, IProjection projection)
        {
            this.aggregate = aggregate;
            this.projection = projection;
        }

        public override bool IsAggregate
        {
            get { return true; }
        }

        public override string ToString()
        {
            return aggregate + "(" + (projection != null ? projection.ToString() : propertyName) + ')';
        }

        //public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    if (projection != null)
        //    {
        //        return projection.GetTypes(criteria, criteriaQuery);
        //    }
        //    return new IType[] {criteriaQuery.GetType(criteria, propertyName)};
        //}

        //public override SqlString ToSqlString(ICriteria criteria, int loc, ICriteriaQuery criteriaQuery,
        //                                      IDictionary<string, IFilter> enabledFilters)
        //{
        //    if (projection != null)
        //    {
        //        return
        //            new SqlString(new object[]
        //                            {
        //                                aggregate, "(",
        //                                StringHelper.RemoveAsAliasesFromSql(projection.ToSqlString(criteria, loc, criteriaQuery,
        //                                                                                           enabledFilters)), ") as y",
        //                                loc.ToString(), "_"
        //                            });
        //    }
        //    else
        //    {
        //        return
        //            new SqlString(new object[]
        //                            {aggregate, "(", criteriaQuery.GetColumn(criteria, propertyName), ") as y", loc.ToString(), "_"});
        //    }
        //}

        public override bool IsGrouped
        {
            get { return false; }
        }

        //public override SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery,
        //                                           IDictionary<string, IFilter> enabledFilters)
        //{
        //    throw new InvalidOperationException("not a grouping projection");
        //}
    }
}