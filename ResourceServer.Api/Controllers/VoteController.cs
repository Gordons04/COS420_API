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
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {

                return InternalServerError();
            }
        }


        [HttpGet]
        public IHttpActionResult RegisterVote(List<int> listOfOrgs)
        {
            try
            {
                return handler.RegisterVote(listOfOrgs);

            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [HttpGet]
        public IHttpActionResult GetCountyGroup()
        {
            try
            {
                var CountyGroupNumber = handler.GetCountyGroup();

                return Ok(CountyGroupNumber);
            
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }


        }
    }
}
