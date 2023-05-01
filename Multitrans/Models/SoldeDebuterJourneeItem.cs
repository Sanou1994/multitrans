using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class SoldeDebuterJourneeItem
    {
		public long? id { get; set; }
		public long? operateurID { get; set; }
		public double montant { get; set; }
		public long? soldeDebuterJourneeID { get; set; }

    }
    public class SoldeDebuterJourneeItemData
    {
        public long? operateurID { get; set; }
        public double montant { get; set; }

    }
}
