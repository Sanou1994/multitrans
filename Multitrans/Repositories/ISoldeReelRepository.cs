
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface ISoldeReelRepository
    {
        Reponse soldeReelActuel(long? id, long? agenceID, string tokenKey);
		Reponse soldeReels(long? structureID, string tokenKey);

	}
}
