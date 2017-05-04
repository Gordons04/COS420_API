using ResourceServer.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace ResourceServer.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/protected")]
    public class UserProtectedController : ApiController
    {
        IHandler _handler;
        public UserProtectedController(IHandler handler)
        {
            _handler = handler;
        }
        
        [Route("")]
        public IEnumerable<object> Get()
        {
            var identity = User.Identity as ClaimsIdentity;

            return identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        [HttpPost]
        [Route("GetTotalPoints")]
        public IHttpActionResult GetTotalPoints([FromBody] dynamic body)
        {
            try
            {
                var userName = body.UserName;

                var result = _handler.GetTotalPoints(userName);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetTotalPointsOfAll")]
        public IHttpActionResult GetTotalPointsOfAll([FromBody] dynamic body)
        {
            try
            {               

                var result = _handler.GetTotalPointsOfAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public IHttpActionResult UpdateProfile([FromBody]dynamic body)
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

                var result = _handler.UpdateUser(fName, lName, mobileNo, city, zip, email, userName);

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
