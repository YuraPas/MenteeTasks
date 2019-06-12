using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
