using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public abstract class Food : IFood
    {
        public string MainFoodName { get; set; }
        public string ExtraFoodName { get; set; }

        protected Food()
        {

        }


        public abstract double CalculateHappiness(double happiness);

    }
}
