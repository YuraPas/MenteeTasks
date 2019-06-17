using System;
using System.Collections.Generic;

namespace RestaurantSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client("Bob", 100);

            Order order = new Order("Chips", new List<string> { "Ketchup" });
            Kitchen kitchen = new Kitchen();
            Waitress cookRobot = new Waitress(kitchen);


            cookRobot.TakeOrders(client, order);
            cookRobot.ServeOrders();

            Console.WriteLine(new string('-', 40));
            client.Eat(cookRobot.completedFood);


            Console.ReadKey();
        }

        
    }
}
