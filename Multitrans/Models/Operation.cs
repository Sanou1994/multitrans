using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class Operation
    {
		public long? id { get; set; }
		public long? structureID { get; set; }
		public bool? status { get; set; }
		public string libelle { get; set; }
        public string sens { get; set; }

    }
}
