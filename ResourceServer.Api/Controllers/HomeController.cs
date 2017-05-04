
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

	[RoutePrefix("api/home")]
	public class HomeController : ApiController
	{
		IHandler _handler;
		public HomeController(IHandler handler)
		{
			_handler = handler;
		}

		[HttpPost]
		[Route("Feed")]
        //johnsandiego - I added the try catch to get read of the error during compile. I also added this function in IHandler. 
		public IHttpActionResult GetTwitterFeed([FromBody]dynamic body)
		{
            // Get the twitter feed
            try
            {
                return null;

            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetUserFirstName")]
        public IHttpActionResult GetUserFirstName([FromBody]dynamic body)
        {
            try
            {
                var userName = (string)body.Username;
                var realName = _handler.GetUserFirstName(userName);
                return Ok(realName);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
	}
}
