using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class Transaction
    {
		public long? id { get; set; }
        public long? operateurID { get; set; }
        public long? operationID { get; set; }
        public long? structureID { get; set; }
        public double commission { get; set; }
        public double frais { get; set; }
        public double taxe { get; set; }
        public double montant { get; set; }
        public double surplux { get; set; }
        public string numero { get; set; }
        public string sens { get; set; }
		public string description { get; set; }
		public string reference { get; set; }
		public string etat { get; set; }
        public long idU { get; set; }
        public long date { get; set; }
        public bool status { get; set; }
		public long agenceID { get; set; }
		public long soldeDebuterJourneeID;

	}
}
