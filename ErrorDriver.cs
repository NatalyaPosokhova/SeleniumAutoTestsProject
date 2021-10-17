using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SeleniumYandexMarketTests
{
    public class ErrorDriver
    {
        private readonly Queue<Exception> _exceptions = new Queue<Exception>();

        public void TryExecute(Action act)
        {
            try
            {
                act();
            }
            catch (NoSuchElementException ex)
            {
                Trace.WriteLine($"{act.Method.Name}: {ex}");
                _exceptions.Enqueue(ex);
            }
            catch (WebDriverException ex)
            {
                Trace.WriteLine($"{act.Method.Name}: {ex}. Network troubles or page was not loaded timely.");
                _exceptions.Enqueue(ex);
            }
            catch (Exception ex)
            {
                Trace.WriteLine($"The following exception was caught while executing {act.Method.Name}: {ex}");
                _exceptions.Enqueue(ex);
            }
        }

        public void AssertExceptionWasRaisedWithMessage(string expectedErrorMessage)
        {
            Assert.IsTrue(_exceptions.Any(), $"No exception was raised but expected exception with message: {expectedErrorMessage}");

            var actualException = _exceptions.Dequeue();
            Assert.AreEqual(expectedErrorMessage, actualException.Message);
        }

        public void AssertNoUnexpectedExceptionsRaised()
        {
            if (_exceptions.Any())
            {
                var unexpectedException = _exceptions.Dequeue();
                Assert.Fail($"No exception was expected to be raised but found exception: {unexpectedException}");
            }
        }
    }
}
