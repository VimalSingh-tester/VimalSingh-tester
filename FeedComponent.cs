using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selenium.Network.Components
{
    class FeedComponent : Baseclass
    {
        public static string pathprofilefromfeed;
        public static string ProfileID;
        private bool passcondition;

        public FeedComponent()
        {
            PageFactory.InitElements(Driver, this);
            pathprofilefromfeed = Configuration.GetSection("Feed").GetSection("pathprofilefromfeed").Value;
            

        }

        //Feed Webelements
        #region Feed Webelements
        [FindsBy(How = How.XPath, Using = "//img[@class='h-100 align-self-end w-40px']")]
        public IWebElement feedprofileimage { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@contenteditable='true']")]
        public IWebElement feedtypetext { get; set; }

        [FindsBy(How = How.Id, Using = "ideaPostBtn")]
        public IWebElement postbutton { get; set; }

        //[FindsBy(How = How.XPath, Using = "//button[@data-toggle='dropdown' and @aria-haspopup='true']")]
        //public IWebElement gotopinbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-hover-text-warning btn-hover-icon-warning btn-sm" +
                             " btn-text-dark-50 bg-hover-light-primary rounded font-weight-bolder font-size-sm p-2']" +
                             "//img[@class='reaction-icon_img']")]
        public IWebElement postCommentButton{ get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@class='form-control' and @type='text']/" +
                                        "ancestor::div[@class='row mb-5']//following::input")]
        public IWebElement postCommentText { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@id='commentSubmitBtn']")]
        public IWebElement postCommentSubmitbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='text-dark-75 font-size-sm font-weight-normal pt-1']")]
        public IWebElement postedCommentText { get; set; }

        [FindsBy(How = How.XPath, Using = "//img[@class='rounded-circle']")]
        public IWebElement userPicOnComment { get; set; }
        // [FindsBy(How = How.XPath, Using = )]
        // public IWebElement profilebuttononfeed { get; set; }
        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement userrecentfeed { get; set; }

        [FindsBy(How = How.XPath, Using = "//i[@class='ki ki-bold-more-ver']")]
        public IWebElement gotoPinButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Pin')]")]
        public IWebElement PinButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='flaticon2-edit ml-5']")]
        public IWebElement EditButtonOnPost { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Unpin')]")]
        public IWebElement UnPinButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='ql-editor']")]
        public IWebElement TextArea { get; set; }

        // reaction elements
        [FindsBy(How = How.XPath, Using = "//button[@class='btn btn-icon-danger btn-sm btn-text-dark-50 bg-hover-light-success btn-hover-text-success rounded font-weight-bolder font-size-sm p-2']")]
        public IWebElement Totalreactionsbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='d-flex align-items-center pt-1']/a[1]")]
        public IWebElement Heartreactionbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='d-flex align-items-center pt-1']/a[2]")]
        public IWebElement Bulbreactionbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='d-flex align-items-center pt-1']/a[3]")]
        public IWebElement Coinreactionbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='d-flex align-items-center pt-1']/a[4]")]
        public IWebElement Rocketreactionbutton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='myTabContent']")]
        public IWebElement Reactiontabusers { get; set; }
        //reaction tick ,wrong and textarea
        [FindsBy(How = How.XPath, Using = "//i[@class='fas fa-check']")]
        public IWebElement Tickbutton { get; set; }

        //[FindsBy(How = How.XPath, Using = "//div[@id='6078a41f73588880764803f2']//div")]
        //public IWebElement TextArea { get; set; }
        //Logo Elements
        [FindsBy(How = How.ClassName, Using = "w-150px")]
        public IWebElement LOGO { get; set; }
        #endregion
        //Logo is Visible
        public Boolean LogoIsVisible()
        {
            Boolean lg = LOGO.Displayed;
            return lg;
        }

        //Feed image of user or not
        [Obsolete]
        public Boolean FeedProfileImageisVisible()
        {
            string imagename = Configuration.GetSection("MyData").GetSection("Imagename").Value;
            Boolean feedprofilelg = AllinOne.Compareimage(feedprofileimage, imagename);
            return feedprofilelg;
        }

        //Posting on feed
        public Boolean PostOnFeed(string text)
        {
            #region Post On Feed
            feedtypetext.SendKeys(text);
            postbutton.Click();
            AllinOne.ConfirmFeedPost(text);
            Boolean okstatus = true;
            return okstatus;
            #endregion 
        }

        //Editing the post 
        [Obsolete]
        public Boolean EditTheFeed(string text)
        {
            #region edit post 
            feedtypetext.SendKeys(text);
            postbutton.Click();
            Driver.Navigate().Refresh();
            AllinOne.WaitUnitElementisClickable(EditButtonOnPost);
            EditButtonOnPost.Click();
            string Text = AllinOne.TEXTForPost();
            TextArea.SendKeys(Text);
            
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.PageDown).Perform();
            Tickbutton.Click();
            Driver.Navigate().Refresh();
            AllinOne.WaitUnitElementisClickable(EditButtonOnPost);
            string actualtext = Text + text;
            IWebElement ActualAfterEdit = Driver.FindElement(By.XPath("//p[contains(text(),'"+ actualtext + "')]"));
            string actualAfterEdit = ActualAfterEdit.Text;
            if (actualtext == actualAfterEdit)
            {
                passcondition = true; ;
            }
            else { passcondition = false; }
            return passcondition;
            #endregion

        }

        //Edit all user post
        //[Obsolete]
        //public Boolean EditAllUserPost()
        //{
        //    #region Edit all user post
        //    passcondition = AllinOne.EditPostOfUSer(EditButtonOnPost,Tickbutton);
        //    return passcondition;
        //    #endregion
        //}

        // Comment on post
        [Obsolete]
        public Boolean CommentOnPost(string text)
        {
            #region Comment On post
            postCommentButton.Click();
            postCommentText.SendKeys(text);
            IWait<IWebDriver> wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(5));
            wait.Until(ExpectedConditions.ElementToBeClickable(postCommentSubmitbutton));
            postCommentSubmitbutton.Click();
            Driver.Navigate().Refresh();
            wait.Until(ExpectedConditions.ElementToBeClickable(postCommentButton));
            postCommentButton.Click();
            string commenttext = postedCommentText.Text;
            if (text == commenttext)
            {
                 passcondition = true;
            }
            else { passcondition = false; }

            return passcondition;
            #endregion
        }

        //Reaction buttons are visible and click able 
        [Obsolete]
        public Boolean CheckReactionVisibleAndClickable()
        {
            #region check the reaction buttons are visible and after that clickable
            passcondition = AllinOne.PostReactionsVisibleClickable(Heartreactionbutton,Bulbreactionbutton,Coinreactionbutton,Rocketreactionbutton,Totalreactionsbutton);
            return passcondition;
            #endregion
        }


        //Total reaction are showing or not and in the reaction tab user is showing or not
        [Obsolete]
        public Boolean CheckPostTotalReaction()
        {
            passcondition = AllinOne.totalReactionNumber(Heartreactionbutton, Bulbreactionbutton, Coinreactionbutton, Rocketreactionbutton, Totalreactionsbutton);
            return passcondition;
        }

        //Pin the post if pined then leave
        [Obsolete]
        public Boolean PinTheFeedPost()
        {
            #region if pinned then ok otherwise pin the post
            passcondition= AllinOne.PinThePost(gotoPinButton,PinButton,UnPinButton);
            return passcondition;         
            #endregion
        }
        //unpin the post if pined if not pined then leave
        [Obsolete]
        public Boolean UnpinTheFeedPost()
        {
            #region if pined then unpined 
            passcondition= AllinOne.UnpinThePost(gotoPinButton, PinButton, UnPinButton);
            return passcondition;
            #endregion
        }


        //user Profile picture on comment is visible or not
        [Obsolete]
        public Boolean UserPictureOnComment()
        {
            #region User pic on Comment
            string imagename =AllinOne.GetParameterFromJsonFile("MyData", "Imagename");
            AllinOne.WaitUnitElementisClickable(postCommentButton);
            postCommentButton.Click();
            AllinOne.WaitUnitElementisClickable(userPicOnComment);
            passcondition = AllinOne.Compareimage(userPicOnComment, imagename);
            return passcondition;
            #endregion
        }


        //User image on all the comment on a particular post
        [Obsolete]
        public Boolean UserImageOnAllComment()
        {
            #region Verify Image On All comment of user
            string imagename = AllinOne.GetParameterFromJsonFile("MyData", "Imagename");
            string ImageOnCommentspath = AllinOne.GetParameterFromJsonFile("Feed", "ImageOnCommentspath");
            AllinOne.WaitUnitElementisClickable(postCommentButton);
            postCommentButton.Click();
           passcondition= AllinOne.CompareimageONComment(ImageOnCommentspath, imagename);

            return passcondition;
            #endregion
        }


    }
}
