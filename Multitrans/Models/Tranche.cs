using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class Tranche
    {	

		public long id { get; set; }		
		public double montant_sup { get; set; }
		public double montant_inf { get; set; }
		public float pourcentage { get; set; }

	}
}
