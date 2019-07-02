using System;
using System.Drawing;
using System.IO;

namespace Epam.Exercises.CleanCode.AdvancedAscii.ConsoleApp
{
    public class ExtendedImage
    {
        private const byte LASTBYTE = 0xFF;
        private const int BYTE = 8;
        private readonly Bitmap image;

        private int rgbValue;

        public static ExtendedImage CreateImage(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new ExtendedImage(fileName);
            }

            throw new ArgumentException($"File not found: {fileName}", nameof(fileName));
        }

        private ExtendedImage(string fileName)
        {
            image = LoadImageFromFile(fileName);
        }

        public int GetWidth() => image.Width;

        public int GetHeight() => image.Height;

        public int GetIntensity(Point point)
        {
            return GetRed(point) + GetBlue(point) + GetGreen(point);
        }

        public int GetGreen(Point point)
        {
            rgbValue = GetRgbValue(point);
            return (rgbValue >> BYTE) & LASTBYTE;
        }

        public int GetBlue(Point point)
        {
            rgbValue = GetRgbValue(point);
            return rgbValue & LASTBYTE;
        }

        public int GetRed(Point point)
        {
            rgbValue = GetRgbValue(point);
            return (rgbValue >> (2 * BYTE)) & LASTBYTE;
        }

        private int GetRgbValue(Point point)
        {
            if (IsInsideBoundries(point, image))
            {
                return image.GetPixel(point.X, point.Y).ToArgb();
            }

            throw new ArgumentOutOfRangeException(nameof(point));
        }

        private bool IsInsideBoundries(Point point, Bitmap image)
        {
            if (point.X < 0 || point.X >= image.Width ||
                point.Y < 0 || point.Y >= image.Height)
            {
                return false;
            }

            return true;
        }

        private Bitmap LoadImageFromFile(string fileName)
        {
            using (var image = Image.FromFile(fileName))
            {
                return new Bitmap(image);
            }
        }
    }
}