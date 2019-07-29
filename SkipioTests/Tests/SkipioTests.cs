using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SkipioDemoFramework.Pages;

namespace SkipioTests.Tests
{
    [TestClass]
    public class SkipioTests
    {
        IWebDriver Driver;
        WebDriverWait Wait;

        [TestInitialize]
        public void Setup()
        {
            Driver = new ChromeDriver(@"/Users/vernkofford/Projects/SkipioDemo");
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Quit();
            Driver.Dispose();
        }

        [TestMethod]
        public void User_can_watch_Skipio_info_video()
        {
            Driver.Navigate().GoToUrl("https://skipio.com/");

            var video = new SkipioHomePage(Driver, Wait);
            video.WaitForSkipioPageLoad();
            video.PlaySkipioVideo();
            Assert.IsTrue(video.Map.SkipioVideoPopupWindow.Displayed);
        }

        [TestMethod]
        public void User_can_calculate_ROI()
        {
            Driver.Navigate().GoToUrl("https://skipio.com/");

            var home = new SkipioHomePage(Driver, Wait);
            home.WaitForSkipioPageLoad();
            home.GotoPricingPage();

            var roi = new PricingPage(Driver, Wait);
            roi.WaitForPageLoad();
            roi.OpenCalculateRoiFrame();

            var iframe = Driver.FindElement(By.CssSelector("[id^='popup-iframe']"));
            Driver.SwitchTo().Frame(iframe);

            roi.CalculateROI("11", "1000", 1);
            Assert.IsTrue(roi.Map.IndustryDropdown.Displayed);
        }

        [TestMethod]
        public void User_can_login_with_valid_credentials()
        {
            Driver.Navigate().GoToUrl("https://www.google.com/");

            var home = new SkipioHomePage(Driver, Wait);
            home.SearchForSkipio("skipio");
            home.GotoLoginPage();

            var login = new LoginPage(Driver, Wait);
            login.WaitForPageLoad();
            login.Login("tyler.dallinga@gmail.com", "Skipio123!");
            login.WaitForSuccessCenterLoad();
            Assert.IsTrue(login.Map.SkipioLink.Displayed);
        }

        [TestMethod]
        public void User_cant_login_without_valid_credential()
        {
            Driver.Navigate().GoToUrl("https://skipio.com/");

            var home = new SkipioHomePage(Driver, Wait);
            home.WaitForSkipioPageLoad();
            home.GotoLoginPage();

            var login = new LoginPage(Driver, Wait);
            login.WaitForPageLoad();
            login.Login("support@bottega.tech", "test1234!");
            Assert.IsTrue(login.Map.InvalidLogin.Displayed);
        }
    }
}