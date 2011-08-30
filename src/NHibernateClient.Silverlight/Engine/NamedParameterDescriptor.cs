using System;
using System.Runtime.Serialization;

//using NHibernate.Type;

namespace NHibernateClient.Engine.Query
{
    /// <summary> Descriptor regarding a named parameter. </summary>
    [Serializable]
    [DataContract(IsReference = true)]
    public class NamedParameterDescriptor
    {
        [DataMember] internal string name;
        //[DataMember] internal IType expectedType;
        [DataMember] internal int[] sourceLocations;
        [DataMember] internal bool jpaStyle;

        public NamedParameterDescriptor(string name, /*IType expectedType, */ int[] sourceLocations, bool jpaStyle)
        {
            this.name = name;
            //this.expectedType = expectedType;
            this.sourceLocations = sourceLocations;
            this.jpaStyle = jpaStyle;
        }

        public string Name
        {
            get { return name; }
        }

        //public IType ExpectedType
        //{
        //    get { return expectedType; }
        //}

        public int[] SourceLocations
        {
            get { return sourceLocations; }
        }

        /// <summary>
        /// Not supported yet (AST parse needed)
        /// </summary>
        public bool JpaStyle
        {
            get { return jpaStyle; }
        }
    }
}