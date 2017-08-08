using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication
{
    class Program
    {
        static IWebDriver wd = null;
        static IWebElement we = null;
        static void Main(string[] args)
        {
            openBrowser("chrome");
            homePage();
            onlineCataloguePage();
            placeorderPage();
            billingInfoPage();
            onlineStoreReciept();
            wd.Quit();
            Console.ReadKey(true);
            
        }

        private static void onlineStoreReciept()
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            wd.FindElement(By.Name("bSubmit")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
           
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
        }

        private static void billingInfoPage()
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);

            //Input the Bill To details
            wd.FindElement(By.Name("billName")).SendKeys("M D Sharma");
            wd.FindElement(By.Name("billAddress")).SendKeys(" Address Line 1");
            wd.FindElement(By.Name("billCity")).SendKeys("City 1");
            wd.FindElement(By.Name("billState")).SendKeys("State 2");
            wd.FindElement(By.Name("billZipCode")).SendKeys("78945");
            wd.FindElement(By.Name("billPhone")).SendKeys("9042450075");
            wd.FindElement(By.Name("billEmail")).SendKeys("abc@mail.com");
           // selectCard();
            if (selectCard().Equals("Amex"))
            {
                wd.FindElement(By.Name("CardNumber")).SendKeys("123456789123456");
            }
            else
            {
                wd.FindElement(By.Name("CardNumber")).SendKeys("1234567891234567");
            }
            wd.FindElement(By.Name("CardDate")).SendKeys("12/20");

            // Input the Ship To details
            we = wd.FindElement(By.Name("shipSameAsBill"));
            String toShipChkbox = "unChecked";
            if(toShipChkbox.Equals("Checked"))
            {
                we.Click();
            }
            
            if (we.Selected)
            {
                Console.WriteLine("To Bill details are Same as To Ship details");
            }
            else
            {
                Console.WriteLine("Entering the To Ship Details");
                wd.FindElement(By.Name("shipName")).SendKeys("K P Joshi");
                wd.FindElement(By.Name("shipAddress")).SendKeys("AddressLine2");
                wd.FindElement(By.Name("shipCity")).SendKeys(" City 3");
                wd.FindElement(By.Name("shipState")).SendKeys("State 4");
                wd.FindElement(By.Name("shipZipCode")).SendKeys("12345");
                wd.FindElement(By.Name("shipPhone")).SendKeys("9876543210");

                saveScreenShot("billingInfo");
            }
            

            // Click on "Place The Order" button
            wd.FindElement(By.Name("bSubmit")).Click();



        }

        private static String selectCard()
        {
            int index = new Random().Next(3);
            we = wd.FindElement(By.CssSelector("select option:nth-child(" + (index + 1) + ")"));
            we.Click();
            String value = wd.FindElement(By.Name("CardType")).GetAttribute("value");
            Console.WriteLine("Card Selected : " + value);
            return value;
        }

        private static void placeorderPage()
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            wd.FindElement(By.XPath("//input[@value='Proceed With Order']")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

        }

        private static void onlineCataloguePage()

        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            we = wd.FindElement(By.Name("QTY_TENTS"));
            we.Clear();
            we.SendKeys("2");
            IWebElement we1 = wd.FindElement(By.Name("QTY_BACKPACKS"));
            we1.Clear();
            we1.SendKeys("10");
            saveScreenShot("onlineCatalogue");
            getTableData();
            wd.FindElement(By.XPath("//input[@value='Place An Order']")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        private static void getTableData()
        {
            IWebElement table = wd.FindElement(By.XPath("html/body/form/table/tbody/tr[2]/td/div/center/table/tbody"));
            int rows = table.FindElements(By.TagName("tr")).Count;
            int col = table.FindElements(By.XPath("tr[1]/td")).Count;

            for (int i=1; i<= rows; i++) {
                for (int j=1; j<=col; j++)
                {
                    IWebElement tablecell = table.FindElement(By.XPath("tr[" + i + "]/td[" + j + "]"));
                    String celldata = tablecell.Text;
                    if(celldata.Equals(""))
                    {
                        IWebElement orderQnty = tablecell.FindElement(By.XPath("h1/input"));
                        celldata = orderQnty.GetAttribute("name") + "=" + orderQnty.GetAttribute("value");
                    }
                    Console.Write(celldata + " " );
                    Console.WriteLine();
                }
                Console.WriteLine("************************************");
            }


            //Console.WriteLine("rows : " + rows + "Cols :" + col);
        }

        private static void homePage()
        {
            Console.WriteLine("Navigated to : "+ wd.Title);
            // Find Element About the GMO Site
            we = wd.FindElement(By.Name("bAbout"));
            we.Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            // Navigate Back to homePage    
            wd.Navigate().Back();
        //****************************************************************************
            // Find Element Browser Test Page
            we = wd.FindElement(By.XPath("//input[@value = 'Browser Test Page']"));
            we.Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            // Navigate Back to homePage   
            wd.Navigate().Back();
        //****************************************************************************
            // Find Element Enter GMO Online
            we = wd.FindElement(By.Name("bSubmit"));
            we.Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        private static void openBrowser(string browserName)
        {
            String driverpath = @"C:\Users\Admin.TRNROOM4-22\Documents\Visual Studio 2015\Projects\MD_SeleniumC#\BrowserDrivers";
            if (browserName.Contains("ff"))
            {
                FirefoxDriverService fds = FirefoxDriverService.CreateDefaultService(driverpath);
                wd = new FirefoxDriver(fds);
            }
            else if (browserName.Contains("ch"))
            {
                wd = new ChromeDriver(driverpath);
            }
            else if (browserName.Contains("ie"))
            {
                wd = new InternetExplorerDriver(driverpath);
            }
            else
            {
                throw new Exception("Browser not found");
            }
            wd.Manage().Window.Maximize();
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            wd.Navigate().GoToUrl("http://demo.borland.com/gmopost/");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        private static void saveScreenShot(String screenshotFirstName)
        {
            var folderLocation = @"C:\Users\Admin.TRNROOM4-22\Documents\visual studio 2015\Projects\SyneAug2017\Screenshots\";
            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
            var screenImage = ((ITakesScreenshot)wd).GetScreenshot();
            var fileName = new StringBuilder(folderLocation);
            fileName.Append(screenshotFirstName + DateTime.Now.ToString("-yyyy-mm-dd-HH_mm_ss")+".png");
            screenImage.SaveAsFile(fileName.ToString(), ScreenshotImageFormat.Png);
            Console.WriteLine("Screenshot is taken please check at : " + fileName);
        }
    }

}
