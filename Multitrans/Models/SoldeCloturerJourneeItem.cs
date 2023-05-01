using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class SoldeCloturerJourneeItem
    {
		public long? id { get; set; }
		public long? operateurID { get; set; }
		public double montant { get; set; }
		public long? soldeCloturerJourneeID { get; set; }

    }
    public class SoldeCloturerJourneeItemData
    {
        public long? operateurID { get; set; }
        public double montant { get; set; }

    }
}
