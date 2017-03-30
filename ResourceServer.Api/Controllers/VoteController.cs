using ResourceServer.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ResourceServer.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/vote")]
    public class VoteController : ApiController
    {
        IHandler handler;

        public VoteController(IHandler _handler)
        {
            handler = _handler;
        }

        [HttpGet]
        public IHttpActionResult GetAllVotes()
        {
            try
            {
                var listOfVotes = handler.GetAllVotes();

                return Ok(listOfVotes);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }
    }
}
