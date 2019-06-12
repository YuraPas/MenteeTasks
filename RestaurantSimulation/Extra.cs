using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public abstract class Extra : IFood
    {
        IFood Food { get; set; }
        public string MainFoodName { get; set; }
        public string ExtraFoodName { get; set; }

        protected Extra(IFood food)
        {
            Food = food;
        }

        public abstract double CalculateHappiness(double happiness);
    }
}
