
using ResourceServer.Api.Interfaces;
using ResourceServer.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceServer.Api.Controllers
{
    
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        IHandler _handler;
        public UserController(IHandler handler)
        {
            _handler = handler;
        }
        /// <summary>
        /// Login API
        /// </summary>
        /// <param name="body">{"UserName":"user",	"Password":"pass#word1"} </param>
        /// <returns>True or False</returns>
        [HttpPost]
        [Route("Login")]
        public IHttpActionResult Login([FromBody]dynamic body)
        {
            try
            {             
                string userName = body.UserName;
                string password = body.Password;
                bool isFalse;
                
                var response = _handler.Login(userName, password);

                bool.TryParse(response.ToString(), out isFalse);

                if (isFalse)
                {
                    return Unauthorized();
                }

                return Ok(response);         
                
            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult RegisterUser([FromBody]dynamic body)
        {
            try
            {                
                string userName = body.UserName;
                string password = body.Password;
                string city = body.City;
                string mobileNo = body.MobileNo;
                string zip = body.Zip;
                string fName = body.FName;
                string lName = body.LName;
                string email = body.Email;

                bool isValidResponse;
                var response = _handler.IsUserNameAvailable(userName);
                bool.TryParse(response.ToString(), out isValidResponse);

                if (isValidResponse)
                {
                    if (!(bool)response)
                    {
                        return Ok("Username not available.");
                    }
                }
                else
                {
                    return InternalServerError((Exception)response);
                }

                var result = _handler.RegisterUser(fName,lName,mobileNo,city,zip,email,userName,password);

                if (!(bool)result)
                {
                    return InternalServerError();
                }

                return Ok(result);

            }
            catch (Exception ex)
            {

                return BadRequest();
            }
        }


    }
}
