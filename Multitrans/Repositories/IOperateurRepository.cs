
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IOperateurRepository
    {
        Reponse ListeOperateur(long? id, string tokenKey);
		Reponse ListeOperateurCaisse(long? id, long? agenceID, string tokenKey);

		Reponse ChercherOperateur(long? id, string type, string tokenKey);
        Reponse AjouterOperateur(Operateur operateur, string tokenKey);
       
        Reponse bloquerOperateur(long? id, string tokenKey);
    }
}
