using Multitrans.Models;
using System;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class SoldeReelRepositoryImpl : ISoldeReelRepository
    {
        private ICAllApi _callApi;
        public SoldeReelRepositoryImpl(ICAllApi callApi)
        {
            _callApi = callApi;

        }

       
		public Reponse soldeReelActuel(long? id, long? agenceID, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/soldereels/caissier/{id}/agence/{agenceID}", tokenKey);

			}
			catch (Exception)
			{
				reponse.code = 500;
				reponse.message = "Une erreur interne coté client";
			}

			return reponse;
		}

		public Reponse soldeReels(long? structureID, string tokenKey)
		{
			Reponse reponse = new Reponse();
			try
			{
				reponse = _callApi.CallBackendGet($"/soldereels/structure/{structureID}", tokenKey);

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




