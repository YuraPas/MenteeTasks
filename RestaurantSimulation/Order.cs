using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Order
    {
        public IEnumerable<string> Extras { get; }
        public string Food { get; }

        public event EventHandler<FoodReadyEventArgs> FoodReady;


        public Order(string food, IEnumerable<string> extras)
        {
            Food = food;
            Extras = extras;
        }


        public void NotifyReady(IFood food)
        {
            FoodReady?.Invoke(this, new FoodReadyEventArgs(food));
        }


    }
}
