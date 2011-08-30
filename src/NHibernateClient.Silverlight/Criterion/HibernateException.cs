using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernateClient.Criterion
{
    public class HibernateException : Exception
    {
        public HibernateException(string message) : base(message)
        {
        }
    }
}