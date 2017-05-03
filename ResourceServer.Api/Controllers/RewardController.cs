using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResourceServer.Api.Interfaces;


namespace ResourceServer.Api.Controllers
{
    public class RewardController : ApiController
    {

        IHandler handler;

        public RewardController(IHandler _handler)
        {
            handler = _handler;

        }

        [HttpPost]
        [Route("GetRewards")]
        public IHttpActionResult GetRewards()
        {
            try
            {
                var listOfRewards = handler.GetRewards();

                return Ok(listOfRewards);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }


        
    }

}
