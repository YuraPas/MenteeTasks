using System;

namespace AirportTask
{
    public static class StringExtensions
    {
        public static int ToInt(this string str)
        {
            int result;
            try
            {
                result = Int32.Parse(str);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return result;

        }

        public static double ToDouble(this string str)
        {
            double result;
            try
            {
                result = double.Parse(str);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return result;
        }
    }
}
