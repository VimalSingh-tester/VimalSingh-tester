using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Selenium.Network.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selenium.Network.TestComponents
{
    public class TestHomeLandingPage : Baseclass
    {
        static string loginusername;
       static string loginpassword;
       public static string failconditionforlogin;
       public  static string passconditionforlogin;
       public static HomeLandingPage Home;
        public static string LoginAlertTextWrongPara;
        public static string baseurl;

        public bool Loginpass { get; private set; }
        public bool Conditionpass { get; private set; }

        public TestHomeLandingPage()
        {
            Initial();
            Home = new HomeLandingPage();
            baseurl = Configuration.GetSection("LoginData").GetSection("BaseUrl").Value;
            LoginAlertTextWrongPara = Configuration.GetSection("LoginData").GetSection("LoginAlertTextWrongPara").Value;
        }

        //Test for Login to Pass
        [Fact]
          void TestLogin()
        {
            loginusername = Configuration.GetSection("LoginData").GetSection("username").Value;
            loginpassword = Configuration.GetSection("LoginData").GetSection("password").Value;
            bool actualtext = Home.Login(loginusername,loginpassword);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.True(actualtext);
            Driver.Close();
        }

        //Test for Login to Fail
        [Theory]
        [InlineData("", "Vimal")]
        [InlineData("startuptest55@gmail.com","")]
        [InlineData("","")]

         public void TestLoginOneParaNullFail(string username,string password)
        {
            
            Home = new HomeLandingPage();
            Boolean actualAlerttext = Home.LoginWithOneParaNullFail(username, password);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            Assert.True(actualAlerttext);
            Driver.Close();
        }


        //both parameters are wrong
        [Theory]
        [InlineData("123@gmail.in", "123456")]

           public void TestLoginBothParaWrongFail(string username, string password)
        {
            Loginpass = Home.LoginWithWrongParaFail(username, password);
            Assert.True(Loginpass);
            Driver.Close();
        }

        [Theory]
        [Obsolete]
        [InlineData("abc","def","Vimal@1", "Vimal@1")]
        public void TestCreatNewAccountPass(string frstname, string lstname, string psswrd,
                                            string cnfrmpssword)
        {
            #region Test to Create  new account
            string email =AllinOne.GetRandomEmail();
            Conditionpass = Home.CreateNewAccountPass(frstname, lstname, email, psswrd, cnfrmpssword);
            Assert.True(Conditionpass);
            Driver.Close();
            #endregion
        }


        [Theory]
        [InlineData("abc", "def", "Vimal@1", "Vimal@1")]
        [Obsolete]
        public void TestResendMail(string frstname, string lstname, string psswrd,
                                            string cnfrmpssword)
        {
            #region Test for Resend mail to pass
            string email = AllinOne.GetRandomEmail();
            Conditionpass = Home.ResendMail(frstname, lstname, email, psswrd, cnfrmpssword);
            Assert.True(Conditionpass);
            Driver.Close();
            #endregion
        }


    }
}
