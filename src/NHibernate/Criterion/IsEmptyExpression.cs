using System;

namespace NHibernate.Criterion
{
	/// <summary>
	/// An <see cref="ICriterion"/> that represents empty association constraint.
	/// </summary>
	[Serializable]
	public class IsEmptyExpression : AbstractEmptinessExpression
	{
		public IsEmptyExpression(string propertyName)
			: base(propertyName)
		{
		}

        internal IsEmptyExpression():base()
        {
            //jd: only used when building up from deserialized NHibernateClient objects
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
