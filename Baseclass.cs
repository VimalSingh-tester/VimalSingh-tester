using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using System.IO;
using Xunit;


namespace Selenium
{
    public class Baseclass
    {

        public static IWebDriver Driver { get; set; }
        public static FirefoxOptions options { get; set; }
        public static string Url { get; set; }
        public static IConfiguration Configuration { get; set; }

        public Baseclass()
        {

            Directory.SetCurrentDirectory("C:/Workspace/bigapple/Tests/Selenium");
            var curDir = Directory.GetCurrentDirectory();
             Configuration = new ConfigurationBuilder().AddJsonFile(curDir + "/" + "appsettings.json").Build();

        }

        public static IWebDriver Initial()              
        {
            Baseclass Baseclassconstructor = new Baseclass();
            Url = Configuration.GetSection("MyData").GetSection("url").Value;            
            Driver = new FirefoxDriver();
            
            Driver.Manage().Window.Maximize();
            Driver.Navigate().GoToUrl(Url);
            Driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(20);
            return Driver;
        }
        public static void ChromeHandling()
        {
            ChromeOptions options = new ChromeOptions();
           
        }

    }
}
