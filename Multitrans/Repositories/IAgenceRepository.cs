
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IAgenceRepository
    {
        Reponse ListeAgence(long? id, string tokenKey);
        Reponse ChercherAgence(long? id, string type, string tokenKey);
        Reponse AjouterAgence(AgenceDtoRequest agence, string tokenKey);
       
        Reponse bloquerAgence(long? id, string tokenKey);
    }
}
