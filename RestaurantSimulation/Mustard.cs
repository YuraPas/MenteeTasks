using RestaurantSimulation.Interfaces;

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
