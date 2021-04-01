using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices.IntegrationTests.Common
{
    public class ExceptionAssert
    {
        public static void Throws<T>(Action task) where T : Exception
        {
            try
            {
                task();
            }
            catch (Exception ex)
            {
                AssertExceptionType<T>(ex);
                return;
            }

            Assert.Fail("Expected exception but no exception was thrown.");
        }

        private static void AssertExceptionType<T>(Exception ex) where T : Exception
        {
            if (typeof(T).Equals(ex.GetType()))
            {
                Assert.Fail($"Expected exception of type {typeof(T)}, but exception is of type {ex.GetType()}.");
            }
        }
    }
}