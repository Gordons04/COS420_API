using ResourceServer.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer.Api.Utils
{
    public class DataAccess
    {
        internal object CheckUser(string userName, string password)
        {
            try
            {
                using (var context = new cos420dbEntities())
                {
                    var user = (from d in context.User_Profile.AsEnumerable()
                                where d.username == userName && PasswordStorage.VerifyPassword(password, d.password)
                                select d).SingleOrDefault();

                    if (user == null)
                    {
                        return false;
                    }
                }
               

                return true;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
               
         
    }
}