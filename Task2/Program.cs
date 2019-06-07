using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RationalNumberHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            // How to invoke method SimplifyRationalNumber automatically
            // after the  object was instantiated ?

            Rational firstObj = Rational.CreateInstance(9, 3);
            Rational secondObj = Rational.CreateInstance(9, 4);
            Rational thirdObj = Rational.CreateInstance(18, 8);

            Console.WriteLine(firstObj); //outputs 3
            Console.WriteLine(secondObj); //outputs 9r4

            Console.WriteLine(firstObj.Equals(secondObj)); //false
            Console.WriteLine(secondObj.Equals(thirdObj)); //true => (18/2, 8/2) == (9, 4)

            Console.WriteLine(firstObj.CompareTo(secondObj)); // -1
            Console.WriteLine(secondObj.CompareTo(thirdObj)); //  0

            Console.WriteLine(firstObj + secondObj); // 21r4 (before simplification 63/12)
            Console.WriteLine(firstObj * secondObj); // 27r4

            Console.ReadLine();
        }

        
    }
}
