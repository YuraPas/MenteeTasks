using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation.Interfaces
{
    public interface IFood
    {
        string MainFoodName { get; set; }
        string ExtraFoodName { get; set; }
        double CalculateHappiness(double happiness);
    }
}
