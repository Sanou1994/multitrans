using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class OperationRepositoryImpl : IOperationRepository
    {
        private ICAllApi _callApi;
        public OperationRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IOperationRepository.AjouterOperation(Operation Operation, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/operations/add", Operation, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer cette opération  ";
            } 
            return reponse;
        }
       
        Reponse IOperationRepository.bloquerOperation(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operations/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IOperationRepository.ListeOperation(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operations/structure/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse IOperationRepository.ChercherOperation(long? id, string type, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/operations/{id}", tokenKey);


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




