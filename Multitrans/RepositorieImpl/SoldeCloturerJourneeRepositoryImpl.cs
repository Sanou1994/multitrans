using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class SoldeCloturerJourneeRepositoryImpl : ISoldeCloturerJourneeRepository
    {
        private ICAllApi _callApi;
        public SoldeCloturerJourneeRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse ISoldeCloturerJourneeRepository.AjouterSoldeCloturerJournee(SoldeCloturerJourneeDto SoldeCloturerJournee, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/soldecloturerjournees/add", SoldeCloturerJournee, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer   ";
            } 
            return reponse;
        }
       
        Reponse ISoldeCloturerJourneeRepository.bloquerSoldeCloturerJournee(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldecloturerjournees/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse ISoldeCloturerJourneeRepository.ListeSoldeCloturerJournee(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldecloturerjournees/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse ISoldeCloturerJourneeRepository.ChercherSoldeCloturerJournee(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldecloturerjournees/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        public Reponse ListeSoldeCloturerJournee(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                var url = $"/soldecloturerjournees?id={id}&agenceID={agenceID}&caissierID={caissierID}&operateurID={operateurID}&operationID={operationID}&Etat={Etat}&DateDebut={DateDebut}&DateFin={DateFin}&sortBy={sortBy}&pageNo={pageNo}&pageSize={pageSize}";

                reponse = _callApi.CallBackendGet(url, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        public Reponse ListeSoldeCloturerJournee(long? id, long? agenceID, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/soldecloturerjournees/caissier/{id}/agence/{agenceID}", tokenKey);


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




