using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            order.FoodReady += a_FoodCooked;
            cookRobot.TakeOrders(client, order);
            cookRobot.ServeOrders();

            Console.WriteLine(new string('-', 40));
            client.Eat(cookRobot.completedFood);


            Console.ReadKey();
        }
        static void a_FoodCooked(object sender, FoodReadyEventArgs e)
        {
            Console.WriteLine($"Notifying observers of Order: {e.Food.MainFoodName} with {e.Food.ExtraFoodName}");
            
        }
        
    }
}
