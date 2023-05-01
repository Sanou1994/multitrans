
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IDepenseExtratRepository
    {
        Reponse ListeDepenseExtrat(long? id, string tokenKey);
		Reponse ListeDepenseExtratParCaisse(long? structureID, long? agenceID, long? id, string tokenKey);
		Reponse ListeDepenseExtratParCaisseEncours(long? id, long? structureID, long? agenceID, string tokenKey);
        Reponse ListeDepenseExtrat(long? id, long? agenceID, long? caissierID,  long? DateDebut, long? DateFin, string type, int? pageNo, int? pageSize, string sortBy, string tokenKey);

        Reponse ChercherDepenseExtrat(long? id,  string tokenKey);
        Reponse AjouterDepenseExtrat(Depense_extrat depenseExtrat, string tokenKey);
       
    }
}
