using NUnit.Framework;
using RationalNumberHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenteeTasks.Tests
{

    [TestFixture]
    public class RationalNumberHandlerTests
    {

        [OneTimeSetUp]
        public void Init()
        {

        }

        [Test]
        public void CreateInstanceWithZeroDenominator_GetException()
        {
            //arrange
            int numerator = 5;
            int denominator = 0;

            //act


            //assert
            Assert.Throws<ArgumentException>(() => new Rational(numerator, denominator));
        }

        [Test]
        public void PassWrongValue_CompareErrorMessages()
        {
            //arrange
            int numerator = 5;
            int denominator = 0;
            string errorMessage = "Denominator can't be zero";

            //act
            var ex = Assert.Throws<ArgumentException>(() => new Rational(numerator, denominator));

            //assert
            Assert.AreEqual(errorMessage, ex.Message);
        }

        [Test]
        public void SimplifyRationalNumber_SimplifyNumber_GetSimplified()
        {
            //arrange
            Rational expected = new Rational(3, 6);
            Rational actual = new Rational(1, 2);

            //act
            expected.SimplifyRationalNumber();

            //assert
            Assert.AreEqual(expected, actual);
            Assert.IsTrue(actual.Equals(expected));
        }
    }
}
