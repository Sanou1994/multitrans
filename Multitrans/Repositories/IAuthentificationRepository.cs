

using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface IAuthentificationRepository
    {
        Reponse Seconnecter(string phone, string pwd);
       
        
    }
}
