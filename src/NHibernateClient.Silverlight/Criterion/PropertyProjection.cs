using System;
using System.Runtime.Serialization;

//using NHibernate.SqlCommand;
//using NHibernate.Type;

namespace NHibernateClient.Criterion
{
    using System.Collections.Generic;

    /// <summary>
    /// A property value, or grouped property value
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class PropertyProjection : SimpleProjection, IPropertyProjection
    {
        [DataMember(Name = "PropertyName")]
        internal string propertyName;
        [DataMember(Name = "Grouped")]
        internal bool grouped;

        protected internal PropertyProjection(string propertyName, bool grouped)
        {
            this.propertyName = propertyName;
            this.grouped = grouped;
        }

        protected internal PropertyProjection(string propertyName)
            : this(propertyName, false)
        {
        }

        public string PropertyName
        {
            get { return propertyName; }
        }

        public override string ToString()
        {
            return propertyName;
        }

        public override bool IsGrouped
        {
            get { return grouped; }
        }

        public override bool IsAggregate
        {
            get { return false; }
        }

        //public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return new IType[] {criteriaQuery.GetType(criteria, propertyName)};
        //}

        //public override SqlString ToSqlString(ICriteria criteria, int loc, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    return new SqlString(new object[]
        //                            {
        //                                criteriaQuery.GetColumn(criteria, propertyName),
        //                                " as y",
        //                                loc.ToString(),
        //                                "_"
        //                            });
        //}

        //public override SqlString ToGroupSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    if (!grouped)
        //    {
        //        throw new InvalidOperationException("not a grouping projection");
        //    }
        //    return new SqlString(criteriaQuery.GetColumn(criteria, propertyName));
        //}
    }
}