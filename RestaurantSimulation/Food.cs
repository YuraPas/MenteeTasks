using RestaurantSimulation.Interfaces;

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
