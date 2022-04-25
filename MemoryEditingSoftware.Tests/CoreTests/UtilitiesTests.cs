using Microsoft.VisualStudio.TestTools.UnitTesting;
using MemoryEditingSoftware.Core.Business;

namespace MemoryEditingSoftware.Tests.CoreTests
{
    [TestClass]
    public class CoreTests
    {
        [TestMethod]
        public void StringOnlyContainsDigits()
        {
            // ARRANGE


            // ACT
            bool isDigits = Utilities.IsDigitsOnly("456");

            // ASSERT
            Assert.AreEqual(true, isDigits);
        }

        [TestMethod]
        public void StringDoesNotOnlyContainsDigits()
        {
            // ARRANGE


            // ACT
            bool isDigits = Utilities.IsDigitsOnly("4-56");

            // ASSERT
            Assert.AreEqual(false, isDigits);
        }
    }
}
