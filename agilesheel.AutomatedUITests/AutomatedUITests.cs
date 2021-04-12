using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using Xunit;

namespace agilesheel.AutomatedUITests
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;

        public AutomatedUITests()
        {
            _driver = new FirefoxDriver();
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [Fact]
        public void Check_If_Homepage_Loads()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/homepage");

            // Assert
            Assert.Equal("Index - agilesheel", _driver.Title);
            Assert.Contains("This week's movies", _driver.PageSource);
        }

        [Fact]
        public void User_Can_Only_Order_When_Logged_in()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Tickets/Create/269");

            // Assert
            Assert.Equal("Login - agilesheel", _driver.Title);
        }

        [Fact]
        public void User_Can_Log_In()
        {
            _driver.Navigate().GoToUrl("http://localhost:5000/Account/Login");

            _driver.FindElement(By.Id("Email")).SendKeys("basic@test.com");
            _driver.FindElement(By.Id("Password")).SendKeys("Test1234!");
            _driver.FindElement(By.Id("Login")).Click();

            // Assert
            Assert.Equal("Index - agilesheel", _driver.Title);
        }
    }
}
