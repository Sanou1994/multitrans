using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class SoldeDebuterJourneeRepositoryImpl : ISoldeDebuterJourneeRepository
    {
        private ICAllApi _callApi;
        public SoldeDebuterJourneeRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }
		Reponse ISoldeDebuterJourneeRepository.ListeSoldeDebuterJournee(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				var url = $"/soldedebuterjournees?id={id}&agenceID={agenceID}&caissierID={caissierID}&operateurID={operateurID}&operationID={operationID}&Etat={Etat}&DateDebut={DateDebut}&DateFin={DateFin}&sortBy={sortBy}&pageNo={pageNo}&pageSize={pageSize}";

				reponse = _callApi.CallBackendGet(url, tokenKey);

			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

		Reponse ISoldeDebuterJourneeRepository.AjouterSoldeDebuterJournee(SoldeDebuterJourneeDto SoldeDebuterJournee, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/soldedebuterjournees/add", SoldeDebuterJournee, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer   ";
            } 
            return reponse;
        }
       
        Reponse ISoldeDebuterJourneeRepository.bloquerSoldeDebuterJournee(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldedebuterjournees/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse ISoldeDebuterJourneeRepository.ListeSoldeDebuterJournee(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldedebuterjournees/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse ISoldeDebuterJourneeRepository.ChercherSoldeDebuterJournee(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldedebuterjournees/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        public Reponse ListeSoldeDebuterJournee(long? id, long? agenceID, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldedebuterjournees/caissier/{id}/agence/{agenceID}", tokenKey);


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




