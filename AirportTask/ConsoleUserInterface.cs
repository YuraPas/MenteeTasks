using System;
using PresentationLayer.Interfaces;

namespace PresentationLayer
{
    public class ConsoleUserInterface : IUserInterface
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }

        public void ReadKey()
        {
            Console.ReadKey();
        }
    }
}
