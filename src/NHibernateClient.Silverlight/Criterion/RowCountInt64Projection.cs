using System;
using System.Runtime.Serialization;

//using NHibernate.Type;

namespace NHibernateClient.Criterion
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class RowCountInt64Projection : RowCountProjection
    {
        //public override IType[] GetTypes(ICriteria criteria, ICriteriaQuery criteriaQuery)
        //{
        //    return new IType[] { NHibernateUtil.Int64 };
        //}
    }
}