using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SkipioDemoFramework.Pages
{
    public class LoginPage
    {
        readonly IWebDriver _driver;
        readonly WebDriverWait _wait;
        public readonly LoginPageMap Map;

        public LoginPage(IWebDriver driver, WebDriverWait wait)
        {
            _driver = driver;
            _wait = wait;
            Map = new LoginPageMap(driver);
        }

        public void Login(string name, string password)
        {
            Map.GotoLoginButton.Click();
           
            WaitForLoginPageLoad();

            Map.UsernameField.SendKeys(name);
            Map.PasswordField.SendKeys(password);
            Map.TermsCheckbox.Click();
            Map.SubmitLoginButton.Click();
        }

        public void WaitForPageLoad()
        {
            _wait.Until((drvr) => Map.GotoLoginButton.Displayed);
        }

        public void WaitForLoginPageLoad()
        {
            _wait.Until((drvr) => Map.UsernameField.Displayed);
        }

        public void WaitForSuccessCenterLoad()
        {
            _wait.Until((drvr) => Map.SkipioLink.Displayed);
        }
    }

    public class LoginPageMap
    {
        readonly IWebDriver _driver;

        public LoginPageMap(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement GotoLoginButton => _driver.FindElement(By.CssSelector("[class='skipio-new-common-btn btn-xlg btn']"));

        public IWebElement UsernameField => _driver.FindElement(By.CssSelector("[name='user[email]']"));
        public IWebElement PasswordField => _driver.FindElement(By.CssSelector("[name='user[password]']"));
        public IWebElement TermsCheckbox => _driver.FindElement(By.CssSelector("[for='user_agreed_to_terms']"));
        public IWebElement RememberMeCheckbox => _driver.FindElement(By.CssSelector("[for='user_remember_me']"));
        public IWebElement SubmitLoginButton => _driver.FindElement(By.CssSelector("[name='commit']"));

        public IWebElement InvalidLogin => _driver.FindElement(By.CssSelector("li[class='alert alert-warning']"));
        public IWebElement SuccessCenterHeader => _driver.FindElement(By.CssSelector("[class^='css-15wtjl4']"));
        public IWebElement SkipioLink => _driver.FindElement(By.CssSelector("[class='navbar-brand']"));
    }
}