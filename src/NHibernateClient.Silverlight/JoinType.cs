namespace NHibernateClient.SqlCommand
{
    /// <summary></summary>
    public enum JoinType
    {
        None = -666,
        InnerJoin = 0,
        FullJoin = 4,
        LeftOuterJoin = 1,
        RightOuterJoin = 2
    }
}