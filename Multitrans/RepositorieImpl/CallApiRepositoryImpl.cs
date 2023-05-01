using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Script.Serialization;
using static Multitrans.Models.Tempon;

namespace Multitrans.Repositories
{
    public class CallApiRepositoryImpl : ICAllApi
    {

        string urlBase = "http://158.69.120.240:8080";
        public Reponse   CallBackendGet(string url, string tokenKey)
        {
            Reponse reponse = new Reponse();
            string apiAddress = urlBase +url;

            HttpClient httpClient = new HttpClient();
              httpClient.DefaultRequestHeaders.Accept.Clear();
              httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
              if (tokenKey != null)
              {
                  httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

              };
              var response = httpClient.GetAsync(apiAddress);                 

                 try
                 {

                     if (((int)response.Result.StatusCode) == 200)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
                     {
                        reponse = (new JavaScriptSerializer()).Deserialize<Reponse>(response.Result.Content.ReadAsStringAsync().Result);               
                     }
                     else
                     {

                         reponse.code = ((int)response.Result.StatusCode);
                         reponse.message = "La requette a échoué !";

                     }

                    }
                    
                    catch (WebException e)
                    {
                        if (e.Status == WebExceptionStatus.ProtocolError)
                        {
                               switch (((HttpWebResponse)e.Response).StatusCode)
                            {
                                case HttpStatusCode.Unauthorized:
                                reponse.code = 401;
                                reponse.message = e.Message; break;

                                case HttpStatusCode.BadRequest:
                                reponse.code = 400;
                                reponse.message = e.Message; break;

                                 case HttpStatusCode.NotFound:
                                reponse.code = 404;
                                reponse.message = e.Message; break;

                                default:
                                reponse.code = 500;
                                reponse.message = e.Message; break;
                            }
                        }
                        else
                        {
                            reponse.code =(int) ((HttpWebResponse)e.Response).StatusCode;
                            reponse.message = e.Message;
                        }
                    }
                 return reponse;

        }

       
        public Reponse CallBackendPost(string url, object obj,string tokenKey)
        {
            Reponse reponse = new Reponse();
           try
             {
                 HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                if(tokenKey != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenKey);

                };
                var response = httpClient.PostAsync(urlBase+url, new StringContent(new JavaScriptSerializer().Serialize(obj), Encoding.UTF8, "application/json"));
                try
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;

                   if (((int)response.Result.StatusCode) == 200)
                    {

                        reponse = (new JavaScriptSerializer()).Deserialize<Reponse>(response.Result.Content.ReadAsStringAsync().Result);

              
                    }
                    else
                    {
                        reponse.code = ((int)response.Result.StatusCode);
                        reponse.message = " La requette a échoué - - " + result;

                    }
                    }
                      catch 
                      {
                          reponse.code = 500;
                          reponse.message = "problème de serveur ";

                      } 
                 }
                  catch 
                  {
                      reponse.code = 500;
                      reponse.message = "une erreur interne est survenue coté client ";

                  }  
                return reponse;



        }

       
    }
}


