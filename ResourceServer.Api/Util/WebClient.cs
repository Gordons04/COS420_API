using ResourceServer.Api.Utils;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer.Api.Util
{
    public class WebClient
    {
        static DataAccess dataAccess;
        internal static T Get<T>(string action, Dictionary<string, string> params_) where T : new()
        {
            try
            {
                var client = new RestClient("http://authservercos420.azurewebsites.net");
                dataAccess = new DataAccess();
               

                var request = new RestRequest(action, Method.GET);

                foreach (var param in params_)
                {
                    request.AddParameter(param.Key, param.Value);
                }

          
                var response = client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != "null")
                {
                    return response.Data;
                }
                else
                {
                    return new T();
                }


            }
            catch (Exception ex)
            {
                return new T();
            }
        }


        internal static string Get(string action, Dictionary<string, string> params_)
        {
            try
            {
                dataAccess = new DataAccess();
                var client = new RestClient("http://authservercos420.azurewebsites.net");

                var request = new RestRequest(action, Method.GET);

                foreach (var param in params_)
                {
                    request.AddParameter(param.Key, param.Value);
                }

            


                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != "null")
                {
                    return response.Content;
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                return "";
            }
        }

        internal static string Get(string uri, string action, Dictionary<string, string> params_)
        {
            try
            {
                dataAccess = new DataAccess();
                var client = new RestClient(uri);

                var request = new RestRequest(action, Method.GET);

                foreach (var param in params_)
                {
                    request.AddParameter(param.Key, param.Value);
                }

            


                var response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != "null")
                {
                    return response.Content;
                }
                else
                {
                    return "";
                }


            }
            catch (Exception ex)
            {
                return "";
            }
        }
        internal static T Post<T>(string action, Dictionary<string, object> params_, Dictionary<string,string> body, string contentType) where T : new()
        {
            try
            {
                dataAccess = new DataAccess();
                var client = new RestClient("http://authservercos420.azurewebsites.net");
         
                var request = new RestRequest(action, Method.POST);

                foreach (var param in params_)
                {
                    request.AddParameter(param.Key, param.Value);
                }

               

                request.AddHeader("Accept", "application/json");
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                var response = client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != "null")
                {
                    return response.Data;
                }
                else
                {
                    return new T();
                }


            }
            catch (Exception ex)
            {
                return new T();
            }
        }

        internal static T Post<T>(string action, string body) where T : new()
        {
            try
            {
                dataAccess = new DataAccess();
                //var client = new RestClient("http://caselawapi3.azurewebsites.net/api/caselaw");
                var client = new RestClient("http://localhost:29863/api/caselaw");
                var request = new RestRequest(action, Method.POST);


                request.AddParameter("text/json", body, ParameterType.RequestBody);              


                var response = client.Execute<T>(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != "null")
                {
                    return response.Data;
                }
                else
                {
                    return new T();
                }


            }
            catch (Exception ex)
            {
                return new T();
            }
        }
    }
}