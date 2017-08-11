using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;


namespace WebApplication
{
    class GMOOnline2DDF
    {
        static IWebDriver wd = null;
        static IWebElement we = null;
        public static void Main(string[] args)
        {
            openBrowser("chrome");
            readFrmCSV(wd);

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
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wd.Navigate().GoToUrl("http://demo.borland.com/gmopost/");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        private static void readFrmCSV(IWebDriver driver)
        {
            Console.WriteLine("Selenium DataDriver run using CSV file");
            string CSVDataPath = @"C:\Users\Admin.TRNROOM4-22\Documents\Visual Studio 2015\Projects\MD_SeleniumC#\data.csv";
            var reader = new StreamReader(File.OpenRead(CSVDataPath));
            var colheader = reader.ReadLine();
            while (!reader.EndOfStream)
            {
                var billAddress = reader.ReadLine().Split(',');
                gmoHome(driver);
                gmoCatalog(driver);
                gmoPlaceOrder(driver);
                gmoBilling(driver, billAddress);
                gmoReciept(driver);
            }

        }

        private static void gmoReceipt(IWebDriver driver)
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            wd.FindElement(By.Name("bSubmit")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            
        }

        private static void gmoBilling(IWebDriver driver, string[] billAddress)
        {
            we = driver.FindElement(By.Name("billName"));
            inputData(we, billAddress[0]);
            we = driver.FindElement(By.Name("billAddress"));
            inputData(we, billAddress[1]);
            we = driver.FindElement(By.Name("billCity"));
            inputData(we, billAddress[2]);
            we = driver.FindElement(By.Name("billState"));
            inputData(we, billAddress[3]);
            we = driver.FindElement(By.Name("billZipCode"));
            inputData(we, billAddress[4]);
            we = driver.FindElement(By.Name("billPhone"));
            inputData(we, billAddress[5]);
            we = driver.FindElement(By.Name("billEmail"));
            inputData(we, billAddress[6]);

            string selectedCard = selectRandomCard(driver);
            we = driver.FindElement(By.Name("CardNumber"));
            if (String.Compare(selectedCard, "amex", StringComparison.OrdinalIgnoreCase) == 0)
            {
                inputData(we, "123456789012345");
            }
            else
            {
                inputData(we, "1234567890123456");
            }
            we = driver.FindElement(By.Name("CardDate"));
            inputData(we, "12/17");
            driver.FindElement(By.Name("shipSameAsBill")).Click();
            driver.FindElement(By.Name("bSubmit")).Click();

        }

        private static string selectRandomCard(IWebDriver driver)
        {
            int index = new Random().Next(3);
            we = wd.FindElement(By.CssSelector("select option:nth-child(" + (index + 1) + ")"));
            we.Click();
            String value = wd.FindElement(By.Name("CardType")).GetAttribute("value");
            Console.WriteLine("Card Selected : " + value);
            return value;
        }

        private static void inputData(IWebElement we, string data)
        {
            we.Clear();
            we.SendKeys(data);
        }

        private static void gmoPlaceOrder(IWebDriver driver)
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            wd.FindElement(By.XPath("//input[@value='Proceed With Order']")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

        }

        private static void gmoCatalog(IWebDriver driver)
        {
            // Get the page Title printed in console
            Console.WriteLine("Navigated to : " + wd.Title);
            we = wd.FindElement(By.Name("QTY_TENTS"));
            we.Clear();
            we.SendKeys("10");
            resetOrderCatalog();
           
            wd.FindElement(By.XPath("//input[@value='Place An Order']")).Click();
            while (alertPresent()) {
                acceptAlertOnly();
                we.Clear();
                we.SendKeys("2");


            }
            wd.FindElement(By.XPath("//input[@value='Place An Order']")).Click();
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
        }

        private static bool alertPresent()
        {
            try
            {
                IAlert popup = wd.SwitchTo().Alert();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Popup not present " + ex.Message);
                return false;
            }

        }

        private static string acceptAlert()
        {
            IAlert popup = wd.SwitchTo().Alert();
            String popuptext = popup.Text;
            popup.Accept();
            return popuptext;
        }

        private static void acceptAlertOnly()
        {
            IAlert popup = wd.SwitchTo().Alert();
            popup.Accept();
        }

        private static void resetOrderCatalog()
        {
            IWebElement resetButton = wd.FindElement(By.XPath("//input[@name='bReset']"));
            resetButton.Click();
            IList<IWebElement> tablecolmn = wd.FindElements(By.XPath("html/body/form/table/tbody/tr[2]/td/div/center/table/tbody/tr/td[4]/h1/input"));
            int count = 0;
            foreach (IWebElement ordervalue in tablecolmn)
            {
                string noOfItems = ordervalue.GetAttribute("value");
                if (noOfItems.Equals("0"))
                {
                    count++;
                }

            }
            if (count == 6)
            {
                Console.WriteLine("Catalog was reset");
            }
        }

        private static void gmoHome(IWebDriver driver)
        {

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

        private static void readFromExcel(IWebDriver driver)
        {
            Console.WriteLine("Selenium data fdriven using excel");
            string excelDataPath = @"F:\Selenium\TestData\Excel.xlsx";
            Excel.Applicaiton excelData = new Excel.Application();
            Excel.Workbook excelWorkbook = excelData.Workbooks.open(excelDataPath);
            Excel.worksheet excelWorksheet = excelWorkbook.Sheets["gmoBillAddress"];
            Excel.Range excelRange = excelWorksheet.UsedRange;
            int rows = excelRange.Rows.Count;
            int cols = excelRange.Cols.Count;
            string[] billAddress = new string[cols];
            for (int row=2; row<= rows; row++)
            {
                for (int col=1; col<=cols;col++)
                {
                    Console.Write(excelRange.Cells[row, col].Value2.ToString()+" ");
                    billAddress[col - 1] = excelRange.Cells[row, col].Value2.ToString90;
                }
                Console.WriteLine();
                gmoHome(driver);
                gmoCatalog(driver);
                gmoPlaceOrder(driver);
                gmoBilling(driver, billAddress);
                gmoReceipt(driver);

            }
        }

        private static void readfromDB(IWebDriver driver)
        {
            Console.WriteLine("Selenium data driven using DB");
            string conStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source = F:\\Selenium\\Test\\msAccess.ext";
            OleDbConnection con = new OleDbConnection(conStr);
            con.Open();
            OleDbCommand command = new OleDbCommand("SELECT * FROM gmoBillAddress", con);
            OleDbDataReader dataReader = command.ExecuteReader();
            int cols = dataReader.FieldCount;
            string[] billAddress = new string[cols];
            while (dataReader.Read())
            {
                for (int j=1, j < cols; j++)
                {
                    billAddress[j - 1] = dataReader[j].ToString();
                    Console.WriteLine(billAddress[j - 1]);
                }
                gmoHome(driver);
                gmoCatalog(driver);
                gmoPlaceOrder(driver);
                gmoBilling(driver, billAddress);
                gmoReceipt(driver);
            }
            dataReader.Close();
            command.Dispose();
        }

    }


}

