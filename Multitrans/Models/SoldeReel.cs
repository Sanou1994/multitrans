using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class SoldeReel
    {
		public long? id { get; set; }
		public long? structureID { get; set; }
		public double caisse { get; set; }
		public List<SoldeReelItem> SoldeReelItems { get; set; }
		public long? date_creation { get; set; }
		public long? agenceID { get; set; }

		public long date { get; set; }
		public long idU { get; set; }
		public bool status { get; set; }
        public long soldeDebuterJourneeID { get; set; }




    }
    public class SoldeReelDto
    {
        public long? structureID { get; set; }
        public double caisse { get; set; }
        public List<SoldeReelItemData> SoldeReelDtoRequests { get; set; }
		public long? agenceID { get; set; }

		public long idU { get; set; }

    }
}
