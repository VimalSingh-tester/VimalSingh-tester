using Newtonsoft.Json;
using RestSharp;
using StartupEd.Engine.MarketNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StartupEd.Test.LibEngine
{
    public class TestControllersAPI : APICall
    {
        public static Tuple<string, string> temp;
        public static IRestResponse response;
        public static IRestResponse getresponse;
        public static APICall callapi;
        public static Boolean passcondition;
        public static string ID;
        public static string TokenID;
        public static string ObjectID;
        public static HttpContent content;
        public TestControllersAPI()
        {
            callapi = new APICall();
            TestUserEmail = GetRandomEmail();
            TestUserName = GetUsername();

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
            }


        // Create a  new BMC and verify it is not null
        [Theory]
        [InlineData("")]
        [InlineData("DEMO 1")]
        [InlineData("12341")]
        [InlineData("#")]
        [InlineData("/")]
        [InlineData("?")]
        [InlineData(".")]
        [InlineData("*")]
        [InlineData("=")]
        [InlineData("+")]
        [InlineData("-")]
        [InlineData("~")]
        [InlineData("`")]
        [InlineData("{}")]

        [Obsolete]
        public void TestCreateBMC(string title)
        {
    
    
    #region create a new account and login
    temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a new BMC
        response = APICall.CreateBMC(title, TokenID);
             if (response.StatusCode == HttpStatusCode.BadRequest)
                   {
        Assert.False(false);
                    }
                else
                    {
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        ObjectID = response.Content;
        
        #endregion

        
        #region Get the created BMC
getresponse = APICall.GetCall("BusinessModel", ObjectID, TokenID);
        var BMC = JsonConvert.DeserializeObject(getresponse.Content);
        Assert.NotNull(BMC);
                    }
    
    #endregion
        }



        //Test to Create Business page and Verify that it with get call
        [Theory]
        [InlineData("", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "")]
        [InlineData("", "", "", "", "", "", "")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("#", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("$", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("?", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("+", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("-", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("*", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]  
        [InlineData("/", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("123456", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "12345678", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "@startuped.net")]
        [InlineData("Demo1", "STARTUP", "Art", "Hello QA", "QA", "1234567890", "user@startupednet")]
        [InlineData("Demo1", "E-CELL", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "INCUBATOR", "Art", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        [InlineData("Demo1", "STARTUP", "TravelTourism", "Hello QA", "QA", "1234567890", "user@startuped.net")]
        public void TestCreateBusinessPage(string Name, string Category, string FocusIndustry,
                          string Description, string ContactInfoName, string ContactInfoPhoneNo, string ContactInfoEmail)
        {
    
    
    #region Create New Account 
        temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a New Business Page

            response = APICall.CreateBusinessPage(Name, Category, FocusIndustry,
    Description, ContactInfoName, ContactInfoPhoneNo, ContactInfoEmail, TokenID);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
        
        Assert.False(false);
                    }
                else
        {
        
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        ObjectID = response.Content;
        
        #endregion

        
        #region Get the Business Page
        getresponse = APICall.GetCall("BusinessPage", ObjectID, TokenID);
        Assert.True(getresponse.StatusCode == HttpStatusCode.OK);
        var BP = JsonConvert.DeserializeObject(getresponse.Content);
        Assert.NotNull(BP);
        
                    }
    
    #endregion

           }

        //Test to Create Plan and Verify that it with get call
        [Theory]
        [InlineData("venture name")]
        [InlineData("")]
        [InlineData("12346")]
        [InlineData("@")]
        [InlineData("$")]
        [InlineData("+")]
        [InlineData("/")]
        [InlineData("*")]
        [InlineData("-")]
        [InlineData("-00")]
        [InlineData("~")]
        [InlineData("`")]
        [InlineData("{}")]
        public void TestCreateBusinessPlan(string Venturename)
        {
    
    #region Create New Account 
temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a New Business Plan

   response = APICall.CreateBusinessPlan(TestUserName, TestUserEmail, Venturename, TokenID);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
        
        Assert.False(false);
                    }
                else
                    {
        
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        ObjectID = response.Content;
        
        #endregion

        
        #region Get the Business Plan By ID
        getresponse = APICall.GetCall("BusinessPlan", ObjectID, TokenID);
        Assert.True(getresponse.StatusCode == HttpStatusCode.OK);
        var BPlan = JsonConvert.DeserializeObject(getresponse.Content);
        Assert.NotNull(BPlan);
        
        
                    }
    
    #endregion
        }

        //Test to Create Community and Verify that it with get call
        [Theory]
        [InlineData("", "Descriptions")]
        [InlineData("Community Name", "")]
        [InlineData("Community Name", "Descriptions")]
        [InlineData("", "")]
        [InlineData("123456", "")]
        [InlineData("community", "13123332")]
        [InlineData("#", "#")]
        [InlineData("=", "=")]
        [InlineData("+", "+")]
        [InlineData("*", "*")]
        [InlineData("-", "-")]
        [InlineData("/", "/")]
        [InlineData("[]", "{}")]
        [InlineData("%%", "%%")]
        [InlineData("`", "`")]
        [InlineData("!", "!")]
        public void TestCreateCommunity(string Communityname, string descriptiontext)
        {
    
    #region Create New Account 
temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a New Business Plan
response = CreateCommunity(Communityname, descriptiontext, TokenID);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
        
        Assert.False(false);
                    }
                else
                    {
        
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        ObjectID = response.Content;
        
        #endregion

        
        #region Get the Business Plan By ID
getresponse = APICall.GetCall("Community", ObjectID, TokenID);
        Assert.True(getresponse.StatusCode == HttpStatusCode.OK);
        var com = JsonConvert.DeserializeObject(getresponse.Content);
        Assert.NotNull(com);
        
        
                    }
    
    #endregion
        }

        //Test to Startup Runway and Verify that it with get call
        [Theory]
        [InlineData("startup runway for testing api")]
        [InlineData("")]
        public void TestCreateStartupRunway(string Title)
        {
    
    #region Create New Account 
         temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a New Business Plan
response = CreateStartupRunway(Title, TokenID);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                   {
        
        Assert.False(false);
                    }
                else
                    {
        
        Assert.True(response.StatusCode == HttpStatusCode.Created);
        ObjectID = response.Content;
        
        #endregion

        
        #region Get the Business Plan By ID
getresponse = APICall.GetCall("Runway", ObjectID, TokenID);
        Assert.True(getresponse.StatusCode == HttpStatusCode.OK);
        var com = JsonConvert.DeserializeObject(getresponse.Content);
        Assert.NotNull(com);
        
        
                    }
    
    #endregion
       }
        //Test to Post On feed and verify that it with get call
        [Theory]
        [InlineData("summary")]
        [InlineData("")]
        [InlineData("@")]
        [InlineData("$")]
        [InlineData("+")]
        [InlineData("/")]
        [InlineData("*")]
        [InlineData("-")]
        [InlineData("-00")]
        [InlineData("~")]
        [InlineData("`")]
        [InlineData("{}")]

        public void TestPOST(string summary)
        {
    
    #region Create New Account 
temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;

            #endregion


            #region Create a New Business Plan
            response = APICall.CreatePost(TestUserName, UserID, summary, TokenID);
                if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
        
        Assert.False(false);
                    }
                else
                    {
        
        Assert.True(response.StatusCode == HttpStatusCode.Created);
       ObjectID = response.Content;
       
        #endregion

        
        #region Get the Business Plan By ID
getresponse = APICall.GetCall("Post", ObjectID, TokenID);
        Assert.True(getresponse.StatusCode == HttpStatusCode.OK);
        var post = JsonConvert.DeserializeObject(getresponse.Content);
       Assert.NotNull(post);
        
        
                    }
    
    #endregion

            }
        [Fact]
        public void TestAssessment()
        {
    
    #region Create New Account 
temp = APICall.CreateNewAccount(TestUserEmail, TestUserName, "@password");
    UserID = temp.Item1;
    TokenID = temp.Item2;
    
    #endregion

    
    #region Create a New Assessment 
response = APICall.CreateAssessment();
    
    
    
    #endregion
        }
    }
}


 