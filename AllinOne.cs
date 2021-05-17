using java.awt.image;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Selenium
{
   public class AllinOne: Baseclass
    {
        public static IWebElement GooglePassword;
        public static IWebElement GoogleNextbutton1;
        public static IWebElement GoogleNextbutton2;
        public static IWebElement GoogleUsername;
        public static string GooglemailURL = Configuration.GetSection("Verification").GetSection("GooglemailURL").Value;
        public static string pathofelement;
        public static Actions action;
        static Boolean psscondition;
        public static string ProfileID;
        public static string Expectedtextpost;
        public static string Expectedtimetext;
        public static string xpathofpostelement;
        private static bool passcondition;
        private static IWebElement TextArea;
        private static bool liked;

        /// <summary>
        /// Confrim method is used when something is posted on feed and then we have to check wheather is posted or not
        /// It returns the boolean value 
        /// </summary>
        /// 
        public static string GetParameterFromJsonFile(string section1,string section2)
        {
            string name = Configuration.GetSection(section1).GetSection(section2).Value;
            return name;
        }
        public static string GetParameterFromJsonFile(string section1, string section2,string section3)
        {
            string name = Configuration.GetSection(section1).GetSection(section2).GetSection(section3).Value;
            return name;
        }

        //Wait unit the element is clickable
        [Obsolete]
        public static void WaitUnitElementisClickable(IWebElement element)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }

        //swtich the frame
        public static void SwtichFrame(IWebElement frameElement)
        {
            Driver.SwitchTo().Frame(frameElement);
        }


        //posted on not
        public static Boolean ConfirmFeedPost(String Expectedtextpost)
        {
            ProfileID = Configuration.GetSection("MyData").GetSection("ProfileID").Value;
            pathofelement = Configuration.GetSection("Feed").GetSection("XpathOfElement").Value;
           // Expectedtextpost = Configuration.GetSection("Feed").GetSection("Posttext").Value;
            Expectedtimetext = Configuration.GetSection("Feed").GetSection("Expectedtimetext").Value;
            action = new Actions(Driver);
            string hrefofpath = "profile/" + ProfileID;


            xpathofpostelement = "//*[contains(text(),'" + Expectedtextpost + "')]";
          
            //list of all the post which is given on the page
            
            IList<IWebElement> ListFeedpostprofile = Driver.FindElements(By.Id("feedPost"));
          
            for (int i = 0; i <= 100; i++)
            {
               
               // string actualhref = ListFeedpostprofile.ElementAt(i).GetAttribute("href");
                //if (actualhref == hrefofpath)
                //{
                    //goes to that element via scrolling
                    action.MoveToElement(ListFeedpostprofile.ElementAt(i)).Build().Perform();

                //search for the text(to change the listfeedpostprofile to element where actuall text is)
                
                IWebElement ValidateElement = Driver.FindElement(By.XPath("//span[contains(text(),'about 5 seconds ago')]"));

                    string Validatetimetext = ValidateElement.Text;
                    if (Validatetimetext == Expectedtimetext)
                    {
                        string Actualtextpost = Driver.FindElement(By.XPath(xpathofpostelement)).Text;
                        if (Actualtextpost == Expectedtextpost)
                        {
                            psscondition = true;
                            break;
                        }
                        else { psscondition = false; }

                    }


               // }
            }
            return psscondition;
        }
        
        public static void GoogleAccountVerification()
        {
           
            Driver.Navigate().GoToUrl(GooglemailURL);
            GoogleUsername = Xpath("//input[@id='identifierId']");
            GoogleNextbutton1 = Xpath("//div[@class='VfPpkd-RLmnJb']");
            GooglePassword = Xpath("//input[@type='password']");
            GoogleNextbutton2 = Xpath(" public static IWebElement GoogleNextbutton;");
        }

        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }

        //random imagename
        public static string Imagename()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int r = rnd.Next();
            return r + "text.jpg";
        }

        //random text
        public static string TEXTForPost()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int r = rnd.Next();
            return "QA Post Edit purpose string="+r+"=>";
        }
    //random email
        public static string GetRandomEmail()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int r = rnd.Next();
            return r + "x@startuped.net";
        }

        //Image verification method
        [Obsolete]
        public static Boolean Compareimage(IWebElement actualimage,string expectedimagename)
        {
            #region pass image name and actualimage path element 
            string imagename = Imagename();
            //#region Take screanshot and save it in destination folder
           
            //Driver.TakeScreenshot().SaveAsFile("imagename");
            
            

            //tempImage.SaveAsFile(@"\Users\Public\Pictures"+imagename, ImageFormat.Png);

           // #endregion
            //go to website where 
            //automatically download the image and save the image in Selenium of bigapple
            //options.SetPreference("browser.download.dir", "C:\\Users\\Public\\Pictures");
            //options.SetPreference("browser.download.folderList", 4);
            //options.SetPreference("browser.download.manager.showWhenStarting", false);
            //options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/octet-stream");
            string URLofImage = actualimage.GetAttribute("src");
            Driver.Navigate().GoToUrl(URLofImage);
           IWebElement navigatedimage= Driver.FindElement(By.ClassName("transparent"));
            AllinOne.WaitUnitElementisClickable(navigatedimage);
            Point point = navigatedimage.Location;
            int width = navigatedimage.Size.Width;
            int height = navigatedimage.Size.Height;
            Rectangle section1 = new Rectangle(point, new Size(width, height));
            //string pathactualImage = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer) + @"C:\\Users\\Public\\Pictures" + imagename;
            string pathEXpectedimage = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"C:\Workspace\bigapple\Tests\Selenium\image\"+ expectedimagename;
            //Bitmap ExpectedImage = new Bitmap(pathEXpectedimage, true);
            Bitmap ExpectedImage = (Bitmap)Image.FromFile(@"C:\\Workspace\\bigapple\\Tests\\Selenium\\image\\" + expectedimagename);
            //Bitmap ActualImage = new Bitmap(pathactualImage, true);
            int width1 = ExpectedImage.Width; int height2 = ExpectedImage.Size.Height;
            Rectangle section2 = new Rectangle(point, new Size(width, height));
            if (section1==section2)
            {
                passcondition = true;
            }
            else { passcondition = false; }
            return passcondition;
            #endregion
        }

        //Verify the image on Comments when user commment on post
        [Obsolete]
        public static Boolean CompareimageONComment(string ImageOnComments, string expectedimagename)
        {
            
            #region pass image name and actualimage path element 
            int imagecount = 0;
            string username=GetParameterFromJsonFile("MyData","Username");
            string imagename = Imagename();
            IList<IWebElement> ListOfUserComment = Driver.FindElements(By.XPath("//a[@class='text-dark-75 text-hover-primary mb-1 font-size-lg font-weight-bolder pr-6'][contains(text(),'"+username +"')]"));
            int usercomment = ListOfUserComment.Count;
            IList<IWebElement> ListofimagesOnComment = Driver.FindElements(By.XPath(ImageOnComments));

            for (int i = 0; i <= ListofimagesOnComment.Count-1; i++)
            {
                IWebElement actualimage = ListofimagesOnComment.ElementAt(i);
                //WaitUnitElementisClickable(actualimage);
               string URLofImage = actualimage.GetAttribute("src");
                //actualimage.SendKeys(Keys.Control+"t");
                //Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                ((IJavaScriptExecutor)Driver).ExecuteScript("window.open();");
                Driver.SwitchTo().Window(Driver.WindowHandles.Last());
                Driver.Navigate().GoToUrl(URLofImage);
                IWebElement navigatedimage = Driver.FindElement(By.XPath("//img[@class='transparent'] | //img[@src='https://s1.startuped.net/incubation_cloud/assets/media/users/default.jpg']"));
                AllinOne.WaitUnitElementisClickable(navigatedimage);
                Point point = navigatedimage.Location;
                int width = navigatedimage.Size.Width;
                int height = navigatedimage.Size.Height;
                Rectangle section1 = new Rectangle(point, new Size(width, height));
                string pathEXpectedimage = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"C:\Workspace\bigapple\Tests\Selenium\image\" + expectedimagename;
                Bitmap ExpectedImage = (Bitmap)Image.FromFile(@"C:\\Workspace\\bigapple\\Tests\\Selenium\\image\\" + expectedimagename);            
                int width1 = ExpectedImage.Width; int height2 = ExpectedImage.Size.Height;
                // Point point2 = ExpectedImage.Location;
                var fileLength = new FileInfo(pathEXpectedimage).Length;
                if (width==width1 && height==height2)
                {
                    imagecount++;
                }
                Driver.Close(); 
                Driver.SwitchTo().Window(Driver.WindowHandles.First());
                
            }
            if (usercomment== imagecount) { passcondition = true; }
            else { passcondition = false; }
            return passcondition;
            #endregion
        }
        //Pined the post 
        [Obsolete]
        public static Boolean PinThePost(IWebElement gotoPinButton, IWebElement PinButton,IWebElement UnPinButton)
        {
            string Username = GetParameterFromJsonFile("MyData", "Username");
            IWebElement username = Driver.FindElement(By.XPath("//*[contains(text(),'"+Username+"')]"));
            if (username.Displayed)
            {
                AllinOne.WaitUnitElementisClickable(gotoPinButton);
                gotoPinButton.Click();
                if (UnPinButton.Displayed)
                {
                    return passcondition = true;
                }
                else
                {
                    PinButton.Click();
                    Driver.Navigate().Refresh();
                    AllinOne.WaitUnitElementisClickable(gotoPinButton);
                    gotoPinButton.Click();
                    passcondition = UnPinButton.Displayed;
                    return passcondition = true;
                }
            }
            else
            {
                AllinOne.WaitUnitElementisClickable(gotoPinButton);
                gotoPinButton.Click();
                if (UnPinButton.Displayed)
                {
                    return passcondition = true;
                }
                else
                {
                    PinButton.Click();
                    Driver.Navigate().Refresh();
                    AllinOne.WaitUnitElementisClickable(gotoPinButton);
                    gotoPinButton.Click();
                    passcondition = UnPinButton.Displayed;
                    return passcondition = true;
                }
                
            }
            return passcondition = false;
        }

        //Unpined the post
        [Obsolete]
        public static Boolean UnpinThePost(IWebElement gotoPinButton, IWebElement PinButton, IWebElement UnPinButton)
        {
            string Username = GetParameterFromJsonFile("MyData", "Username");
            IWebElement username = Driver.FindElement(By.XPath("//*[contains(text(),'" + Username + "')]"));
            if (username.Displayed)
            {
                AllinOne.WaitUnitElementisClickable(gotoPinButton);
                gotoPinButton.Click();
                if (UnPinButton.Displayed)
                {
                    UnPinButton.Click();
                    Driver.Navigate().Refresh();
                    AllinOne.WaitUnitElementisClickable(gotoPinButton);
                    gotoPinButton.Click();
                    passcondition = PinButton.Displayed;

                    return passcondition = true;
                }
                else
                {
                    return passcondition = true;
                }
            }
            else
            {
                AllinOne.WaitUnitElementisClickable(gotoPinButton);
                gotoPinButton.Click();
                if (UnPinButton.Displayed)
                {
                    UnPinButton.Click();
                    Driver.Navigate().Refresh();
                    AllinOne.WaitUnitElementisClickable(gotoPinButton);
                    gotoPinButton.Click();
                    passcondition = PinButton.Displayed;

                    return passcondition = true;
                }
                else
                {
                    return passcondition = true;
                }
            }
                return passcondition;
        }

        //Edit all the user post 
        [Obsolete]
      //  public static Boolean EditPostOfUSer(IWebElement EditButtonOnPost,IWebElement Tickbutton)
        //{
        //    int count = 0;
        //    IList<IWebElement> EditButtonOnPosts = Driver.FindElements(By.XPath("//*[@class='flaticon2-edit ml-5']"));
        //    IList<IWebElement> posttext = Driver.FindElements(By.XPath("//*[@class='card-custom py-5 pr-5']"));

        //    string Username = GetParameterFromJsonFile("MyData", "Username");
        //    IList<IWebElement> Userpost = Driver.FindElements(By.XPath("//*[contains(text(),'" + Username + "')]"));
        //    int userpost = Userpost.Count - 1;
        //   // IList<IWebElement> totalpost = Driver.FindElements(By.XPath("//*[@id='feedPost']"));

        //    for (int i = 1; i <= Userpost.Count; i++)
        //    {
        //        Actions actions = new Actions(Driver);
        //        int z= 0;
                
        //        if (i%3 == 0)
        //        {
                //    actions.SendKeys(Keys.PageDown).Perform();
                //}
                //actions.MoveToElement(Userpost.ElementAt(i));
                //actions.Perform();
                //if (EditButtonOnPosts.ElementAt(z).Displayed)
                //{   
                //    string actualtextbeforclick = posttext.ElementAt(i-1).Text;
                //    EditButtonOnPost.Click();

                //    IWebElement EditTextArea = Driver.FindElement(By.XPath("//div[@class='ql-editor']"));
                //    string actualtextafterclick = EditTextArea.Text;
                //    Assert.True(actualtextafterclick == actualtextbeforclick);

                //    string newtext = actualtextafterclick + TEXTForPost();
                //    EditTextArea.SendKeys(newtext);

                //    try
                //    {
                //        Tickbutton.Click();
                //        Driver.Navigate().Refresh();
                //        WaitUnitElementisClickable(EditButtonOnPost);
                //        actions.MoveToElement(Userpost.ElementAt(i)).Perform();
                //        string UpdatedText = Userpost.ElementAt(i).Text;
                //        if (UpdatedText == newtext)
                //        {
                //            count++;
                //        }
                //    }
                //    catch (Exception)
                //    {
                //        actions.SendKeys(Keys.PageDown).Perform();
                //        Tickbutton.Click();
                //        //Driver.Navigate().Refresh();
                //        WaitUnitElementisClickable(EditButtonOnPost);
                //        //actions.MoveToElement(totalpost.ElementAt(i)).Perform();
                //        string UpdatedText = Userpost.ElementAt(i).Text;
                //        if (UpdatedText == newtext)
                //        {
                //            count++;
                //        }
                       
                //    }
                   
                           

                         
                    
                    





                }
            }
            if (count == userpost-2)
            {
                passcondition = true;
            }

            else { passcondition = false; }

            return passcondition;
        }

        //POST reaction buttons are visible and clickable
        [Obsolete]
        public static Boolean PostReactionsVisibleClickable(IWebElement HeartButton, IWebElement BulbButton, IWebElement CoinButton, IWebElement RocketButton,IWebElement TotalReaction)
        {
            WaitUnitElementisClickable(HeartButton);
            int a=0;
            int b=0;
            
            for (int i = 1; i< 5; i++)
            {
                switch (i)
                {
                    case 1:
                        Assert.True(HeartButton.Displayed);
                        a = Convert.ToInt16(HeartButton.Text);
                        HeartButton.Click();
                        b=Convert.ToInt16(HeartButton.Text);
                        if (a + 1 == b || b-1==a)
                        {
                            liked = true;
                            Assert.True(liked);
                        }
                        break;
                    case 2:
                        Assert.True(BulbButton.Displayed);
                        a = Convert.ToInt16(BulbButton.Text);
                        BulbButton.Click();
                        b = Convert.ToInt16(BulbButton.Text);
                        if (a + 1 == b || b - 1 == a)
                        {
                            liked = true;
                            Assert.True(liked);
                        }
                        break;
                    case 3:
                        Assert.True(CoinButton.Displayed);
                        a = Convert.ToInt16(CoinButton.Text);
                        CoinButton.Click();
                        b = Convert.ToInt16(CoinButton.Text);
                        if (a + 1 == b || b - 1 == a)
                        {
                            liked = true;
                            Assert.True(liked);
                        }
                        break;
                    case 4:
                        Assert.True(RocketButton.Displayed);
                        a = Convert.ToInt16(RocketButton.Text);
                        RocketButton.Click();
                        b = Convert.ToInt16(RocketButton.Text);
                        if (a + 1 == b || b - 1 == a)
                        {
                            liked = true;
                            Assert.True(liked);
                        }
                        break;
                    default:
                        Assert.True(false);
                        break;

                }
            }
            return passcondition = true;
        }

        [Obsolete]
        public static Boolean totalReactionNumber(IWebElement HeartButton, IWebElement BulbButton, IWebElement CoinButton, IWebElement RocketButton, IWebElement TotalReaction)
        {
            WaitUnitElementisClickable(HeartButton);
            int a,b,c,d,e;
            a = Convert.ToInt16(TotalReaction.Text);
            if (a == 0)
            {
                b = Convert.ToInt16(HeartButton.Text);
                c = Convert.ToInt16(BulbButton.Text);
                d = Convert.ToInt16(CoinButton.Text);
                e = Convert.ToInt16(RocketButton.Text);
                Assert.True(a == b + c + d + e);
                return passcondition = true;
            }
            else if (a >= 1)
            {
                b = Convert.ToInt16(HeartButton.Text);
                c = Convert.ToInt16(BulbButton.Text);
                d = Convert.ToInt16(CoinButton.Text);
                e = Convert.ToInt16(RocketButton.Text);
                Assert.True(a == b + c + d + e);
                TotalReaction.Click();
                IList<IWebElement> ReactionTabUsers = Driver.FindElements(By.XPath(" //div[@id='myTabContent']"));
                Assert.True(a == ReactionTabUsers.Count);
                return passcondition = true;
            }
            else { return passcondition; }
        }








        //Path Functions

        public static IWebElement Xpath(string path)
        {
           return Driver.FindElement(By.XPath(path));
        } 
        public static IWebElement IdPath(string path)
        {
           return Driver.FindElement(By.Id(path));
        }
        public static IWebElement Classnamepath(string path)
        {
            return Driver.FindElement(By.ClassName(path));
        }
        public static IWebElement Tagnamepath(string path)
        {
            return Driver.FindElement(By.TagName(path));
        }
    }

}

