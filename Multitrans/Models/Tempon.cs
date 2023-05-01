using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static Unity.Storage.RegistrationSet;

namespace Multitrans.Models
{
    public class Tempon
    {
        public class DashboadData
        {

			public SoldeReel soldeReelActuel { get; set; }
			public List<Depense_extrat>  depenseExtrat { get; set; }
			public bool soldeDebuterJourneeTermine { get; set; }
            public List<Operateur> operateurs { get; set; }
            public List<Operation> operations { get; set; }
			public List<Transaction> transactions { get; set; }
			



		}
		public class StatistiqueRapportData
		{
			public List<Transaction> transactions { get; set; }
			public List<Operation> operations { get; set; }
			public List<Operateur> operateurs { get; set; }
			public List<Agence> agences { get; set; }
			public List<User> users { get; set; }
			public List<SoldeCloturerJournee> soldeCloturerJournees { get; set; }
            public List<Depense_extrat> depenseExtrats { get; set; }

            public List<SoldeDebuterJournee> soldeDebuterJournees { get; set; }

			public string message { get; set; }
			public int? pageNo { get; set; }
			public int? pageSize { get; set; }

			public long? agenceID { get; set; }

			public long? caissierID { get; set; }

			public long? operateurID { get; set; }
			public long? operationID { get; set; }
            public string type { get; set; }

            public string Etat { get; set; }
			public string sortBy { get; set; }
			public long? DateDebut { get; set; }
			public long? DateFin { get; set; }


		}
        public class RapportDataList
        {


            public long? id { get; set; }

            public double? frais { get; set; }
            public double? commission { get; set; }
            public double? dec { get; set; }
            public double? enc { get; set; }

            public double? surplux { get; set; }
            public double? taxe { get; set; }
            public double? depense { get; set; }
            public double? extrat { get; set; }
            public double? benefice { get; set; }

            public long? agenceID { get; set; }

            public long? caissierID { get; set; }        

           
            public long? Date { get; set; }


        }

        public class RapportData
        {
           

            public int? pageNo { get; set; }
            public int? pageSize { get; set; }

            public List<RapportDataList> rapportDataList { get; set; }
            public long? agenceID { get; set; }
            public List<Agence> agences { get; set; }
            public List<User> users { get; set; }
            public List<Operation> operations { get; set; }
            public List<SoldeDebuterJournee> DebuterJourneeRapports { get; set; }
            public List<SoldeCloturerJournee> CloturerJourneeRapports { get; set; }
            public List<SoldeReel> SoldeReelRapports { get; set; }
            public List<Operateur> operateurs { get; set; }
            public long? caissierID { get; set; }

            public long? operateurID { get; set; }
            public long? operationID { get; set; }
            public string type { get; set; }

            public string Etat { get; set; }
            public string sortBy { get; set; }
            public long? DateDebut { get; set; }
            public long? DateFin { get; set; }


        }
        public class StatistiqueData
		{
			public List<Transaction> transactions { get; set; }
			public List<Operation> operations { get; set; }
			public List<Operateur> operateurs { get; set; }
			public List<Agence> agences { get; set; }
			public List<User> users { get; set; }
			public string message { get; set; }
			public int? pageNo { get; set; }
			public int? pageSize { get; set; }

			public long? agenceID { get; set; }

			public long? caissierID { get; set; }

			public long? operateurID { get; set; }
			public long? operationID { get; set; }

			public string Etat { get; set; }
			public string sortBy { get; set; }
			public long? DateDebut { get; set; }
			public long? DateFin { get; set; }
			

		}

		public class Reponse
        {
            public string message { get; set; }
            public int code { get; set; }
            public dynamic result { get; set; }
        }
        public class Login
        {
            public string telephone { get; set; }
            public string password { get; set; }



        }
    }
    public static class Utils
        {
            public static T ToObject<T>(this Object fromObject)
            {
                return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(fromObject));
            }


            public static List<T> ToObjectList<T>(this Object fromObject)
            {
                return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(fromObject));
            }
        }



    
}