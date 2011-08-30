using System;
using System.Runtime.Serialization;

namespace NHibernateClient.Criterion
{
    /// <summary>
    /// An <see cref="ICriterion"/> that represents non-empty association constraint.
    /// </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class IsNotEmptyExpression : AbstractEmptinessExpression
    {
        public IsNotEmptyExpression(string propertyName)
            : base(propertyName)
        {
        }

        protected override bool ExcludeEmpty
        {
            get { return true; }
        }

        public override IProjection[] GetProjections()
        {
            return null;
        }
    }
}