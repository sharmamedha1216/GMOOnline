using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class Expedia
    {
        static IWebDriver wd = null;
        
        public static void Main(String[] args)
        {
            wd = new ChromeDriver("C:\\Users\\Admin.TRNROOM4-22\\Documents\\Visual Studio 2015\\Projects\\MD_SeleniumC#\\BrowserDrivers\\");
            wd.Manage().Window.Maximize();
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wd.Navigate().GoToUrl("https://www.expedia.co.in/");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            IWebElement support = wd.FindElement(By.Id("header-support-menu"));
            support.Click();
            WebDriverWait wait = new WebDriverWait(wd, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("header-support-feedback"))).Click();
            IList<String> windows = wd.WindowHandles;
            int wincount = windows.Count;

            Console.WriteLine("Total " + wincount + " open browsers are in scope of selenium as of now");
            foreach (String windw in windows)
            {
                wd.SwitchTo().Window(windw);
                wd.Manage().Window.Maximize();
                Console.WriteLine("Title of the browser is : " + wd.Title);
                if (wd.Title.Equals("Feedback"))
                {
                    wd.SwitchTo().Window(windw);
                    inputfeedback();
                }

            }
        }
        private static void inputfeedback()
        {
            IWebElement we = wd.FindElement(By.XPath(".//*[@class='bottom-text']/a[contains(text(),'Website')]"));
            we.Click();
            Console.WriteLine("Title of feedback window is : " + wd.Title);
            wd.Manage().Window.Maximize();
            //Input Rating
            int index = new Random().Next(5);
            wd.FindElement(By.Id("rater_" + index)).Click();
            Console.WriteLine("Feedback Rating " + index + " is enetered.");
            int i = new Random().Next(7);
            System.Threading.Thread.Sleep(1000);
            SelectElement sel = new SelectElement (wd.FindElement(By.Id("topic_select")));

           
            sel.SelectByIndex(i);
            we = wd.FindElement(By.Id("comment_box"));
            we.Click();
            we.SendKeys("I am entering this message as feedback in comment box");
            we = wd.FindElement(By.Id("email_address"));
            we.Click();
            we.SendKeys("abc@gmail.com");
            wd.FindElement(By.Id("submit_button")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
           // Console.WriteLine("Title of submitted feedback window is : " + wd.Title);
           // Console.WriteLine(wd.FindElement(By.Id("'tyHeader")).Text);


                   }

    }
}
