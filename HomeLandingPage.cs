using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace Selenium.Network.Components
{
    public class HomeLandingPage : Baseclass
    {
        static string url;
        static string VerificationmailText;
        static string Toastmsgverification; 
        static AllinOne oneclass;
        public static Boolean URLhome;
        private static WebDriverWait wait;

        public HomeLandingPage()
        {
            wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20));
            PageFactory.InitElements(Driver, this);
            VerificationUrl= Configuration.GetSection("Verification").GetSection("VerificationUrl").Value;
            VerificationmailText = Configuration.GetSection("CreateNewAccount").GetSection("VerificationmailText").Value;
            url = Configuration.GetSection("LoginData").GetSection("geturl").Value;
            Toastmsgverification = Configuration.GetSection("Verification").GetSection("Toastmsgverification").Value;
           
        }

      
        
        private static string VerificationUrl;


        //Elements or login
        #region Login elements

        [FindsBy(How = How.Id, Using = "loginEmail")]
        public IWebElement Usernametext { get; set; }

        [FindsBy(How = How.Id, Using = "loginPassword")]
        public IWebElement Passwordtext { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='loginBtn']")]
        public IWebElement Loginbutton { get; set; }

        [FindsBy(How = How.ClassName, Using = "validation-message")]
        public IWebElement Loginerror { get; set; }

        [FindsBy(How = How.CssSelector, Using = "span[class='text-muted font-weight-bold font-size-lg']")]
        public IWebElement Loginpasscondition { get; set; }
        #endregion

        // Elements for Google and LinkedIn 
        #region Social Login(Google and LinkedIn)
        [FindsBy(How = How.ClassName, Using = "socicon-linkedin")]
        public IWebElement Linkedinlinkbutton { get; set; }

        [FindsBy(How = How.ClassName, Using = "socicon-google")]
        public IWebElement Googlelinkbutton { get; set; }

        #endregion

        //elements for create new user

        #region Create new user elements
        [FindsBy(How = How.XPath, Using = "//a[@id='kt_login_signup' and @href='signup']")]
        public IWebElement Createnewaccountbutton { get; set; }

        [FindsBy(How = How.Id, Using = "firstName")]
        public IWebElement Firstnametext { get; set; }

        [FindsBy(How = How.Id, Using = "lastName")]
        public IWebElement Lastnametext { get; set; }

        [FindsBy(How = How.Id, Using = "signupEmail")]
        public IWebElement Emailtext { get; set; }

        [FindsBy(How = How.Id, Using = "signupPassword")]
        public IWebElement Signuppasswordtext { get; set; }

        [FindsBy(How = How.Id, Using = "confirmPassword")]
        public IWebElement Signupconfirmpasswordtext { get; set; }

        [FindsBy(How = How.ClassName, Using = "filter-option-inner-inner")]
        public IWebElement Roleselectdropdownbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//label[@class='checkbox checkbox-outline font-weight-bold']//span")]
        public IWebElement Agreebutton { get; set; }

        [FindsBy(How = How.Id, Using = "signupBtn")]
        public IWebElement Submitbutton { get; set; }

        [FindsBy(How = How.Id, Using = "kt_login_signup_cancel")]
        public IWebElement Cancelbutton { get; set; }

        [FindsBy(How = How.ClassName, Using = "validation-message")]
        public IWebElement Emailerrorcondition { get; set; }

        [FindsBy(How = How.ClassName, Using = "validation-message")]
        public IWebElement Passworderrortext { get; set; }

        #endregion

        //Elements for claim
        #region Claim acoount elements
        [FindsBy(How = How.Id, Using = "claim_modal")]
        public static IWebElement Claimaccountbutton { get; set; }

        [FindsBy(How = How.Id, Using = "kt_resend-verification-email")]
        public IWebElement Sendverificationbutton { get; set; }

        [FindsBy(How = How.Id, Using = "providedEmailId")]
        public IWebElement Claimemail { get; set; }

        [FindsBy(How = How.Id, Using = "providedPassword")]
        public IWebElement Claimpassword { get; set; }

        [FindsBy(How = How.CssSelector, Using = "css=button[class='btn.btn-primary.font-weight-bold']")]
        public IWebElement Submitclaimbutton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "css=button[class='btn.btn-light-primary.font-weight-bold']")]
        public IWebElement Cancelsubmitbutton { get; set; }
        #endregion

        //Elements of user onboarding
        #region Elements of useronboarding

        //[FindsBy(How = How.XPath, Using = "//*[@id='kt_body']/iframe")]
        //public IWebElement ToastFrame { get; set; }

        #endregion

        //for Toast service
        #region ToastServices elements
        [CacheLookup]
        [FindsBy(How = How.XPath, Using = "//*[text()='Registration Successful!']")]
        //*[text()="Login Failed"]--xpath
        //*[@id="kt_body"]/iframe--frame

        public IWebElement ToastVerification { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@id='kt_body']/iframe")]
        public IWebElement ToastFrame { get; set; }
        #endregion


        //Verification mail text
        #region Vertification Elements
        [FindsBy(How = How.XPath, Using = "//div[@class='text-center mb-10 mb-lg-20']/h3")]
        public IWebElement Actualverifytext { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Resend')]")]
        public IWebElement ResendButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='login-forgot']")]
        public IWebElement Frameresend1 { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='text-center mb-10 mb-lg-20']")]
        public IWebElement Frameresend2 { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@type='email']")]
        public IWebElement Resendmailtextarea { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@type='submit']")]
        public IWebElement ResendmailButton { get; set; }
        public bool pass { get; private set; }
        public bool verifypass { get; private set; }
        #endregion

        // public static IWebElement ToastParent = Driver.FindElement(By.XPath("//div[@id='toast-container' and @class='toast-top-right']"));
        //public static IWebElement ToastChild = ToastParent.FindElement(By.XPath("//*[contains(text(),'User not found')]"));

        //For Login
        [Obsolete]
        public Boolean Login(string id, string password)
        {
            AllinOne.WaitUnitElementisClickable(Usernametext);
            Usernametext.SendKeys(id);
            Passwordtext.SendKeys(password);
            Loginbutton.Click();
            Boolean pass = Loginpasscondition.Displayed;
            return pass;
        }


        // For Login to Fail 1.when one parameter is null
        public Boolean LoginWithOneParaNullFail(string id, string password)
        {
            Usernametext.SendKeys(id);
            Passwordtext.SendKeys(password);
            Loginbutton.Click();
            Boolean error = Loginerror.Displayed;
            return error;
        }

        // For Login to Fail 1.when both parameters are wrong
        public bool LoginWithWrongParaFail(string id, string password)
        {
            Usernametext.SendKeys(id);
            Passwordtext.SendKeys(password);
            Loginbutton.Click();
            Actions action = new Actions(Driver);
            //Driver.Manage().Timeouts().ImplicitWait= TimeSpan.FromSeconds(1);
            // IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(1));
            //IWebElement element = wait.Until(driver => driver.FindElement(By.XPath("//*[contains(text(),'User not found')]")));
            // action.MoveToElement(ToastChild).Build().Perform();
            string URL = Driver.Url;
            if (url == URL)
            {
                pass = true;
            }

            //string Toasttext = Toast.Text;
            return pass;
        }

        private object WebDriverWait(IWebDriver driver, TimeSpan timeSpan)
        {
            throw new NotImplementedException();
        }


        /// For Create a new account to pass
        [Obsolete]
        public bool CreateNewAccountPass(string frstname, string lstname, string email, string psswrd,
                                            string cnfrmpssword)
        {
            #region Create new account to pass
            NewAccount(frstname, lstname, email, psswrd, cnfrmpssword);
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            bool verifycreatedtext = Actualverifytext.Displayed;
            return verifycreatedtext;
            #endregion
        }

        [Obsolete]
        public bool ResendMail(string frstname, string lstname, string email, string psswrd,
                                            string cnfrmpssword)
        {
            #region Resendmail for verification to pass
            NewAccount(frstname, lstname, email, psswrd, cnfrmpssword);
            AllinOne.WaitUnitElementisClickable(ResendButton);
            ResendButton.Click();
            Resendmailtextarea.SendKeys(email);
            ResendmailButton.Click();
            bool verifycreatedtext = Actualverifytext.Displayed;
            return verifycreatedtext;
            #endregion
        }

        ///For Create a new account to Fail

        [Obsolete]
        public void NewAccount(string frstname, string lstname, string email, string psswrd,
                                           string cnfrmpssword)
        {
            #region NewAccount
            wait.Until(ExpectedConditions.ElementToBeClickable(Createnewaccountbutton));
            Createnewaccountbutton.Click();
            Agreebutton.Click();
            Firstnametext.SendKeys(frstname);
            Lastnametext.SendKeys(lstname);
            Emailtext.SendKeys(email);
            Signuppasswordtext.SendKeys(psswrd);
            Signupconfirmpasswordtext.SendKeys(cnfrmpssword);
            Submitbutton.Click();
            #endregion
        }

        ///when firstname and lastname is empty
        [Obsolete]
        public string CreateNewAccountnullFirstname( string email, string psswrd,
                                           string cnfrmpssword)
        {
            NewAccount("", "", email, psswrd, cnfrmpssword);
            var alert = Driver.SwitchTo().Alert();
            string alerttext = alert.Text;
            return alerttext;
        }

        ///when email is empty
        [Obsolete]
        public string CreateNewAccountEmptyEmail(string frstname, string lstname, string psswrd,
                                           string cnfrmpssword)
        {
            NewAccount(frstname, lstname, "", psswrd, cnfrmpssword);
            string emailerror = Emailerrorcondition.Text;
            return emailerror;
        }

        ////when password is empty
        [Obsolete]
        public string CreateNewAccountEmptypassword(string frstname, string lstname, string email,
                                           string cnfrmpssword)
        {
            NewAccount(frstname, lstname, email, "", cnfrmpssword);
            string passworderror = Passworderrortext.Text;
            return passworderror;
        }

        ///when confirm password or password is empty
        [Obsolete]
        public string CreateNewAccountnullConfirmPassword(string frstname, string lstname, string email)
                                         
        {
            NewAccount(frstname, lstname, email, "", "");
            var alert = Driver.SwitchTo().Alert();
            string confirmpassword = alert.Text;
            return confirmpassword;
        }

       

        public void ClaimAccount(string claimid, string claimPassword)
        {
            Claimemail.SendKeys(claimid);
            Claimpassword.SendKeys(claimPassword);
            Claimaccountbutton.Click();
        }

    }
}









    

