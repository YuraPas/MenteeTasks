using NUnit.Framework;
using System;
using Task1;

namespace MenteeTasks.Tests
{

    [TestFixture]
    public class ArabicToRomanConverterTests
    {
        
        [OneTimeSetUp]
        public void Init()
        {

        }

        [Test]
        public void ToRoman_ValidArabicNumeral_ReturnsValidRoman()
        {
            //arrange
            int actual = 1974;

            //act
            string result = Extension.ToRoman(actual);

            //assert
            Assert.AreEqual("MCMLXXIV", result);
        }

        [Test]
        public void ToRoman_PassValueLessThatZero_GetException()
        {
            //arrange
            int actual = -5;

            //act
            
            //assert
            Assert.Throws<ArgumentException>(() => Extension.ToRoman(actual));
        }
        [Test]
        public void ToRoman_PassValueGreaterThat4000_GetException()
        {
            //arrange
            int actual = 4500;

            //act

            //assert
            Assert.Throws<ArgumentException>(() => Extension.ToRoman(actual));
        }

        [Test]
        public void ToRoman_WrongValue_EqualErrorMessages()
        {
            //arrange
            int actual = -100;
            string errorMessage = "Argument should be greater that 0 but less that 4000!";

            //act
            var ex = Assert.Throws<ArgumentException>(() => Extension.ToRoman(actual));

            //assert
            Assert.AreEqual(errorMessage, ex.Message);
        }

        [Test]
        public void RetrieveElementByIndex_RetriveFirstNumeral_NumeralsMatch()
        {
            //arrange
            RomanNumeral romanNumeral = RomanNumeral.M;
            int index = 0;
            
            //act 
            RomanNumeral actualNumeral = romanNumeral.RetrieveElementByIndex(index);

            //assert
            Assert.AreEqual(romanNumeral, actualNumeral);
        }

        [Test]
        public void RetrieveElementByIndex_GetValueOfNumeral_ValuesMatch()
        {
            //arrange
            RomanNumeral romanNumeral = RomanNumeral.M;
            int index = 0;
            int romanNumeralValue = 1000;

            //act 
            RomanNumeral actualNumeral = romanNumeral.RetrieveElementByIndex(index);
            int actualRomanValue = (int)actualNumeral;
            //assert
            Assert.AreEqual(romanNumeralValue, actualRomanValue);
        }

    }
}
