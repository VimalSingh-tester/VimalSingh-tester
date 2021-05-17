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

namespace Selenium.Build.Components
{
    class BMC:Baseclass
    {
        public BMC()
        {
            PageFactory.InitElements(Driver, this);
        }
        //Build and under build buttons

        #region Build Buttons

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Build')]")]

        public IWebElement BuildButton{ get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Business Model Canvas')]")]
        public IWebElement BMCButton { get; set; }

        [FindsBy(How = How.XPath, Using = "	//span[contains(text(),'Business Plan')]")]
        public IWebElement BusinessPlanButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Startup Runway')]")]
        public IWebElement StartupRunwayButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Cap Table')]")]
        public IWebElement CapTableButton { get; set; }

        #endregion

        #region Bmc buttons 

        [FindsBy(How = How.XPath, Using = "//a[@class='btn btn-sm btn-secondary btn-hover-secondary']")]
        public IWebElement BMCCreateNewButton { get; set; }

        [FindsBy(How = How.XPath, Using = " //div[@class='d-flex']//input")]
        public IWebElement BMCTitleText { get; set; }

        [FindsBy(How = How.XPath, Using = "//select[@id='eventList'])]
        public IWebElement BMCSelectEvent { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[contains(text(),'Submit')]")]
        public IWebElement BMCSubmit { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='ql-editor ql-blank']")]
        public IWebElement BMCAllDetailText { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-1']//div")]
        public IWebElement BMCKeyPartnersText  { get; set; }

       
        [FindsBy(How = How.XPath, Using = "//div[@id='editor-2']")]
        public IWebElement BMCKeyActivities  { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-3']")]
        public IWebElement BMCKeyResources { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-4']")]
        public IWebElement BMCValueProposition  { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-5']")]
        public IWebElement BMCCustomerRelationship  { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-6']")]
        public IWebElement BMCChannnels { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-7']")]
        public IWebElement BMCCustomersSegments  { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-8']")]
        public IWebElement BMCCostStructure  { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='editor-9']")]
        public IWebElement BMCRevenueStreams { get; set; }

        #endregion
        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement profilebuttononfeed { get; set; }

        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement profilebuttononfeed { get; set; }

        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement profilebuttononfeed { get; set; }

        //[FindsBy(How = How.XPath, Using = "")]

        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement profilebuttononfeed { get; set; }

        //[FindsBy(How = How.XPath, Using = "")]

        //[FindsBy(How = How.XPath, Using = "")]
        //public IWebElement profilebuttononfeed { get; set; }
        


    }
}
