
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface ISoldeCloturerJourneeRepository
    {
        Reponse ListeSoldeCloturerJournee(long? id, string tokenKey);
        Reponse ChercherSoldeCloturerJournee(long? id, string type, string tokenKey);
        Reponse AjouterSoldeCloturerJournee(SoldeCloturerJourneeDto agence, string tokenKey);
        Reponse ListeSoldeCloturerJournee(long? id, long? agenceID, long? caissierID, long? operateurID, long? operationID, string Etat, long? DateDebut, long? DateFin, int? pageNo, int? pageSize, string sortBy, string tokenKey);
        Reponse ListeSoldeCloturerJournee(long? id, long? agenceID, string tokenKey);

        Reponse bloquerSoldeCloturerJournee(long? id, string tokenKey);
    }
}
