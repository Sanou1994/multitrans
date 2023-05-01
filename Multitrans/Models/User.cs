using System;
using System.Collections.Generic;

namespace Multitrans.Models
{
   
   public  class User
    {
		public long? id { get; set; }
		public long? agenceID { get; set; }
		public string prenom { get; set; }
		public string nom { get; set; }
		public long? structureID { get; set; }
		public string email { get; set; }
		public bool status { get; set; }
		public string telephone { get; set; }
		public string monToken { get; set; }
		public string login { get; set; }
		public string password { get; set; }
		public string role { get; set; }
		public string pseudo { get; set; }
		public long? dateCreation { get; set; }       
       

    }
}
