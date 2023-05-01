using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class DepenseExtratRepositoryImpl : IDepenseExtratRepository
    {
        private ICAllApi _callApi;
        public DepenseExtratRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IDepenseExtratRepository.AjouterDepenseExtrat(Depense_extrat DepenseExtrat, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/depenseextrats/add", DepenseExtrat, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer   ";
            } 
            return reponse;
        }
       
       

        Reponse IDepenseExtratRepository.ListeDepenseExtrat(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/depenseextrats/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse IDepenseExtratRepository.ChercherDepenseExtrat(long? id,  string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/depenseextrats/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

		public Reponse ListeDepenseExtratParCaisse(long? structureID, long? agenceID, long? id, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/depenseextrats/structure/{structureID}/agence/{agenceID}/id/{id}", tokenKey);


			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

		public Reponse ListeDepenseExtratParCaisseEncours(long? id, long? structureID, long? agenceID, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/depenseextrats/structure/{structureID}/agence/{agenceID}/id/{id}/encours", tokenKey);



			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

        public Reponse ListeDepenseExtrat(long? id, long? agenceID, long? caissierID, long? DateDebut, long? DateFin, string type, int? pageNo, int? pageSize, string sortBy, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                var url = $"/depenseextrats?id={id}&agenceID={agenceID}&caissierID={caissierID}&DateDebut={DateDebut}&DateFin={DateFin}&type={type}&sortBy={sortBy}&pageNo={pageNo}&pageSize={pageSize}";

                reponse = _callApi.CallBackendGet(url, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }
            return reponse;

        }
    }
}




