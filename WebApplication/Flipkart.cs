using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class Flipkart
    {
        public static void Main(String [] args)
        {
            IWebDriver wd = null;
            
            wd = new ChromeDriver("C:\\Users\\Admin.TRNROOM4-22\\Documents\\Visual Studio 2015\\Projects\\MD_SeleniumC#\\BrowserDrivers\\");
            wd.Manage().Window.Maximize();
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wd.Navigate().GoToUrl("https://www.flipkart.com/");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            Actions mouse_actions = new Actions(wd);
            IWebElement booksandMore = wd.FindElement(By.XPath(".//*[@title='Books & More']"));
            mouse_actions.MoveToElement(booksandMore).Perform();

            WebDriverWait wait = new WebDriverWait(wd, TimeSpan.FromSeconds(10));
            var element =  wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(".//*[@title='Indian Writing']")));
            mouse_actions.MoveToElement(element).Click().Perform();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            System.Threading.Thread.Sleep(1000);
            wd.Quit();
            Console.WriteLine("Action performed");

        } 
    }
}
