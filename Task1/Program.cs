using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Numbers must be in range from 0 to 3999");
            Console.WriteLine();

            int inputNumb = AskInputFromConsole();

            Console.WriteLine(inputNumb.ToRoman());
            Console.ReadLine();

        }

        public static int AskInputFromConsole()
        {
            Console.Write("Enter number you want to convert: ");
            int inputNumb = Convert.ToInt32(Console.ReadLine());
            while (inputNumb < 0 || inputNumb > 4000)
            {
                Console.Write("Enter number you want to convert: ");
                inputNumb = Convert.ToInt32(Console.ReadLine());
            }

            return inputNumb;
        }


    }
}