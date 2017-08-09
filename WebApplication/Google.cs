using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class Google
    {
        public static void Main (String [] args)
        {
            IWebDriver wd = new ChromeDriver("C:\\Users\\Admin.TRNROOM4-22\\Documents\\Visual Studio 2015\\Projects\\MD_SeleniumC#\\BrowserDrivers\\");
            wd.Manage().Window.Maximize();
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            wd.Navigate().GoToUrl("https://www.google.co.in");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);

            IWebElement textbox = wd.FindElement(By.Id("lst-ib"));
            textbox.SendKeys("syn");
            textbox.SendKeys(Keys.Down);
            System.Threading.Thread.Sleep(300);
            textbox.SendKeys(Keys.Down);
            System.Threading.Thread.Sleep(300);
            textbox.SendKeys(Keys.Down);
            System.Threading.Thread.Sleep(300);
            textbox.SendKeys(Keys.Down);
            System.Threading.Thread.Sleep(300);
            textbox.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(300);
            wd.Quit();
            
        }
    }
}
