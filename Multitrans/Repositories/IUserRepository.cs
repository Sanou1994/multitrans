
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IUserRepository
    {
        Reponse ListeUser(long? type, string tokenKey);
        Reponse ChercherUser(long? id, string tokenKey);
        Reponse AjouterUser(User user, string tokenKey);
		Reponse ListeUserByAgence(long? id, string tokenKey);

		Reponse bloquerUser(long? id, string tokenKey);
    }
}
