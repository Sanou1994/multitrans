using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class UserRepositoryImpl : IUserRepository
    {
        private ICAllApi _callApi;
        public UserRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

        Reponse IUserRepository.AjouterUser(User User, string tokenKey)
        {
            Reponse reponse = new Reponse();

           try
            {
               reponse = _callApi.CallBackendPost("/user/register", User, tokenKey);

            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Impossible de créer ce compte  ";
            } 
            return reponse;
        }
       
        Reponse IUserRepository.bloquerUser(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/users/delete/{id}", tokenKey);


            }
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

        Reponse IUserRepository.ListeUser(long? id, string tokenKey)
        {

            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/users/structure/{id}", tokenKey);
				reponse.code = 200;
			}
            catch (Exception)
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;

         }

        
        Reponse IUserRepository.ChercherUser(long? id, string tokenKey)
        {
            Reponse reponse = new Reponse();
            try
            {
                reponse = _callApi.CallBackendGet($"/users/{id}", tokenKey);


            }
            catch 
            {
                reponse.code = 500;
                reponse.message = "Une erreur interne coté client";
            }

            return reponse;
        }

		public Reponse ListeUserByAgence(long? id, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/users/agence/{id}", tokenKey);
				reponse.code = 200;
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




