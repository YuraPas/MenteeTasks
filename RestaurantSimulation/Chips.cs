using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Chips : Food
    {
        public Chips()
        {
            MainFoodName = "Chips";
        }
        public override double CalculateHappiness(double happiness)
        {
            return happiness + happiness * 0.05;
        }
    }
}
