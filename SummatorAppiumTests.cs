using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appuim
{
    public class SmmatorAppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumSurverUri = "http://[::1]:4723/wd/hub";
        private const string SummatorAppPath = @"C:\Appium\SummatorDesktopApp.exe";

        [OneTimeSetUp]
        public void Setup()
        {
            var appiumOptions = new AppiumOptions() { PlatformName = "Windows"};
            appiumOptions.AddAdditionalCapability("app", SummatorAppPath);
            driver = new WindowsDriver<WindowsElement>(new Uri(AppiumSurverUri), appiumOptions);  
        }
        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void Sum_of_Two_numbers()
        {
            var field1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
                field1.Clear();
            field1.SendKeys("7.5");

            var field2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            field2.Clear();
            field2.SendKeys("7.5");

            var calcbutton = driver.FindElementByAccessibilityId("buttonCalc");
            calcbutton.Click();

            var resultfield = driver.FindElementByAccessibilityId("textBoxSum");
           

            Assert.That(resultfield.Text, Is.EqualTo("15.0"));

        }
        [Test]
        public void Sum_of_Two_numbers_Invailid()
        {
            var field1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            field1.Clear();
            field1.SendKeys("7.5");

            var field2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            field2.Clear();
            field2.SendKeys("hey");

            var calcbutton = driver.FindElementByAccessibilityId("buttonCalc");
            calcbutton.Click();

            var resultfield = driver.FindElementByAccessibilityId("textBoxSum");


            Assert.That(resultfield.Text, Is.EqualTo("error"));

        }
        [Test]
        public void Sum_of_number_and_empty_field()
        {
            var field1 = driver.FindElementByAccessibilityId("textBoxFirstNum");
            field1.Clear();
            field1.SendKeys("7.5");

            var field2 = driver.FindElementByAccessibilityId("textBoxSecondNum");
            field2.Clear();
            field2.SendKeys("");

            var calcbutton = driver.FindElementByAccessibilityId("buttonCalc");
            calcbutton.Click();

            var resultfield = driver.FindElementByAccessibilityId("textBoxSum");


            Assert.That(resultfield.Text, Is.EqualTo("error"));

        }
    }
}