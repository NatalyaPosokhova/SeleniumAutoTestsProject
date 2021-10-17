using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumYandexMarketTests.Pages
{
    /// <summary>
    /// Yandex Market Main Page
    /// </summary>
    internal class YandexMarketHomePage
    {
        private IWebDriver _driver;
        public YandexMarketHomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//*[@data-apiary-widget-name='@MarketNode/HeaderCatalogEntrypoint']/*/*[@type='button']")]
        [CacheLookup]
        public IWebElement BurgerMenu { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-zone-name='category-link']/*/*[@class='_1UCDW' and text()='Компьютеры']")]
        [CacheLookup]
        public IWebElement ComputersSection { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@data-autotest-id='subItems']/*/*/*[text()='Ноутбуки']")]
        [CacheLookup]
        public IWebElement LaptopsSection { get; set; }
    }
}
