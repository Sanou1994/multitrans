using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class OperateurRepositoryImpl : IOperateurRepository
    {
        private ICAllApi _callApi;
        public OperateurRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IOperateurRepository.AjouterOperateur(Operateur Operateur, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/operateurs/add", Operateur, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            } 
            return reponse;
        }
       
        Reponse IOperateurRepository.bloquerOperateur(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operateurs/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IOperateurRepository.ListeOperateur(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operateurs/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse IOperateurRepository.ChercherOperateur(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operateurs/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

		public Reponse ListeOperateurCaisse(long? id, long? agenceID, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/operateurs/structure/{id}/agence/{agenceID}", tokenKey);
		

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




