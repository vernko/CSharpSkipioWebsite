using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SkipioDemoFramework.Pages
{
    public class SkipioHomePage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;
        public readonly SkipioHomePageMap Map;

        public SkipioHomePage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Map = new SkipioHomePageMap(driver);
        }

        public void SearchForSkipio(string term)
        {
            WaitForGoogle();
            Map.GoogleSearchBar.SendKeys(term);
            Map.GoogleSearchBar.SendKeys(Keys.Enter);
            Map.SkipioLink.Click();
            WaitForSkipioPageLoad();
        }

        public void WaitForGoogle()
        {
            _wait.Until((drvr) => Map.GoogleSearchBar.Displayed);
        }

        public void WaitForSkipioPageLoad()
        {
            _wait.Until((drvr) => Map.SignUpNowButton.Displayed);
        }

        public void GotoPricingPage()
        {
            Map.PricingPageLink.Click();
        }

        public void GotoLoginPage()
        {
            Map.LoginPageLink.Click();
        }

        public void PlaySkipioVideo()
        {
            Map.SkipioInfoVideo.Click();
            _wait.Until((drvr) => Map.SkipioVideoPopupWindow.Displayed);
        }

    }

    public class SkipioHomePageMap
    {
        readonly IWebDriver _driver;

        public SkipioHomePageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement GoogleSearchBar => _driver.FindElement(By.Name("q"));
        public IWebElement SkipioLink => _driver.FindElement(By.CssSelector("[class='LC20lb']"));

        public IWebElement SignUpNowButton => _driver.FindElement(By.ClassName("sign-up-nav-btn"));

        public IWebElement PricingPageLink => _driver.FindElement(By.CssSelector("a[href='https://skipio.com/pricing/']"));

        public IWebElement SkipioInfoVideo => _driver.FindElement(By.ClassName("what-is-skipio-play"));
        public IWebElement SkipioVideoPopupWindow => _driver.FindElement(By.ClassName("fancybox-skin"));

        public IWebElement LoginPageLink => _driver.FindElement(By.CssSelector("a[href='https://skipio.com/login/']"));
    }
}