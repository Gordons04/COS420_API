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
        public IHttpActionResult GetTriviaQuestion([FromBody] dynamic body)
        {
            try
            {
                var index = (int)body.Index;

                var question = handler.GetTriviaQuestion(index);

                return Ok(question);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetTriviaQuestions")]
        public IHttpActionResult GetTriviaQuestions()
        {
            try
            {
                var questions = handler.GetTriviaQuestions();

                return Ok(questions);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetAnswer")]
        public IHttpActionResult GetAnswer([FromBody] dynamic body)
        {
            try
            {
                var qid = (int)body.Qid;

                var listOfAnswer = handler.GetAnswer(qid);

                return Ok(listOfAnswer);
            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

    }
}
