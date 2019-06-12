using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class HotDog : Food
    {
        public HotDog()
        {
            MainFoodName = "HotDog";
        }
        
        public override double CalculateHappiness(double happiness)
        {
            return happiness + 2;
        }
    }
}
