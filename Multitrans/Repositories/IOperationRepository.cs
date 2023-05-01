
using Multitrans.Models;

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IOperationRepository
    {
        Reponse ListeOperation(long? id, string tokenKey);
        Reponse ChercherOperation(long? id, string type, string tokenKey);
        Reponse AjouterOperation(Operation operation, string tokenKey);
       
        Reponse bloquerOperation(long? id, string tokenKey);
    }
}
