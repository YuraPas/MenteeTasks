using System;
using System.Drawing;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class Program
    {
        private static readonly char[] CharsByDarkness = { '#', '@', 'X', 'L', 'I', ':', '.', ' ' };
        private static ExtendedImage image = ExtendedImage.CreateImage("pair_hiking.png");
        private static int max = 0;
        private static int min = 255 * 3;

        public static void Main(string[] args)
        {
            int imageHeight = image.GetHeight();
            int imageWidth = image.GetWidth();

            int stepY = imageHeight / 45;
            int stepX = imageWidth / 150;

            CalculateBorderValues(stepX, stepY);

            OutputChars(imageHeight, imageWidth, stepY, stepX, image, CharsByDarkness, min, max);

            Console.ReadLine();
        }

        private static void CalculateBorderValues(int stepX, int stepY)
        {
            for (int y = 0; y < image.GetHeight(); y += stepY)
            {
                for (int x = 0; x < image.GetWidth(); x += stepX)
                {
                    int sum = GetSum(x, y, stepX, stepY, image);
                    EvaluateBorders(ref max, ref min, sum);
                }
            }
        }

        private static void OutputChars(int imageHeight, int imageWidth, int stepY, int stepX, ExtendedImage image, char[] charsByDarkness, int min, int max)
        {
            for (int y = 0; y < imageHeight - stepY; y += stepY)
            {
                for (int x = 0; x < imageWidth - stepX; x += stepX)
                {
                    int sum = GetSum(x, y, stepX, stepY, image);
                    Console.Write(charsByDarkness[(sum - min) * charsByDarkness.Length / (max - min + 1)]);
                }

                Console.WriteLine();
            }
        }

        private static void EvaluateBorders(ref int max, ref int min, int sum)
        {
            if (max < sum)
            {
                max = sum;
            }

            if (min > sum)
            {
                min = sum;
            }
        }

        private static int GetSum(int x, int y, int stepX, int stepY, ExtendedImage image)
        {
            int sum = 0;

            int timesToMultiply = stepX * stepY;

            sum += timesToMultiply * GetPointSum(image, x, y);

            sum /= stepY / stepX;

            return sum;
        }

        private static int GetPointSum(ExtendedImage image, int x, int y)
        {
            int total = image.GetRed(new Point(x, y)) +
                        image.GetBlue(new Point(x, y)) +
                        image.GetGreen(new Point(x, y));

            return total;
        }
    }
}
