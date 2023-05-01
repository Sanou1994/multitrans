using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class Agence
   {
		public long? id { get; set; }
		public long? structureID { get; set; }
		public bool? status { get; set; }
		public string nom { get; set; }
		public long? date_creation { get; set; }

		public string adresse { get; set; }
		public string longitude { get; set; }
		public string latitude { get; set; }
	}
	public class AgenceDtoRequest
	{
		public long? id { get; set; }

		public long? structureID { get; set; }
		public bool? status { get; set; }
		public string nom { get; set; }
		public long? date_creation { get; set; }

		public string adresse { get; set; }
		public string longitude { get; set; }
		public string latitude { get; set; }

		public List<string>  operateurs { get; set; }
	}
}
