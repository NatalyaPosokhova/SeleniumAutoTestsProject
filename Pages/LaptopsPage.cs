using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SeleniumYandexMarketTests.Pages
{
    internal class LaptopsPage
    {
        private IWebDriver _driver;
        public LaptopsPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@data-autotest-id='7893318']/footer/button[text()='Показать всё']")]
        [CacheLookup]
        public IWebElement ShowAllBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='_1JYTt']")]
        [CacheLookup]
        public IWebElement Input { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-autotest-id='7893318']/ul[@class='_2y67x']/*/*/*/*/*/span")]
        [CacheLookup]
        public IWebElement Manufacturer { get; set; }

        [FindsBy(How = How.Id, Using = "glpricefrom")]
        [CacheLookup]
        public IWebElement PriceFrom { get; set; }

        [FindsBy(How = How.Id, Using = "glpriceto")]
        [CacheLookup]
        public IWebElement PriceTo { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-zone-name='price']/*/a/*/span")]
        [CacheLookup]
        public IList<IWebElement> Prices { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-zone-name='title']/*/span")]
        [CacheLookup]
        public IList<IWebElement> Titles { get; set; }
        
    }
}
