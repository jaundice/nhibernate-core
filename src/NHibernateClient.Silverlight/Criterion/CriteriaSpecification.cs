using NHibernateClient.SqlCommand;
using NHibernateClient.Transform;

namespace NHibernateClient.Criterion
{
    public static class CriteriaSpecification
    {
        /// <summary> The alias that refers to the "root" entity of the criteria query.</summary>
        public static readonly string RootAlias = "this";

        /// <summary> Each row of results is a <see cref="System.Collections.IDictionary"/> from alias to entity instance</summary>
        public static readonly IResultTransformer AliasToEntityMap;

        /// <summary> Each row of results is an instance of the root entity</summary>
        public static readonly IResultTransformer RootEntity;

        /// <summary> Each row of results is a distinct instance of the root entity</summary>
        public static readonly IResultTransformer DistinctRootEntity;

        /// <summary> This result transformer is selected implicitly by calling <see cref="ICriteria.SetProjection"/> </summary>
        public static readonly IResultTransformer Projection;

        /// <summary> Specifies joining to an entity based on an inner join.</summary>
        public static readonly JoinType InnerJoin;

        /// <summary> Specifies joining to an entity based on a full join.</summary>
        public static readonly JoinType FullJoin;

        /// <summary> Specifies joining to an entity based on a left outer join.</summary>
        public static readonly JoinType LeftJoin;

        static CriteriaSpecification()
        {
            AliasToEntityMap = new AliasToEntityMapResultTransformer();
            RootEntity = new RootEntityResultTransformer();
            DistinctRootEntity = new DistinctRootEntityResultTransformer();
            Projection = new PassThroughResultTransformer();
            InnerJoin = JoinType.InnerJoin;
            FullJoin = JoinType.FullJoin;
            LeftJoin = JoinType.LeftOuterJoin;
        }
    }
}