using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Moq;
using NUnit.Framework;
using System;

namespace AirportTask.Tests
{
    [TestFixture]
    public class ServiceTests
    {
        ServiceBLL serviceBLL;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            serviceBLL = new ServiceBLL();
        }

        [Test]
        public void RemoveQuotes_PassLine_GetLineWithoutQuotes()
        {
            // arrange
            string stringToTransform = "quotes\" \"removed";
            string expected = "quotes removed";

            // act
            string actual = serviceBLL.RemoveQuotes(stringToTransform);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SplitLine_PassLine_GetSplitedItems()
        {
            // arrange
            string stringToTransform = "this,should,be,splitted";
            var serviceBLL = new ServiceBLL();
            string[] expected = new string[] { "this", "should", "be", "splitted" };

            // act
            string[] actual = serviceBLL.SplitLine(stringToTransform);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IsValid_PassWrongIATACode_ReturnFalse()
        {
            // arrange
            string IATACode = @"\N";
            string[] items = new string[] { "test", "test", "test", "test", IATACode, "test", };

            // act
            bool actual = serviceBLL.IsValid(items, "test");

            // assert
            Assert.IsFalse(actual);

        }

    }
}
