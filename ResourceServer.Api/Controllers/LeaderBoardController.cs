using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResourceServer.Api.Interfaces;


namespace ResourceServer.Api.Controllers
{
    public class LeaderBoardController : ApiController
    {
        IHandler handler;

        public LeaderBoardController(IHandler _handler)
        {
            handler = _handler;
        }
        [HttpPost]
        [Route("GetVotesTotal")]
        public IHttpActionResult GetTotalVotes()
        {
            try
            {
                var listOfTotalVotes = handler.GetTotalVotes();

                return Ok(listOfTotalVotes);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

    }
}
