
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface ITransactionRepository
    {
        Reponse ListeTransactionCaissier(long? id, long? agenceID, string tokenKey);
		Reponse ListeTransactionCaissierEncours(long? id, long? agenceID, string tokenKey);
		Reponse ListeTransactions(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey);
		Reponse ListeTransactions(long? id, string tokenKey);
        Reponse ListeTransactionRapports(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, string sortBy, string tokenKey);

        Reponse ChercherTransaction(long? id, string type, string tokenKey);
        Reponse AjouterTransaction(Transaction transaction, string tokenKey);
       
        Reponse bloquerTransaction(long? id, string tokenKey);
    }
}
