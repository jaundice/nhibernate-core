using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

//using NHibernate.Engine;
//using NHibernate.SqlCommand;
//using NHibernate.Util;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// The base class for an <see cref="ICriterion"/> that compares a single Property
    /// to a value.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class SimpleExpression : AbstractCriterion
    {
        [DataMember(Name = "Projection")]
        internal readonly IProjection _projection;
        [DataMember(Name = "PropertyName")]
        internal readonly string propertyName;
        [DataMember(Name = "Value")]
        internal object value;
        [DataMember(Name = "IgnoreCase")]
        internal bool ignoreCase;
        [DataMember(Name = "Op")]
        internal readonly string op;

        protected internal SimpleExpression(IProjection projection, object value, string op)
        {
            _projection = projection;
            this.value = value;
            this.op = op;
        }

        /// <summary>
        /// Initialize a new instance of the <see cref="SimpleExpression" /> class for a named
        /// Property and its value.
        /// </summary>
        /// <param name="propertyName">The name of the Property in the class.</param>
        /// <param name="value">The value for the Property.</param>
        /// <param name="op">The SQL operation.</param>
        public SimpleExpression(string propertyName, object value, string op)
        {
            this.propertyName = propertyName;
            this.value = value;
            this.op = op;
        }

        public SimpleExpression(string propertyName, object value, string op, bool ignoreCase)
            : this(propertyName, value, op)
        {
            this.ignoreCase = ignoreCase;
        }

        public SimpleExpression IgnoreCase()
        {
            ignoreCase = true;
            return this;
        }

        /// <summary>
        /// Gets the named Property for the Expression.
        /// </summary>
        /// <value>A string that is the name of the Property.</value>
        public string PropertyName
        {
            get { return propertyName; }
        }

        /// <summary>
        /// Gets the Value for the Expression.
        /// </summary>
        /// <value>An object that is the value for the Expression.</value>
        public object Value
        {
            get { return value; }
        }

        ///// <summary>
        ///// Converts the SimpleExpression to a <see cref="SqlString"/>.
        ///// </summary>
        ///// <returns>A SqlString that contains a valid Sql fragment.</returns>
        //public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery, IDictionary<string, IFilter> enabledFilters)
        //{
        //    SqlString[] columnNames =
        //        CriterionUtil.GetColumnNamesForSimpleExpression(propertyName, _projection, criteriaQuery, criteria, enabledFilters,
        //                                                         this, value);

        //    criteriaQuery.AddUsedTypedValues(GetTypedValues(criteria, criteriaQuery));
        //    if (ignoreCase)
        //    {
        //        if (columnNames.Length != 1)
        //        {
        //            throw new HibernateException(
        //                "case insensitive expression may only be applied to single-column properties: " +
        //                propertyName);
        //        }

        //        return new SqlStringBuilder(6)
        //            .Add(criteriaQuery.Factory.Dialect.LowercaseFunction)
        //            .Add(StringHelper.OpenParen)
        //            .Add(columnNames[0])
        //            .Add(StringHelper.ClosedParen)
        //            .Add(Op)
        //            .AddParameter()
        //            .ToSqlString();
        //    }
        //    else
        //    {
        //        SqlStringBuilder sqlBuilder = new SqlStringBuilder(4 * columnNames.Length);

        //        for (int i = 0; i < columnNames.Length; i++)
        //        {
        //            if (i > 0)
        //            {
        //                sqlBuilder.Add(" and ");
        //            }

        //            sqlBuilder.Add(columnNames[i])
        //                .Add(Op)
        //                .AddParameter();
        //        }
        //        return sqlBuilder.ToSqlString();
        //    }
        //}

        //public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    object icvalue = ignoreCase ? value.ToString().ToLower() : value;
        //    return CriterionUtil.GetTypedValues(criteriaQuery, criteria, _projection,propertyName, icvalue);
        //}

        public override IProjection[] GetProjections()
        {
            if (_projection != null)
            {
                return new IProjection[] { _projection };
            }
            return null;
        }

        /// <summary></summary>
        public override string ToString()
        {
            return (_projection ?? (object)propertyName) + Op + value;
        }

        /// <summary>
        /// Get the Sql operator to use for the specific 
        /// subclass of <see cref="SimpleExpression"/>.
        /// </summary>
        protected virtual string Op
        {
            get { return op; }
        }
    }
}