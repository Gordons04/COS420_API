using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResourceServer.Api.Interfaces;


namespace ResourceServer.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/trivia")]
    public class TriviaController : ApiController
    {
        IHandler handler;
        
        public TriviaController(IHandler _handler)
        {
            handler = _handler;

        }
        [HttpPost]
        [Route("GetTrivia")]
        public IHttpActionResult GetTriviaQuestion()
        {
            try
            {
                var listOfTriviaQuestion = handler.GetTriviaQuestion();

                return Ok(listOfTriviaQuestion);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetAnswer")]
        public IHttpActionResult GetAnswer()
        {
            try
            {
                var listOfAnswer = handler.GetAnswer();

                return Ok(listOfAnswer);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

    }
}
