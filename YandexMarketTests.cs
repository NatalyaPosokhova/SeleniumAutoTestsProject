using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumYandexMarketTests
{
    internal class YandexMarketTests
    {
        private IWebDriver _driver;
        private string _baseURL;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _baseURL = "https://market.yandex.ru/";
        }

        [TearDown]
        public void CleanUp()
        {
            
        }

        [Test]
        public void SearchDefiniteCostLaptopsOnYandexMarket()
        {
            
        }       
    }
}