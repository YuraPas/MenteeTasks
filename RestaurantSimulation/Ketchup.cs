﻿using RestaurantSimulation.Interfaces;

namespace RestaurantSimulation
{
    public class Ketchup : Extra
    {
        IFood mainFood;

        public Ketchup(IFood mainFood) : base(mainFood)
        {
            ExtraFoodName = "Ketchup";
            this.mainFood = mainFood;
        }
        public override double CalculateHappiness(double happiness)
        {
            double mainFoodHappiness = mainFood.CalculateHappiness(happiness);

            return mainFood.CalculateHappiness(mainFoodHappiness);
        }
    }
}
