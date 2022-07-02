using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.IO;
using System.Threading;

namespace Appuim
{
    public class SmmatorAppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private WindowsDriver<WindowsElement> desktopDriver;
        private const string AppiumSurverUri = "http://[::1]:4723/wd/hub";
        private const string AppPath = @"C:\Program Files\7-Zip\";
        private const string ZipAppPath = @"C:\Program Files\7-Zip\7zFM.exe";
        private string workDir;


        [OneTimeSetUp]
        public void Setup()
        {

            var appiumOptions = new AppiumOptions() { PlatformName = "Windows" };
            var appiumOptionsDesktop = new AppiumOptions() { PlatformName = "Windows" };
            appiumOptionsDesktop.AddAdditionalCapability("app", "Root");
            desktopDriver = new WindowsDriver<WindowsElement>(new Uri(AppiumSurverUri), appiumOptionsDesktop);
            appiumOptions.AddAdditionalCapability("app", ZipAppPath);
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumSurverUri), appiumOptions);




            workDir = Directory.GetCurrentDirectory() + @"\7zFM.exe";
            if (Directory.Exists(workDir))
                Directory.Delete(workDir, true);
            Directory.CreateDirectory(workDir);
        }


        [OneTimeTearDown]
        public void TearDown()
        {

            driver.CloseApp();
            desktopDriver.CloseApp();
            driver.Quit();
            desktopDriver.Quit();
        }
        [Test]
        public void Two_Zips()
        {
            var textboxLocationFolder = driver.FindElementByXPath
            ("/Window/Pane/Pane/ComboBox/Edit");

            textboxLocationFolder.SendKeys(@"C:\Program Files\7-Zip\" + Keys.Enter);

            var listBoxfiles = driver.FindElementByClassName("SysListView32");
            listBoxfiles.SendKeys(Keys.Control + "a");

            var buttonAdd = driver.FindElementByName("Add");
            buttonAdd.Click();

            Thread.Sleep(500);
            var windowAddtoArchive = desktopDriver.FindElementByName("Add to Archive");

            var textBoxArchiveName = windowAddtoArchive.FindElementByAccessibilityId("1001");
            textBoxArchiveName.Clear();
            string archiveFilename = workDir + "\\" + DateTime.Now.Ticks +@"\7zFM.exe";
            textBoxArchiveName.SendKeys(archiveFilename);


            var comboArchiveFormat = windowAddtoArchive.FindElementByAccessibilityId("104");
            comboArchiveFormat.SendKeys("7z");

            var comboCompresionlevel = windowAddtoArchive.FindElementByAccessibilityId("102");
            comboCompresionlevel.SendKeys("Ultra");

            var comboDictionarysize = windowAddtoArchive.FindElementByAccessibilityId("107");
            comboDictionarysize.SendKeys(Keys.End);

            var comboWordsize = windowAddtoArchive.FindElementByAccessibilityId("108");
            comboWordsize.SendKeys(Keys.End);

            var buttonOkcreateArchive = windowAddtoArchive.FindElementByAccessibilityId("1");
            buttonOkcreateArchive.Click();

            Thread.Sleep(3000);

            string originalZip = @"C:\Program Files\7-Zip\7zFM.exe";
            string extractedZip = workDir + @"\7zFM.exe";
            FileAssert.AreEqual(extractedZip, originalZip);


        }
    }
}