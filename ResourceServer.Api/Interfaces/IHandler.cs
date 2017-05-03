using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ResourceServer.Api.Interfaces
{
    public interface IHandler
    {
        object Login(String username, String password);
        string GetQuery(String q);
        object RegisterUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName, string password);
        object UpdateUser(string fName, string lName, string mobileNo, string city, string zip, string email,  string userName);
        object GetAllVotes();
        object IsUserNameAvailable(string userName);
        object GetTotalVotes();
        object GetRewards();
        object ChangePassword(string userName, string newPassword);
        object GetTriviaQuestion();
        object GetCountyRegions();
        object GetCharities(string county, string userName);
        object GetCounties(string region);
        object VoteForOrg(int orgId, string userName);
        IHttpActionResult RegisterVote(List<int> listOfOrgs);
        IHttpActionResult GetTwitterFeed([FromBody]dynamic body);
        object GetAnswer();
        object GetInterest(string interests_id);
        object GetCountyByInterest();
    }
}