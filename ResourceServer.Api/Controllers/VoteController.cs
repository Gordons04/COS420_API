﻿using ResourceServer.Api.Interfaces;
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
        public IHttpActionResult RegisterVote([FromBody] dynamic body)
        {
            try
            {
                int orgId = (int)body.OrgId;
                string userName = (string)body.UserName;

                return Ok(handler.VoteForOrg(orgId, userName));

            }
            catch (Exception ex)
            {

                return InternalServerError();
            }
        }

      
        [HttpPost]
        [Route("GetCountyRegions")]
        public IHttpActionResult GetCountyRegions([FromBody] dynamic body)
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
                string userName = body.UserName;



                var list = handler.GetCharities(county, userName);

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
        //[HttpPost]
        //[Route("GetInterest")]
        //public IHttpActionResult GetInterest([FromBody] dynamic body)
        //{
        //    try
        //    {

        //        //int interest = body.Interest;

        //        //var list = handler.GetInterest(interest);

        //        //return Ok(list);

        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError();
        //    }
        //}
        //[HttpPost]
        //[Route("GetCountyByInterest")]
        //public IHttpActionResult GetCountiesByInterest([FromBody] dynamic body)
        //{
        //    try
        //    {
        //        int interest = body.Interest;

        //        var list = handler.GetCountiesByInterest(interest);

        //        return Ok(list);

        //    }
        //    catch (Exception ex)
        //    {
        //        return InternalServerError();
        //    }

        //}
    }
}
