using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAGApplication
{
    class EAGApp 
    {
        static IWebDriver driver = null;
        static IWebElement element = null;

        static void Main(string[] args)
        {
            openBrowser("ff");
            loginApp();
            navigateTo("appname");
            logoutApp();
        }

        private static void navigateTo(String appName)
        {
            IList<IWebElement> elements =  driver.FindElements(By.XPath("//ul[@class='navigation-list']/li"));
            foreach (IWebElement ele in elements) {
                //if()
            }
        }

        private static void logoutApp()
        {
            element = driver.FindElement(By.XPath("//div[@title='Logout']"));
            element.Click();
            element = driver.FindElement(By.LinkText("Logout"));
            element.Click();
        }

        private static String getWebPageTitle()
        {
            //Get Title of the page
            //Console.WriteLine("Title of Page is : " + driver.Title);
            return driver.Title;

        }

        private static void openBrowser(string browserName)
        {
            String driverpath = @"C:\Users\Admin.TRNROOM4-22\Documents\Visual Studio 2015\Projects\MD_SeleniumC#\BrowserDrivers";
            if (browserName.Contains("ff"))
            {
                FirefoxDriverService fds = FirefoxDriverService.CreateDefaultService(driverpath);
                driver = new FirefoxDriver(fds);
            }
            else if (browserName.Contains("ch"))
            {
                driver = new ChromeDriver(driverpath);
            }
            else if (browserName.Contains("ie"))
            {
                driver = new InternetExplorerDriver(driverpath);
            }
            else
            {
                throw new Exception("Browser not found");
            }
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://eag.synechron.com/");
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Console.WriteLine(getWebPageTitle());
        }

        private static void loginApp()
        {
            element = driver.FindElement(By.Id("UserName"));
            //Console.Write("Enter User Name : ");
            //String username = Console.ReadLine();
            element.SendKeys("Medha.Sharma");

            element = driver.FindElement(By.Id("Password"));
            //Console.Write("Enter valid password : ");
            //String passwd = Console.ReadLine();
            element.SendKeys("H@nuli-1216");
            try {
            element = driver.FindElement(By.XPath("//input[@value='Sign In']"));
            element.Click();
           // driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
             }
            catch(WebDriverTimeoutException timexc)
            {
                Console.WriteLine("Exception occurred : " + timexc.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine(getWebPageTitle());
            }
            

        }
    }
}
