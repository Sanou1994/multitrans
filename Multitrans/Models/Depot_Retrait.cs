using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class Depot_Retrait
    {
		public double Montant { get; set; }
		public double Taxe { get; set; }
		public double Surplux { get; set; }
		public long? id { get; set; }

		public double Commission { get; set; }
		public double Frais { get; set; }
		public long Operation { get; set; }
		public string Sens { get; set; }
		public string Etat { get; set; }
		public string description { get; set; }

		public string Telephone { get; set; }
		public long Operateur { get; set; }





	}
}
