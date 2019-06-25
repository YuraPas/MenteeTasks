using System;
using System.Drawing;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ExtendedImage image = ExtendedImage.CreateImage("pair_hiking.png");
            char[] charsByDarkness = { '#', '@', 'X', 'L', 'I', ':', '.', ' ' };
            int max = 0;
            int min = 255 * 3;
            int stepY = image.GetHeight() / 45;
            int stepX = image.GetWidth() / 150;
            for (int y = 0; y < image.GetHeight(); y += stepY) {
                for (int x = 0; x < image.GetWidth(); x += stepX) {
                    int sum = 0;
                    for (int avgy = 0; avgy < stepY; avgy++) {
                        for (int avgx = 0; avgx < stepX; avgx++) {
                            sum = sum + (image.GetRed(new Point(x, y)) + image.GetBlue(new Point(x, y)) + image.GetGreen(new Point(x, y)));
                        }
                    }
                    sum = sum / stepY / stepX;
                    if (max < sum) {
                        max = sum;
                    }
                    if (min > sum) {
                        min = sum;
                    }
                }
            }
            for (int y = 0; y < image.GetHeight() - stepY; y += stepY) {
                for (int x = 0; x < image.GetWidth() - stepX; x += stepX) {
                    int sum = 0;
                    for (int avgy = 0; avgy < stepY; avgy++) {
                        for (int avgx = 0; avgx < stepX; avgx++) {
                            sum = sum + (image.GetRed(new Point(x, y)) + image.GetBlue(new Point(x, y)) + image.GetGreen(new Point(x, y)));
                        }
                    }
                    sum = sum / stepY / stepX;
                    Console.Write(charsByDarkness[(sum - min) * charsByDarkness.Length / (max - min + 1)]);
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
