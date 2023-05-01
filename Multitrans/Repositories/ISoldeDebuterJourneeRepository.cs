
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface ISoldeDebuterJourneeRepository
    {
        Reponse ListeSoldeDebuterJournee(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey);
		Reponse ChercherSoldeDebuterJournee(long? id, string type, string tokenKey);
        Reponse AjouterSoldeDebuterJournee(SoldeDebuterJourneeDto agence, string tokenKey);
		Reponse ListeSoldeDebuterJournee(long? id, string tokenKey);
        Reponse ListeSoldeDebuterJournee(long? id, long? agenceID, string tokenKey);

        Reponse bloquerSoldeDebuterJournee(long? id, string tokenKey);
    }
}
