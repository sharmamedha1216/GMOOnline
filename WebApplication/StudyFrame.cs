using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class StudyFrameJavaDoc
    {
        public static void Main(String [] args)
        {
            IWebDriver wd = new ChromeDriver("C:\\Users\\Admin.TRNROOM4-22\\Documents\\Visual Studio 2015\\Projects\\MD_SeleniumC#\\BrowserDrivers");
            wd.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wd.Manage().Window.Maximize();
            wd.Navigate().GoToUrl("http://seleniumhq.github.io/selenium/docs/api/java/index.html");
            wd.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            IList<IWebElement> frameList = wd.FindElements(By.TagName("frame"));
            Console.WriteLine("#Frames: " + frameList.Count);
           // Console.Write("Frame Names are : ");
            foreach (IWebElement ele in frameList)
            {
                String frameName = ele.GetAttribute("name");
                Console.WriteLine(frameName + " ");
                wd.SwitchTo().Frame(frameName);
                IList<IWebElement> links = wd.FindElements(By.TagName("a"));
                int numoflinks = links.Count;
                Console.WriteLine("Total Number of oblects in selected frame are : " + numoflinks);
                foreach (IWebElement elem in links)
                {
                    Console.WriteLine(elem.Text);
                }
                wd.SwitchTo().DefaultContent();
            }
            Console.WriteLine();
        }

    }
}
