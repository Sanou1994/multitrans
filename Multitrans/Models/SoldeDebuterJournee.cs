using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class SoldeDebuterJournee
    {
		public long? id { get; set; }
		public long? structureID { get; set; }
		public double caisse { get; set; }
		public List<SoldeDebuterJourneeItem> soldeDebuterJourneeItems { get; set; }
		public long? agenceID { get; set; }

		public long date { get; set; }
		public long idU { get; set; }
		public bool status { get; set; }

       

        
    }
    public class SoldeDebuterJourneeDto
    {
        public long? structureID { get; set; }
        public double caisse { get; set; }
        public List<SoldeDebuterJourneeItemData> soldeDebuterJourneeDtoRequests { get; set; }
		public long? agenceID { get; set; }
		public long? id { get; set; }

		public long idU { get; set; }

    }
}
