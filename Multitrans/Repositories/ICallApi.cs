

using System.IO;
using System.Threading.Tasks;
using System.Web;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
  public interface ICAllApi
    {
        Reponse CallBackendPost(string url, object obj, string tokenKey);
        Reponse CallBackendGet(string url, string tokenKey);
      
    }
}
