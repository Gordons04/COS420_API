﻿using ResourceServer.Api.Interfaces;
using ResourceServer.Api.Model;
using ResourceServer.Api.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ResourceServer.Api.Util
{
    public class Handler : IHandler
    {
        private String token_response;
        readonly cos420dbEntities dbModel;
        public Handler()
        {
            dbModel = new cos420dbEntities();
        }

        public object Login(String username, String password)
        {
            Authentication auth = new Authentication();

            token_response = auth.getToken(username, password);

            if (token_response != null)
            {
                return token_response;
            }

            return false;
        }

        public string GetQuery(String q)
        {
            throw new NotImplementedException();

        }




        //Update User Profile. This can only be accessed through protected User controller.
        public object UpdateUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName)
        {
            try
            {
                var user = (from d in dbModel.User_Profile.AsEnumerable() where d.username == userName select d).SingleOrDefault();

                if (user != null) //Exists
                {
                    user.fName = fName;
                    user.lName = lName;
                    user.mobileNo = mobileNo;
                    user.modifiedOn = DateTime.UtcNow;
                    user.zipCode = zip;
                    user.city = city;
                    user.email = email;

                    dbModel.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Register new User. Need to call IsUserNameAvailable before calling this.
        public object RegisterUser(string fName, string lName, string mobileNo, string city, string zip, string email, string userName, string password)
        {
            try
            {

                var user = new User_Profile
                {
                    fName = fName,
                    lName = lName,
                    mobileNo = mobileNo,
                    modifiedOn = DateTime.UtcNow,
                    zipCode = zip,
                    city = city,
                    email = email,
                    username = userName,
                    password = PasswordStorage.CreateHash(password)
                };
                dbModel.User_Profile.Add(user);
                dbModel.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Check the Availability of the Username 
        public object IsUserNameAvailable(string userName)
        {
            try
            {
                var user = (from d in dbModel.User_Profile where d.username == userName select d).SingleOrDefault();

                if (user != null) //Exists
                {
                    return false;
                }

                return true;

            }
            catch (Exception ex)
            {

                return ex;
            }
        }


        public object ChangePassword(string userName, string newPassword)
        {
            var user = (from d in dbModel.User_Profile.AsEnumerable() where d.username == userName select d).SingleOrDefault();

            if (user != null)
            {
                user.password = PasswordStorage.CreateHash(newPassword);

                dbModel.SaveChanges();
            }

            return true;
        }

        /// <summary>
        /// Returns the users real first name.
        /// </summary>
        public object GetUserFirstName(string userName)
        {
            try
            {
                //Find user in database by searching via username
                var user = (from d in dbModel.User_Profile.AsEnumerable() where d.username == userName select d).SingleOrDefault();

                if (user != null)
                    return user.fName;
                else
                    return "Null";
            }

            catch (Exception ex)
            {
                return "Null";
            }
        }

        public object GetAllVotes()
        {
            try
            {
                var list = (from d in dbModel.votes
                            select d).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public object GetCounties(string region)
        {
            try
            {
                var list = (from d in dbModel.organizations
                            where d.region == region
                            orderby d.county
                            select new { County = d.county }).Distinct().ToList<object>();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object GetCharities(string county, string userName)
        {
            try
            {
                var list = (from d in dbModel.organizations
                            from e in dbModel.organization_has_votes
                            from v in dbModel.votes
                            from u in dbModel.User_Profile
                            where d.county == county && d.id != e.organization_id && e.votes_id == v.id && v.User_Profile_uid == u.uid && u.username == userName
                            orderby d.name
                            select new { Name = d.name, Id = d.id }).Distinct().ToList<object>();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public object GetCountyRegions()
        {
            try
            {
                var list = (from d in dbModel.organizations
                            select new { Region = d.region }).Distinct().ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool RegisterVote(List<int> listOfOrgs)
        {
            try
            {
                foreach (var org in listOfOrgs)
                {
                    var vote = new vote()
                    {
                        date = DateTime.UtcNow
                    };

                    var orgHasVote = (from d in dbModel.organization_has_votes
                                      where d.organization_id == org
                                      select d
                                      ).SingleOrDefault();

                    orgHasVote.votes_id = vote.id;

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        IHttpActionResult IHandler.RegisterVote(List<int> listOfOrgs)
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult GetTwitterFeed([FromBody] dynamic body)
        {
            throw new NotImplementedException();
        }


        public object VoteForOrg(int orgId, string userName)
        {
            try
            {
                var vote = new vote()
                {
                    date = DateTime.UtcNow,
                    User_Profile_uid = GetUserId(userName)
                };

                dbModel.votes.Add(vote);
                dbModel.SaveChanges();

                var voteorg = new organization_has_votes()
                {
                    organization_id = orgId,
                    votes_id = vote.id
                };

                dbModel.organization_has_votes.Add(voteorg);
                dbModel.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private int GetUserId(string userName)
        {
            try
            {
                return (from d in dbModel.User_Profile where d.username == userName select d.uid).SingleOrDefault();
            }
            catch (Exception ex)
            {
                return -99;

            }
        }
 

        public object GetTriviaQuestion(int index)
        {
          try
            {
//                var qs = (from d in dbModel.trivias
//                            where d.idtrivia == index
//                            select d.question).SingleOrDefault();

                var qs = (from d in dbModel.trivias
                          where d.idtrivia == index
                          select new { Question = d.question,
                              Answer = d.answer,
                              Wrong1 = d.wrong1,
                              Wrong2 = d.wrong2}).SingleOrDefault();

                return qs;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object GetAnswer(int qid)
        {
            try
            {
                var ans = (from d in dbModel.trivias
                            where d.idtrivia == qid
                            select d.answer).SingleOrDefault();

                return ans;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object GetRewards()
        {
            try
            {
                var list = (from d in dbModel.rewards
                            select d).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public object GetTotalVotes()
        {
            try
            {
                var list = (from d in dbModel.rewards
                            select d).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

       

        public object GetCountiesByInterest(int id)
        {
            try
            {
                var list = (from d in dbModel.organizations
                            where d.interests_id == id
                            select new { Name = d.name, Id = d.id }).ToList<object>();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public object GetInterest(int interestID)
        {
            try
            {
                var list = (from d in dbModel.interests
                           
                            select new { id = d.id, description = d.desc}).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object GetTriviaQuestions()
        {
            try
            {
                var list = (from d in dbModel.trivias.AsEnumerable()
                           // from e in dbModel.triviapoints.AsEnumerable()
                           // where ! (d.idtrivia == e.qsid && e.Date.Value.Date == DateTime.UtcNow.Date )
                            select new { Question = d.question, Qid = d.idtrivia, Answers = new String[] {d.answer, d.wrong1, d.wrong2 }, Correct = new Random().Next(0,2), Points = d.points }).Distinct().ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object UpdatePoints(int qid, int point, string userName)
        {
            try
            {
                int userId = (from d in dbModel.User_Profile where d.username == userName select d.uid).SingleOrDefault();

                var points = new triviapoint()
                {
                    Date = DateTime.UtcNow,
                    Points = point,
                    qsid = qid,
                    UserID = userId
                };

                dbModel.triviapoints.Add(points);
                dbModel.SaveChanges();


                var totalpoints = (from d in dbModel.points where d.userid == userId select d).SingleOrDefault();

                if (totalpoints != null)
                {
                    var cur_points = (int)totalpoints.points;
                    cur_points += point;

                    totalpoints.datemodified = DateTime.UtcNow;
                    totalpoints.points = cur_points;

                    dbModel.SaveChanges();
                }

                return true;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
           
        public object GetTotalTriviaPoints(string userName)
        {
            try
            {
                int userId = (from d in dbModel.User_Profile where d.username == userName select d.uid).SingleOrDefault();

                var total = (from d in dbModel.triviapoints.AsEnumerable() where d.UserID == userId && d.Date.Value.Date == DateTime.UtcNow.Date select d.Points).Sum();

                return total;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public object GetTotalPoints(string userName)
        {
            try
            {
                int userId = (from d in dbModel.User_Profile where d.username == userName select d.uid).SingleOrDefault();

                var total = (from d in dbModel.points where d.userid == userId select d.points).SingleOrDefault();

                return total;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public object GetTotalPointsOfAll()
        {
            try
            {
                var list = (from d in dbModel.points join e in dbModel.User_Profile on d.userid equals e.uid orderby d.points descending
                             select new {FirstName  = e.fName, LastName = e.lName, Points = d.points, Date = d.datemodified, Pic =  e.pic }).Take(20).ToList();

                return list;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object GetInterest(string interests_id)
        {
            throw new NotImplementedException();
        }

        public object GetCountyByInterest()
        {
            throw new NotImplementedException();
        }
    }

}