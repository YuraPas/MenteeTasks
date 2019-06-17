using System;
using System.Collections.Generic;
using System.Linq;

namespace Task1
{
    public static class Extension
    {
        public static RomanNumeral RetrieveElementByIndex(this RomanNumeral romanNumeral, int index)
        { 
            List<RomanNumeral> values = Enum.GetValues(typeof(RomanNumeral)).Cast<RomanNumeral>().ToList();
            values.Reverse();
            RomanNumeral desiredItem = values[index];
            
            return desiredItem;

        }

        #region Convert Algorithm
        /*
           LET number be an integer between 1 and 4000
           LET symbol be RomanNumeral.values()[0]
           LET result be an empty String
           WHILE number > 0: 
           *iterations to check every symbol one-by-one*
             IF symbol's value <= number:
             append the result with the symbol's name
             subtract symbol's value from number
           ELSE:
             pick the next symbol
         */
        #endregion

        public static string ToRoman(this int arabicNumb)
        {
            if (arabicNumb < 0 || arabicNumb > 4000)
            {
                throw new ArgumentException("Argument should be greater that 0 but less that 4000!");
            }

            string output = String.Empty;
            RomanNumeral romanNumeral = new RomanNumeral();
            int iterations = 0;
            int enumSize = Enum.GetValues(typeof(RomanNumeral)).Length;

            while ((arabicNumb > 0) && (iterations < enumSize))
            {
                RomanNumeral currentSymbol = romanNumeral.RetrieveElementByIndex(iterations);

                if ((int)currentSymbol <= arabicNumb)
                {
                    output += currentSymbol;
                    arabicNumb -= Convert.ToInt32(currentSymbol);
                }
                else
                {
                    iterations++;
                }
            }

            return output;
        }
    }
}
