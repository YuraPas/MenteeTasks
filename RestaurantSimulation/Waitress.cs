using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Waitress
    {
        public Queue<Order> orders;
        public Kitchen kitchen;
        public IFood completedFood;
        
        public Waitress(Kitchen kitchen)
        {
            orders = new Queue<Order>();
            this.kitchen = kitchen;
        }

        public void ServeOrders()
        {
            var order = orders.Dequeue();
            order.NotifyReady(completedFood);
        }

        public void TakeOrders(Client client, Order order )
        {
            orders.Enqueue(order);
            Console.WriteLine($"Order registered \nClint[name = {client.Name}, happiness = {client.Happiness}]");
            Console.WriteLine($"Order[food = {order.Food}, extras = {string.Join(", ", order.Extras)}]");

            Console.WriteLine($"Processing {orders.Count} orders...");
            completedFood = kitchen.Cook(order);

        }

    }
}
