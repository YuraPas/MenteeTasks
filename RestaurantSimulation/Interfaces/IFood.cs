namespace RestaurantSimulation.Interfaces
{
    public interface IFood
    {
        string MainFoodName { get; set; }
        string ExtraFoodName { get; set; }
        double CalculateHappiness(double happiness);
    }
}
