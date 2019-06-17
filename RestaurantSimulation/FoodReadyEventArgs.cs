using RestaurantSimulation.Interfaces;
using System;

namespace RestaurantSimulation
{
    public class FoodReadyEventArgs : EventArgs
    {
        public IFood Food { get; set; }

        public FoodReadyEventArgs(IFood food)
        {
            Food = food;
        }
    }
}
