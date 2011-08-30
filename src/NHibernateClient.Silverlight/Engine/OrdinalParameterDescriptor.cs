using System;
using System.Runtime.Serialization;

//using NHibernate.Type;

namespace NHibernateClient.Engine.Query
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class OrdinalParameterDescriptor
    {
        [DataMember] internal int ordinalPosition;
        //[DataMember] internal IType expectedType;
        [DataMember] internal int sourceLocation;

        public OrdinalParameterDescriptor(int ordinalPosition, /*IType expectedType, */int sourceLocation)
        {
            this.ordinalPosition = ordinalPosition;
            //this.expectedType = expectedType;
            this.sourceLocation = sourceLocation;
        }

        public int OrdinalPosition
        {
            get { return ordinalPosition; }
        }

        //public IType ExpectedType
        //{
        //    get { return expectedType; }
        //}

        public int SourceLocation
        {
            get { return sourceLocation; }
        }
    }
}