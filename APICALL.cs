using System;
using Xunit;
using System.Net;
using RestSharp;
using System.Collections.Generic;
using Newtonsoft.Json;
using StartupEd.Engine.MarketNetwork;
using System.Net.Http;
using StartupEd.Engine.MarketNetwork.Content;
using StartupEd.Engine.MarketNetwork.Assessment;

namespace StartupEd.Test.LibEngine
{
    public class APICall
    {

        static bool TestInitialized = false;
        public static string IDToken;
        public static string UserID;
        public static string baseurl;
        public static string _apiUrl;
        public static RestClient client;
        public static RestClient clientget;
        public static RestRequest request;
        public static RestRequest requestget;
        public static IRestResponse response;
        public static Dictionary<string, string> data;
        public static string TestUserEmail;
        public static string TestUserName;
        public static HashSet<string> myhash;
        public static HttpContent content;

        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();
        //}

        //public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)+        //{
        //    return base.GrantResourceOwnerCredentials(context)
        //}

        public static string apiUrl
        {
            get { return _apiUrl; }
            set
            {
                _apiUrl = value;
                client = new RestClient(_apiUrl);
                clientget = new RestClient(_apiUrl);
            }
        }

        private string GetRandomEmail()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int r = rnd.Next();
            return r + "x@startuped.net";
        }
        private string GetUsername()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int r = rnd.Next();
            return "Test" + r;
        }      // Runs every time before every individual test execution 
        public APICall()
        {
            baseurl = "http://localhost:5000/";
            TestUserEmail = GetRandomEmail();
            TestUserName = GetUsername();
            request = new RestRequest();
            requestget = new RestRequest();
            request.Timeout = 90000;
            requestget.Timeout = 90000;
            DisableLogAndEmail();

        }

        private void DisableLogAndEmail()
        {
            if (TestInitialized)
            {
                return;
            }

            request.Method = Method.GET;
            apiUrl = baseurl + "admin/email/enable/false";
            Assert.True(Execute());

            apiUrl = baseurl + "admin/log/disable";
            Assert.True(Execute());

            TestInitialized = true;
        }

        public static bool Execute()
        {
            try
            {
                //response = request.
                //.CreateRequest("/api/action-to-test")
                //.AddHeader("Content-type", "application/json")
                //.AddHeader("Authorization", "Bearer <insert token here>")
                //.GetAsync();
                response = client.Execute(request);
                if (response == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                response = client.Execute(requestget);
                if (response == null)
                {
                    return false;
                }
                return true;
                return false;
            }
        }
        public static bool Execute2()
        {
            try
            {
                //response = request.
                //.CreateRequest("/api/action-to-test")
                //.AddHeader("Content-type", "application/json")
                //.AddHeader("Authorization", "Bearer <insert token here>")
                //.GetAsync();
                response = clientget.Execute(request);
                if (response == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                response = clientget.Execute(requestget);
                if (response == null)
                {
                    return false;
                }
                return true;
                return false;
            }
        }

        //Get the data by ID and return the response
        public static IRestResponse GetCall(string Routeaddress, string ObjectID, string TokenID)
        {

            #region Get the data by ID
            requestget.Method = Method.GET;
            requestget.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            apiUrl = baseurl + Routeaddress + "/" + ObjectID;
            client.ClearHandlers();
            response = clientget.Execute(requestget);
            return response;
            #endregion
        }

        //Create a new User ,Login and passes a Tuple which contains userId and Token
        public static Tuple<string, string> CreateNewAccount(string TestUserEmail, string TestUserName, string Password)
        {

            #region Create a new user and login and return the UserID and Token
            request.Method = Method.POST;
            apiUrl = baseurl + "Authentication/Register";
            Authentication payload = new Authentication();
            payload.FirstName = TestUserName;
            payload.LastName = "user";
            payload.UserName = TestUserEmail;
            payload.Password = Password;
            payload.ConfirmPassword = Password;
            payload.IsValidAccount = true;
            payload.IsValidateTerms = true;
            var Data = JsonConvert.SerializeObject(payload);
            request.AddJsonBody(Data);
            Assert.True(Execute());
            Assert.True(response.StatusCode == HttpStatusCode.Created);
            request.Method = Method.POST;
            apiUrl = baseurl + "Authentication/Login";
            request.AddJsonBody(new { Email = TestUserEmail, Password = "@Password" });
            Assert.True(Execute());
            Assert.True(response.StatusCode == HttpStatusCode.OK);
            Authentication authresult = JsonConvert.DeserializeObject<Authentication>(response.Content);
            string IDToken = authresult.Token;
            string ID = authresult.UserId;
            return Tuple.Create(ID, IDToken);

            #endregion
        }

        //Create a New BMC and Passes the repsonse Which contain the Object ID.
        public static IRestResponse CreateBMC(string Title, string TokenID)
        {


            #region Create BMC

            request.Method = Method.POST;
            apiUrl = baseurl + "BusinessModel";
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            BusinessModel BMC = new BusinessModel();
            // BMC.Name = Title;
            var BMCData = JsonConvert.SerializeObject(BMC,
            new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            request.AddJsonBody(BMCData);
            Assert.True(Execute());

            return response;

            #endregion
        }

        //Create a New Business Page and Returns the response.
        public static IRestResponse CreateBusinessPage(string Name, string Category, string FocusIndustry,
                                                        string Description, string ContactInfoName, string ContactInfoPhoneNo, string ContactInfoEmail, string TokenID)
        {

            #region Create Business Page
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "BusinessPage";
            BusinessPage bpage = new BusinessPage();
            bpage.Name = Name;
            bpage.Category = Category;
            bpage.FocusIndustry = FocusIndustry;
            bpage.Description = Description;
            bpage.ContactInfo.Name = ContactInfoName;
            bpage.ContactInfo.PhoneNo = ContactInfoPhoneNo;
            bpage.ContactInfo.Email = ContactInfoEmail;
            var Data1 = JsonConvert.SerializeObject(bpage);
            request.AddJsonBody(Data1);
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            Assert.True(Execute());
            // var BP = JsonConvert.DeserializeObject(response.Content);

            return response;

            #endregion

        }
        //Create a new Business Plan and return the response
        public static IRestResponse CreateBusinessPlan(string TestUserName, string TestUserEmail, string Venturename, string TokenID)
        {

            #region Create a new Business plan 
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "BusinessPlan";
            BusinessPlan bpln = new BusinessPlan();
            bpln.Name = TestUserName;
            bpln.Email = TestUserEmail;
            bpln.VentureName = Venturename;
            var Data = JsonConvert.SerializeObject(bpln);
            request.AddJsonBody(Data);
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            Assert.True(Execute());
            return response;

            #endregion
        }
        //Create a New Community and return the response
        public static IRestResponse CreateCommunity(string Communityname, string descriptiontext, string TokenID)
        {

            #region Create Coummunity and return the reponse
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "Community";
            Community com = new Community();
            com.Name = Communityname;
            com.Description = descriptiontext;
            com.Privacy = PrivacyType.Public;
            var Data = JsonConvert.SerializeObject(com);
            request.AddJsonBody(Data);
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            Assert.True(Execute());
            return response;

            #endregion
        }
        //Create a New Startup Runway and return the response
        public static IRestResponse CreateStartupRunway(string Title, string TokenID)
        {

            #region Create a new Startup Runway
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "Runway";
            Runway runway = new Runway();
            runway.Name = Title;
            var Data = JsonConvert.SerializeObject(runway);
            request.AddJsonBody(Data);
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            Assert.True(Execute());
            return response;

            #endregion
        }

        //create a post data and post it ad return the response
        public static IRestResponse CreatePost(string TestUserName, string UserID, string summary, string TokenID)
        {

            #region Post and get the response
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "Post";
            Post post = new Post();
            post.Name = TestUserName;
            post.AuthorId = UserID;
            post.Summary = summary;
            var pt = JsonConvert.SerializeObject(post);
            request.AddHeader("Authorization", string.Format("Bearer {0}", TokenID));
            Assert.True(Execute());
            return response;

            #endregion
        }

        //Create A new Assessment and return the response
        public static IRestResponse CreateAssessment()
        {

            #region Create and get the response
            request.Parameters.Clear();
            request.Method = Method.POST;
            apiUrl = baseurl + "Assessment/CreateAssessment";
            Assessment assessment = new Assessment();
            assessment.Name = TestUserName;
            return response;

            #endregion
        }
    }
}


