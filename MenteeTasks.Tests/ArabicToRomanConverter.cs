using NUnit.Framework;
using System;
using Task1;

namespace MenteeTasks.Tests
{

    [TestFixture]
    public class ArabicToRomanConverter
    {
        
        [OneTimeSetUp]
        public void Init()
        {

        }

        [Test]
        public void Convert1974ToRoman()
        {
            //arrange
            int actual = 1974;

            //act
            string result = Extension.ToRoman(actual);

            Assert.AreEqual("MCMLXXIV", result);
        }

        [Test]
        public void PassValueLessThatZero_GetException()
        {
            //arrange
            int actual = -5;

            //act
            
            //assert
            Assert.Throws<ArgumentException>(() => Extension.ToRoman(actual));
        }
        [Test]
        public void PassValueGreaterThat4000_GetException()
        {
            //arrange
            int actual = 4500;

            //act

            //assert
            Assert.Throws<ArgumentException>(() => Extension.ToRoman(actual));
        }

        [Test]
        public void PassWrongValue_CompareErrorMessages()
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
        public void CompareRomanNumerals()
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
        public void CompareRomanNumeralValues()
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
