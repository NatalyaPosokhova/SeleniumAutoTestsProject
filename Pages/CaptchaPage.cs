using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumYandexMarketTests.Pages
{
    /// <summary>
    /// Captcha, appears when trying to load main yandex page by selenium.
    /// </summary>
    internal class CaptchaPage
    {
        private IWebDriver _driver;
        public CaptchaPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(_driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = ".CheckboxCaptcha-Button")]
        [CacheLookup]
        public IWebElement Captcha { get; set; }
    }
}
