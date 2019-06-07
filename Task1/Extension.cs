using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            String output = String.Empty;
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
