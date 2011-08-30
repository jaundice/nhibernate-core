using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//using NHibernate.Engine;
//using NHibernate.SqlCommand;
//using NHibernate.Util;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// An identifier constraint
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class IdentifierEqExpression : AbstractCriterion
    {
        [DataMember(Name = "Value")]
        internal object value;

        public IdentifierEqExpression(IProjection projection)
        {
            _projection = projection;
        }

        [DataMember(Name = "Projection")]
        internal IProjection _projection;

        public IdentifierEqExpression(object value)
        {
            this.value = value;
        }

        #region ICriterion Members

        //public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    //Implementation changed from H3.2 to use SqlString
        //    string[] columns = criteriaQuery.GetIdentifierColumns(criteria);
        //    SqlStringBuilder result = new SqlStringBuilder(4 * columns.Length + 2);
        //    if (columns.Length > 1)
        //    {
        //        result.Add(StringHelper.OpenParen);
        //    }

        //    for (int i = 0; i < columns.Length; i++)
        //    {
        //        if (i > 0)
        //        {
        //            result.Add(" and ");
        //        }

        //        result.Add(columns[i])
        //            .Add(" = ");

        //        AddValueOrProjection(criteria, criteriaQuery, enabledFilters, result);
        //    }

        //    if (columns.Length > 1)
        //    {
        //        result.Add(StringHelper.ClosedParen);
        //    }
        //    return result.ToSqlString();
        //}

        //private void AddValueOrProjection(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters, SqlStringBuilder result)
        //{
        //    if (_projection == null)
        //    {
        //        criteriaQuery.AddUsedTypedValues(GetTypedValues(criteria,criteriaQuery));
        //        result.AddParameter();
        //    }
        //    else
        //    {
        //        SqlString sql = _projection.ToSqlString(criteria, GetHashCode(),criteriaQuery, enabledFilters);
        //        result.Add(StringHelper.RemoveAsAliasesFromSql(sql));
        //    }
        //}

        //public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return new TypedValue[] {criteriaQuery.GetTypedIdentifierValue(criteria, value)};
        //}

        public override IProjection[] GetProjections()
        {
            if (_projection != null)
                return new IProjection[] { _projection };
            return null;
        }

        #endregion

        public override string ToString()
        {
            return (_projection != null ? _projection.ToString() : "ID") + " == " + value;
        }
    }
}