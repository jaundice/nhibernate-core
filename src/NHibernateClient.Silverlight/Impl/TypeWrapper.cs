using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace NHibernateClient.Impl
{
    [DataContract(IsReference = true)]
    public class TypeWrapper
    {
        public Type Type { get; set; }

        [DataMember]
        public string TypeName
        {
            get { return Type.AssemblyQualifiedName; }
            set { Type = Type.GetType(value); }
        }
    }
}