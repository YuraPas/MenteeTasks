using RestaurantSimulation.Interfaces;

namespace RestaurantSimulation
{
    public abstract class Extra : IFood
    {
        IFood Food { get; set; }
        public string MainFoodName { get; set; }
        public string ExtraFoodName { get; set; }

        public abstract double CalculateHappiness(double happiness);

        protected Extra(IFood food)
        {
            Food = food;
        }

    }
}
