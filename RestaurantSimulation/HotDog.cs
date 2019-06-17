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
