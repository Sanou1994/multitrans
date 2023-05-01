using Multitrans.Models;
using System;
using System.Drawing.Printing;
using System.EnterpriseServices;
using System.Globalization;
using System.Web.Services.Description;
using System.Web.Util;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class TransactionRepositoryImpl : ITransactionRepository
    {
        private ICAllApi _callApi;
        public TransactionRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse ITransactionRepository.AjouterTransaction(Transaction transaction, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/transactions/add", transaction, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer   ";
            } 
            return reponse;
        }
       
        Reponse ITransactionRepository.bloquerTransaction(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/transactions/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse ITransactionRepository.ListeTransactionCaissier(long? id, long? agenceID, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/transactions/caissier/{id}/agence/{agenceID}", tokenKey);

			}
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse ITransactionRepository.ChercherTransaction(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/transactions/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

		public Reponse ListeTransactionCaissierEncours(long? id, long? agenceID, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/transactions/caissier/{id}/agence/{agenceID}/encours", tokenKey);

			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

		public Reponse ListeTransactions(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{

                var url = $"/transactions/structure?id={id}&agenceID={agenceID}&caissierID={caissierID}&operateurID={operateurID}&operationID={operationID}&Etat={Etat}&DateDebut={DateDebut}&DateFin={DateFin}&sortBy={sortBy}&pageNo={pageNo}&pageSize={pageSize}";
				reponse = _callApi.CallBackendGet(url, tokenKey);

			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

		public Reponse ListeTransactions(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {

			
				var url = $"/transactions/structure/{id}";
                reponse = _callApi.CallBackendGet(url, tokenKey);
            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }
            return reponse;
        }

        public Reponse ListeTransactionRapports(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, string sortBy, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {

                var url = $"/transactions/rapport?id={id}&agenceID={agenceID}&caissierID={caissierID}&operateurID={operateurID}&operationID={operationID}&Etat={Etat}&DateDebut={DateDebut}&DateFin={DateFin}&sortBy={sortBy}";
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




