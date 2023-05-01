using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class AgenceRepositoryImpl : IAgenceRepository
    {
        private ICAllApi _callApi;
        public AgenceRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IAgenceRepository.AjouterAgence(AgenceDtoRequest agence, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/agences/add", agence, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            } 
            return reponse;
        }
       
        Reponse IAgenceRepository.bloquerAgence(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/agences/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IAgenceRepository.ListeAgence(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/agences/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse IAgenceRepository.ChercherAgence(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/agences/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }
    }
}




