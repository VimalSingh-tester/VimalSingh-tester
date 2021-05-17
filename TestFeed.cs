using Selenium.Network.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selenium.Network.TestComponents
{
    public class TestFeed : Baseclass
    {
        static string loginusername;
       static string loginpassword;
        static string Expectedtextpost;
      HomeLandingPage homepage;
        FeedComponent feed;
        static string Commenttext;

        public TestFeed()
        {
            Initial();
            homepage = new HomeLandingPage();
            loginusername = Configuration.GetSection("LoginData").GetSection("username").Value;
            loginpassword = Configuration.GetSection("LoginData").GetSection("password").Value;
            Expectedtextpost = Configuration.GetSection("Feed").GetSection("Posttext").Value;
            Commenttext= Configuration.GetSection("Feed").GetSection("Commenttext").Value; ;
            homepage.Login(loginusername, loginpassword);
            feed = new FeedComponent();

        }
        //Check company logo is visible
        [Fact]
        public void TestLogoIsVisible()
        {
            Boolean lg = feed.LogoIsVisible();
            Assert.True(lg);
            Driver.Close();
        }

        //check user image is displaced on the feed when he uploaded the image
        [Fact]
        [Obsolete]
        void TestFeedProfileImageisVisible()
        {
            Boolean feedprofileimg = feed.FeedProfileImageisVisible();
            Assert.True(feedprofileimg);
            Driver.Close();
        }

        //Test to check post on feed
        [Fact]
        public void TestPostOnFeed()
        {
            Boolean feedisposted = feed.PostOnFeed(Expectedtextpost);
            Assert.True(feedisposted);
            Driver.Close();
        }

        //Test to check Comment on post

        [Fact]
        [Obsolete]
        public void TestCommentOnPost()
        {
            Boolean commentisdone = feed.CommentOnPost(Commenttext);
            Assert.True(commentisdone);
            Driver.Close();
        }

        //Test to check to Edit the user post
        [Fact]
        [Obsolete]
        public void TestEditTheFeed()
        {
            Boolean editThepost = feed.EditTheFeed(Expectedtextpost);
            Assert.True(editThepost);
            Driver.Close();
        }

      

        //Check Reactions are visible and clickable on post
        [Fact]
        [Obsolete]
        public void TestCheckReactionVisibleAndClickable()
        {
            Boolean ReactionsAreVisibleAndClickable = feed.CheckReactionVisibleAndClickable();
            Assert.True(ReactionsAreVisibleAndClickable);
            Driver.Close();
        }

        [Fact]
        [Obsolete]
        public void TestCheckPostTotalReaction()
        {
            Boolean Totalreactionmatch = feed.CheckPostTotalReaction();
        }

        // Test to Check the picture on comment
        [Fact]
        [Obsolete]
        public void TestUserPictureOnComment()
        {
            Boolean userPicIsShown = feed.UserPictureOnComment();
            Assert.True(userPicIsShown);
            Driver.Close();
        }

        //Test to check the image on all comment of user on given post
        [Fact]
        [Obsolete]
        public void TestUserImageOnAllComment()
        {
            Boolean userPicsOnComment = feed.UserImageOnAllComment();
            Assert.True(userPicsOnComment);
            Driver.Close();
        }

        //Test to check whether post are pinned or not, if pinned then don't do anything
        [Fact]
        [Obsolete]
        public void TestPinTheFeedPost()
        {
            Boolean Pinpasscondition = feed.PinTheFeedPost();
            Assert.True(Pinpasscondition);
            Driver.Close();
        }

        [Fact]
        [Obsolete]
        public void TestUnpinTheFeedPost()
        {
            Boolean Unpinedsuccessful = feed.UnpinTheFeedPost();
            Assert.True(Unpinedsuccessful);
            Driver.Close();
        }
    }
}
