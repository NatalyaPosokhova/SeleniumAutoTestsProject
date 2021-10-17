using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SeleniumYandexMarketTests.Pages;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace SeleniumYandexMarketTests
{
    internal class YandexMarketTests
    {
        private IWebDriver _driver;
        private Actions _action;
        private CaptchaPage _captchaPage;
        private YandexMarketHomePage _yandexMarketHomePage;

        private string _baseURL;
        private string _manufacturer;
        private uint _priceFrom;
        private uint _priceTo;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            _action = new Actions(_driver);
            _yandexMarketHomePage = new YandexMarketHomePage(_driver);
            _captchaPage = new CaptchaPage(_driver);

            _baseURL = "https://market.yandex.ru/";
            _manufacturer = "Lenovo";
            _priceFrom = 25000;
            _priceTo = 30000;
            if (_priceFrom >= _priceTo)
                throw new Exception("Prices are not correct.");
        }

        [TearDown]
        public void CleanUp()
        {
            if (_driver != null)
            {
                _driver.Dispose();
            }
        }

        [Test]
        public void SearchDefiniteCostAndManufacturerLaptopsShouldBeSuccess()
        {
            var laptopsPage = new LaptopsPage(_driver);
            string[] actualPrices = new string[] { };
            string[] actualTitles = new string[] { };

            try
            {
                _driver.Navigate().GoToUrl(_baseURL);

                CheckCaptcha();

                _yandexMarketHomePage.BurgerMenu.Click();
                _action.MoveToElement(_yandexMarketHomePage.ComputersSection).Perform();
                _yandexMarketHomePage.LaptopsSection.Click();

                CheckCaptcha();

                laptopsPage.ShowAllBtn.Click();
                laptopsPage.Input.SendKeys(_manufacturer);
                laptopsPage.Manufacturer.Click();

                laptopsPage.PriceFrom.SendKeys(_priceFrom.ToString());
                laptopsPage.PriceTo.SendKeys(_priceTo.ToString());

                Thread.Sleep(5000);
                actualPrices = laptopsPage.Prices.
                    Select(price => price.Text).
                    Where(price => !price.Contains("%")).
                    Select(price => Regex.Replace(price, @"\D", "")).ToArray();

                actualTitles = laptopsPage.Titles.Select(price => price.Text).ToArray();
            }
            catch (NoSuchElementException ex)
            {
                throw new ($"{ex.Message}");
            }
            catch (WebDriverException)
            {
                throw new ($"Network troubles or page was not loaded timely.");
            }

            var lowerPricesFromElements = actualPrices.Where(price => Convert.ToInt32(price) < _priceFrom);
            var upperPricesToElements = actualPrices.Where(price => Convert.ToInt32(price) > _priceTo);
            Assert.IsTrue(actualPrices.Length > 0);
            Assert.IsEmpty(lowerPricesFromElements);
            Assert.IsEmpty(upperPricesToElements);
            Assert.IsEmpty(actualTitles.Where(title => !title.Contains(_manufacturer)));
        }
        private void CheckCaptcha()
        {
            if (_driver.Url.Contains("showcaptcha?"))
            {
                _captchaPage.Captcha.Click();
            }
        }
    }
}