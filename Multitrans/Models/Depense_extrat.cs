using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multitrans.Models
{
	public class Depense_extrat
	{
		public long? id { get; set; }
		public string type { get; set; }
		public double montant { get; set; }

		public string description { get; set; }
		public long? agenceID { get; set; }
		public long? structureID { get; set; }
		public long? idU { get; set; }
		public long? soldeDebuterJourneeID { get; set; }
		public long? date { get; set; }


	}
}
