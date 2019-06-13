using RationalNumberHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumberHandler
{
    public static class RationalNumbersExtention
    {
        static int Gcd(int x, int y) => y == 0 ? x : Gcd(y, x % y); //greatest common divisor

        static int Lcm(int x, int y)  //least common multiple
        {
            return x * y / Gcd(x, y);
        }
        
        public static void SimplifyRationalNumber(this ref Rational  number)
        {
            int greatestCommonDivisor = Gcd(number.Numerator, number.Denominator);

            number.Numerator /= greatestCommonDivisor;
            number.Denominator /= greatestCommonDivisor;
        }

        public static int CalculateLeastCommonMultiple(this Rational obj1, Rational obj2)
        {
            int leastCommonMultiple = Lcm(obj1.Denominator, obj2.Denominator);

            return leastCommonMultiple;
        }

    }
}
