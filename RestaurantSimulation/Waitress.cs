using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;

namespace RestaurantSimulation
{
    public class Waitress
    {
        public Queue<Order> orders;
        public Kitchen kitchen;
        public IFood completedFood;
        
        public Waitress(Kitchen _kitchen)
        {
            orders = new Queue<Order>();
            kitchen = _kitchen;
        }

        public void ServeOrders()
        {
            var order = orders.Dequeue();
            order.NotifyReady(completedFood);

            order.FoodReady -= a_FoodCooked;
        }

        public void TakeOrders(Client client, Order order )
        {
            orders.Enqueue(order);
            Console.WriteLine($"Order registered \nClint[name = {client.Name}, happiness = {client.Happiness}]");
            Console.WriteLine($"Order[food = {order.Food}, extras = {string.Join(", ", order.Extras)}]");

            Console.WriteLine($"Processing {orders.Count} orders...");
            completedFood = kitchen.Cook(order);

            order.FoodReady += a_FoodCooked;

        }

        static void a_FoodCooked(object sender, FoodReadyEventArgs e)
        {
            Console.WriteLine($"Notifying observers of Order: {e.Food.MainFoodName} with {e.Food.ExtraFoodName}");

        }

    }
}
