using System;
using System.Runtime.Serialization;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// An <see cref="ICriterion"/> that represents empty association constraint.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class IsEmptyExpression : AbstractEmptinessExpression
    {
        public IsEmptyExpression(string propertyName)
            : base(propertyName)
        {
        }

        protected override bool ExcludeEmpty
        {
            get { return false; }
        }

        public override IProjection[] GetProjections()
        {
            return null;
        }
    }
}