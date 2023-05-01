using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class SoldeCloturerJournee
    {
		public long? id { get; set; }
		public long? structureID { get; set; }
		public double caisse { get; set; }
		public List<SoldeCloturerJourneeItem> soldeCloturerJourneeItems { get; set; }
		public long? date_creation { get; set; }
		public long? agenceID { get; set; }

		public long date { get; set; }
		public long idU { get; set; }
		public bool status { get; set; }
        public long? soldeDebuterJourneeID { get; set; }




    }
    public class SoldeCloturerJourneeDto
    {
        public long? structureID { get; set; }
        public double caisse { get; set; }
        public List<SoldeCloturerJourneeItemData> soldeCloturerJourneeDtoRequests { get; set; }
		public long? agenceID { get; set; }
        public long? id { get; set; }

        public long idU { get; set; }

    }
}
