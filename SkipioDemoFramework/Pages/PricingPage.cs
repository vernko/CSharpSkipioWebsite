using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace SkipioDemoFramework.Pages
{
    public class PricingPage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;
        public readonly PricingPageMap Map;

        public PricingPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Map = new PricingPageMap(driver);
        }

        public void CalculateROI(string closedsales, string avgsalesize, int optionIndex)
        {
            WaitForROICalculateLoad();
            Map.RoiLetsGoButton.Click();

            //Number of Closed Sales Input
            WaitForClosedSalesLoad();
            Map.ClosedSalesField.SendKeys(closedsales);
            Map.NextButton.Click();

            //Average Sale Size Input
            WaitForAverageSaleSizeLoad();
            Map.AverageSaleSizeField.SendKeys(avgsalesize);
            Map.SecondNextButton.Click();

            //Industry Select Dropdown
            WaitForIndustrySelectLoad();
            var select = new SelectElement(Map.IndustryDropdown);
            select.SelectByIndex(optionIndex);

            ////Communication Checkboxes
            //WaitForCommunicationFrameLoad();
            //Map.CommunicationCheckBoxes.Click();
            //Map.CommunicationNextButton.Click();
            //Map.SkipLink.Click();
        }

        public void OpenCalculateRoiFrame()
        {
            Map.LetsGoButton.Click();
        }

        public void WaitForAverageSaleSizeLoad()
        {
            _wait.Until((drvr) => Map.AverageSaleSizeField.Displayed);
        }

        public void WaitForClosedSalesLoad()
        {
            _wait.Until((drvr) => Map.ClosedSalesField.Displayed);
        }

        public void WaitForCommunicationFrameLoad()
        {
            _wait.Until((drvr) => Map.CommunicationCheckBoxes.Displayed);
        }

        public void WaitForIndustrySelectLoad()
        {
            _wait.Until((drvr) => Map.IndustryDropdown.Displayed);
        }

        public void WaitForPageLoad()
        {
            _wait.Until((drvr) => Map.LetsGoButton.Displayed);
        }

        public void WaitForResultsLoad()
        {
            _wait.Until((drvr) => Map.GetStartedButton.Displayed);
        }

        public void WaitForROICalculateLoad()
        {
             _wait.Until((drvr) => Map.RoiLetsGoButton.Displayed);
        }
    }

    public class PricingPageMap
    {
        readonly IWebDriver _driver;

        public PricingPageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement LetsGoButton => _driver.FindElement(By.CssSelector("[class='outgrow-b']"));
        public IWebElement RoiLetsGoButton => _driver.FindElement(By.CssSelector("[class^='btn']"));

        public IWebElement ClosedSalesField => _driver.FindElement(By.CssSelector("[class^='postfixset']"));
        public IWebElement AverageSaleSizeField => _driver.FindElement(By.XPath("(//input[@placeholder='--'])[last()]"));
        public IWebElement NextButton => _driver.FindElement(By.CssSelector("button[class*='submit-textbox-btn']"));
        public IWebElement SecondNextButton => _driver.FindElement(By.XPath("(//button[contains(@class, 'submit-textbox-btn')])[last()]"));

        public IWebElement IndustryDropdown => _driver.FindElement(By.CssSelector("div[class*='selectize-input']"));

        //may need to get a list
        public IWebElement CommunicationCheckBoxes => _driver.FindElement(By.CssSelector("input[class='filled-in']"));
        public IWebElement CommunicationNextButton => _driver.FindElement(By.CssSelector("[class='submit-checkbox-img-custom']"));

        ////Experience Skipio form from ROI Results
        //public IWebElement NameField => _driver.FindElement(By.Id("icon_prefix0"));
        //public IWebElement EmailField => _driver.FindElement(By.Id("icon_prefix1"));
        //public IWebElement PhoneNumberField => _driver.FindElement(By.Id("icon_prefix2"));
        //public IWebElement TextMeButton => _driver.FindElement(By.CssSelector("[data-text='Text me!']"));
        public IWebElement SkipLink => _driver.FindElement(By.ClassName("lf-skip-btn"));

        //Results
        public IWebElement GetStartedButton => _driver.FindElement(By.CssSelector("[data-text='Get Started With Skipio!']"));
    }
}