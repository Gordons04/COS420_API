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

        [HttpPost]
        [Route("GetVotes")]
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


        [HttpPost]
        [Route("RegisterVote")]
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

      
        [HttpPost]
        [Route("GetCountyRegions")]
        public IHttpActionResult GetCountyRegions()
        {
            try
            {
                var CountyGroupNumber = handler.GetCountyRegions();

                return Ok(CountyGroupNumber);
            
            }
            catch(Exception ex)
            {
                return InternalServerError();
            }


        }

        [HttpPost]
        [Route("GetCharities")]
        public IHttpActionResult GetCharities([FromBody] dynamic body)
        {
            try
            {

                string county = body.County;

                var list = handler.GetCharities(county);

                return Ok(list);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }


        }

        [HttpPost]
        [Route("GetCounties")]
        public IHttpActionResult GetCounties([FromBody] dynamic body)
        {
            try
            {

                string region = body.Region;

                var list = handler.GetCounties(region);

                return Ok(list);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }


        }
        [HttpPost]
        [Route("GetInterest")]
        public IHttpActionResult GetInterest([FromBody] dynamic body)
        {
            try
            {

                string interest = body.Interest;

                var list = handler.GetInterest(interest);

                return Ok(list);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        [HttpPost]
        [Route("GetCountyByInterest")]
        public IHttpActionResult GetCountyByInterest()
        {
            try
            {
                var InterestGroupNumber = handler.GetCountyByInterest();

                return Ok(InterestGroupNumber);

            }
            catch (Exception ex)
            {
                return InternalServerError();
            }

        }
    }
}
