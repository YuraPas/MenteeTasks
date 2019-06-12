using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Mustard : Extra
    {
        public Mustard(IFood mainFood) : base(mainFood)
        {
            ExtraFoodName = "Mustard";
        }

        public override double CalculateHappiness(double happiness)
        {
            return happiness + 1;
        }
    }
}
