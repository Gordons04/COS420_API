using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace ResourceServer.Api.Util
{
    public class Authentication
    {
        private String URI;
        private String client_id;

        public Authentication()
        {
            URI = System.Web.Configuration.WebConfigurationManager.AppSettings["authApi"].ToString();
            client_id = "099153c2625149bc8ecb3e85e03f0022";
        }

        public String getToken(String username, String password)
        {

            using (var client = new System.Net.WebClient())
            {
                // prepare headers
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                // prepare collection (body) with proper fields
                var loginData = new NameValueCollection{

                    { "client_id", client_id },
                    { "grant_type", "password" },
                    { "username", username },
                    { "password", password },
                };

                // authenticate -- UploadValues handles construction of request body
                try
                {
                    byte[] response = client.UploadValues(URI, "POST", loginData);
                    string s = client.Encoding.GetString(response);
                    return s;
                }
                catch (WebException wex)
                {
                    Console.WriteLine("Error");
                    return null;
                }
            }
        }



    }
}