
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
		public IHttpActionResult GetTwitterFeed([FromBody]dynamic body)
		{
			// Get the twitter feed
		}

	}
}
