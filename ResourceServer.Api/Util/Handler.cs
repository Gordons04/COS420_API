using ResourceServer.Api.Interfaces;
using ResourceServer.Api.Model;
using ResourceServer.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer.Api.Util
{
    public class Handler : IHandler
    {
        private String token_response;
        readonly cos420dbEntities dbModel;
        public Handler()
        {
            dbModel = new cos420dbEntities();
        }

        public object Login(String username, String password)
        {
            Authentication auth = new Authentication();

            token_response = auth.getToken(username, password);

            if (token_response != null)
            {
                return token_response;
            }

            return false;
        }

        public string GetQuery(String q)
        {
            throw new NotImplementedException();

        }

        //Update User Profile. This can only be accessed through protected User controller.
        public object UpdateUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName)
        {
            try
            {
                var user = (from d in dbModel.User_Profile.AsEnumerable() where d.username == userName  select d).SingleOrDefault();

                if (user != null) //Exists
                {
                    user.fName = fName;
                    user.lName = lName;
                    user.mobileNo = mobileNo;
                    user.modifiedOn = DateTime.UtcNow;
                    user.zipCode = zip;
                    user.city = city;
                    user.email = email;

                    dbModel.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Register new User. Need to call IsUserNameAvailable before calling this.
        public object RegisterUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName, string password)
        {
            try
            {

                var user = new User_Profile
                {
                    fName = fName,
                    lName = lName,
                    mobileNo = mobileNo,
                    modifiedOn = DateTime.UtcNow,
                    zipCode = zip,
                    city = city,
                    email = email,
                    username = userName,
                    password = PasswordStorage.CreateHash(password)
                };
                    dbModel.User_Profile.Add(user);
                    dbModel.SaveChanges();
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Check the Availability of the Username 
        public object IsUserNameAvailable(string userName)
        {
            try
            {
                var user = (from d in dbModel.User_Profile where d.username == userName select d).SingleOrDefault();

                if (user != null) //Exists
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {

                return ex;
            }
        }

        public object ChangePassword(string userName,  string newPassword)
        {
            var user = (from d in dbModel.User_Profile.AsEnumerable() where d.username == userName select d).SingleOrDefault();

            if (user != null)
            {
                user.password = PasswordStorage.CreateHash(newPassword);

                dbModel.SaveChanges();
            }

            return true;
        }

        public object GetAllVotes()
        {
            try
            {
                var list = (from d in dbModel.votes
                            select d).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}