using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApplication
{
    class FirefoxProfiling
    {
        public static void Main(string [] args)
        {
            Console.WriteLine("FP Demo");
            FirefoxProfile fp = new FirefoxProfile(@"C:\Users\Admin.TRNROOM4-22\AppData\Roaming\Mozilla\Firefox\Profiles\8b7vqjrg.default");

            fp.SetPreference("network.proxy.type", 1); // manual proxy settings
            fp.SetPreference("network.proxy.http", "localhost"); //specify ip (or localhost)
            fp.SetPreference("network.proxy.http_port", 8080); //specify port

            FirefoxDriverService fds = FirefoxDriverService.CreateDefaultService(@"C:\Users\Admin.TRNROOM4-22\Documents\Visual Studio 2015\Projects\MD_SeleniumC#\BrowserDrivers");
            var ffoptions = new FirefoxOptions();
            ffoptions.Profile = fp;


            IWebDriver wd = new FirefoxDriver(fds, ffoptions, TimeSpan.FromSeconds(10));
            wd.Navigate().GoToUrl("http://www.google.co.in");
        }

    }

}
