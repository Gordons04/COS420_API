﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResourceServer.Api.Interfaces
{
    public interface IHandler
    {
        object Login(String username, String password);
        string GetQuery(String q);
        object RegisterUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName, string password);
        object UpdateUser(string fName, string lName, string mobileNo, string city, string zip, string email,  string userName);
        object IsUserNameAvailable(string userName);
        object ChangePassword(string userName, string newPassword);
        
    }
}