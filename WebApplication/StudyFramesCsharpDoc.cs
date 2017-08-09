using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class StudyFrameCSharp
    {
        public static void Main(String[] args)
        {
            IWebDriver wd = new ChromeDriver("C:\\Users\\Admin.TRNROOM4-22\\Documents\\Visual Studio 2015\\Projects\\MD_SeleniumC#\\BrowserDrivers");
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wd.Manage().Window.Maximize();
            wd.Navigate().GoToUrl("http://seleniumhq.github.io/selenium/docs/api/dotnet/index.html");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IList<IWebElement> objectList = wd.FindElements(By.XPath(".//*[@id='divTree']/div/a"));
            Console.WriteLine("Number of links in top windoe are : " + objectList.Count);
            foreach (IWebElement links in objectList)
            {
              Console.WriteLine(links.Text);
            }

            IWebElement frameList = wd.FindElement(By.TagName("iframe"));
            String frameName = frameList.GetAttribute("name");
            Console.WriteLine("FrameName: " + frameName);
            wd.SwitchTo().Frame(frameName);
            IList<IWebElement> tableheader = wd.FindElements(By.XPath(".//*[@id='mainBody']/div/div[1]"));
            IList<IWebElement> tables = wd.FindElements(By.XPath(".//*[@id='typeList']/tbody"));
            int tablecount = tables.Count;
            Console.WriteLine("Number of Tables : " + tablecount);
            int i = 0;
            foreach (IWebElement table in tables)
            {
                Console.WriteLine(".................");
                Console.WriteLine(tableheader[i].Text);
                Console.WriteLine(".................");
                Console.WriteLine();
                IList <IWebElement> rows = table.FindElements(By.TagName("tr"));
                int rowcount = rows.Count;
                Console.WriteLine("Number of Rows in below table are : "+ rowcount);
                Console.WriteLine();
                    IList<IWebElement> headerdata = table.FindElements(By.XPath("tr[1]/th"));
                    foreach (IWebElement headervalue in headerdata)
                    {
                        Console.Write(headervalue.Text + " || ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("================================================");
                    for (int j = 2; j <= rowcount; j++)
                    {
                        IList<IWebElement> data = table.FindElements(By.XPath("tr[2]/td"));
                        for (int k = 1; k <= 3; k++)
                        {
                            if (k == 1)
                            {
                                Console.Write("Image || ");
                            }
                            else if (k == 2)
                            {
                                Console.Write(table.FindElement(By.XPath("tr[" + j + "]/td/a")).Text + " || ");
                            }
                            else
                            {
                                Console.WriteLine(table.FindElement(By.XPath("tr[" + j + "]/td/div")).Text);
                            }

                        }
                        Console.WriteLine("================================================");
                    }
               i++;
            }
        }

    }
}
