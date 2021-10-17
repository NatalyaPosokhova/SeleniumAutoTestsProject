using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using SeleniumYandexMarketTests.Pages;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TechTalk.SpecFlow;
using NUnit.Framework;
using System.Reflection;

namespace SeleniumYandexMarketTests.StepDefinitions
{
    [Binding]
    public class YandexMarketLaptopsFeatureSteps
    {
        private readonly ScenarioContext _context;
        private IWebDriver _driver;

        private CaptchaPage _captchaPage;
        private YandexMarketHomePage _yandexMarketHomePage;
        private LaptopsPage _laptopsPage;
        
        private Actions _action;
        private readonly ErrorDriver _errorDriver;

        public YandexMarketLaptopsFeatureSteps(ScenarioContext context, ErrorDriver errorDriver)
        {
            _context = context;
            _driver = new ChromeDriver(Assembly.GetExecutingAssembly().Location + "/..");
            _driver.Manage().Window.Maximize();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);

            _captchaPage = new CaptchaPage(_driver);
            _yandexMarketHomePage = new YandexMarketHomePage(_driver);
            _laptopsPage = new LaptopsPage(_driver);
            _action = new Actions(_driver);
            _errorDriver = errorDriver;
        }

        [Given(@"I have navigated to YandexMarket (.*) website")]
        public void GivenIHaveNavigatedToYandexMarketWebsite(string url)
        {
            _errorDriver.TryExecute(() => _driver.Navigate().GoToUrl(url));
            CheckCaptcha();
        }
        
        [Given(@"I transferred on laptops page")]
        public void GivenITransferredOnLaptopsPage()
        {
            _errorDriver.TryExecute(() =>
           {
               _yandexMarketHomePage.BurgerMenu.Click();
               _action.MoveToElement(_yandexMarketHomePage.ComputersSection).Perform();
               _yandexMarketHomePage.LaptopsSection.Click();
           });
            CheckCaptcha();
        }
        
        [When(@"I choose manufacturer (.*)")]
        public void WhenIChooseManufacturer(string manufacturer)
        {
            _context.Add("Manufacturer", manufacturer);
            _errorDriver.TryExecute(() =>
            {
                _laptopsPage.ShowAllBtn.Click();
                _laptopsPage.Input.SendKeys(manufacturer);
                _laptopsPage.Manufacturer.Click();
            });
        }
        
        [When(@"I choose lower price (.*)")]
        public void WhenIChooseLowerPrice(uint priceFrom)
        {
            _context.Add("PriceFrom", priceFrom);
            _errorDriver.TryExecute(() => _laptopsPage.PriceFrom.SendKeys(priceFrom.ToString()));
        }
        
        [When(@"I choose upper price (.*)")]
        public void WhenIChooseUpperPrice(uint priceTo)
        {
            _context.Add("PriceTo", priceTo);
            _errorDriver.TryExecute(() => _laptopsPage.PriceTo.SendKeys(priceTo.ToString()));
        }
        
        [Then(@"I should get laptops according to manufacturer and entered prices")]
        public void ThenIShouldGetLaptopsAccordingToManufacturerAndEnteredPrices()
        {
            Thread.Sleep(5000);
            string[] actualPrices = new string[] { };
            string[] actualTitles = new string[] { };

            _errorDriver.TryExecute(() =>
            {
                actualPrices = _laptopsPage.Prices.
                    Select(price => price.Text).
                    Where(price => !price.Contains("%")).
                    Select(price => Regex.Replace(price, @"\D", "")).ToArray();

                actualTitles = _laptopsPage.Titles.Select(price => price.Text).ToArray();
            });
            var lowerPricesFromElements = actualPrices.Where(price => Convert.ToInt32(price) < _context.Get<uint>("PriceFrom"));
            var upperPricesToElements = actualPrices.Where(price => Convert.ToInt32(price) > _context.Get<uint>("PriceTo"));
            Assert.IsTrue(actualPrices.Length > 0);
            Assert.IsEmpty(lowerPricesFromElements);
            Assert.IsEmpty(upperPricesToElements);
            Assert.IsEmpty(actualTitles.Where(title => !title.Contains(_context.Get<string>("Manufacturer"))));
        }

        [AfterScenario("YandexMarket")]
        public void CleanUp()
        {
            if (_driver != null)
            {
                _driver.Dispose();
            }
        }

        private void CheckCaptcha()
        {
            _errorDriver.TryExecute(() =>
            {
                if (_driver.Url.Contains("showcaptcha?"))
                {
                    _captchaPage.Captcha.Click();
                }
            });
        }
    }
}
