using System;
using System.Collections.Generic;
using System.Text;
using Iesi.Collections.Generic;

namespace NHibernate.Test.NHSpecificTest.NH2386
{
    public class Organisation
    {
        //internal to TGA
        //private int organisationId;
        public virtual Guid OrganisationId { get; set; }
        private Iesi.Collections.Generic.ISet<TradingName> tradingNames;
        private Iesi.Collections.Generic.ISet<ResponsibleLegalPerson> responsibleLegalPersons;

        /// <summary>
        /// 
        /// </summary>
        

         public virtual Iesi.Collections.Generic.ISet<ResponsibleLegalPerson> ResponsibleLegalPersons {
            get {
                if (responsibleLegalPersons == null) {
                    responsibleLegalPersons = new HashedSet<ResponsibleLegalPerson>();
                }
                return responsibleLegalPersons;
            }
            protected set {
                responsibleLegalPersons = value;
               
            }
        }

        public virtual Iesi.Collections.Generic.ISet<TradingName> TradingNames {
            get {
                if (tradingNames == null) {
                    tradingNames = new HashedSet<TradingName>();
                }
                return tradingNames;
            }
            protected set {
                tradingNames = value;
               
            }
        }

         protected internal virtual byte[] RowVersion { get; protected set; }

    }

}
