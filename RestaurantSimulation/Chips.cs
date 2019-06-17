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
