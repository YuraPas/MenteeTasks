using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace RestaurantSimulation
{
    public class Kitchen
    {
        private readonly string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        public IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            foreach (var extra in extras)
            {
                return CreateInstance(extra, mainFood);
            }
            return null;
        }

        public IFood Cook(Order order)
        {
            IFood mainFood = CreateMainFood(order.Food);
            IFood mainFoodWithExtras = AddExtras(mainFood, order.Extras);

            Console.WriteLine($"Food prepeared: {mainFood.MainFoodName} with {mainFoodWithExtras.ExtraFoodName}");
            mainFoodWithExtras.MainFoodName = mainFood.MainFoodName;

            return mainFoodWithExtras;

        }

        public IFood CreateMainFood(string food)
        { 
            return CreateInstance(food);
        }

        public IFood CreateInstance(string className, params object[] paramArray)
        {
            Type foodType = Type.GetType(assemblyName + "." + className);

            return (IFood)Activator.CreateInstance(foodType, args: paramArray);
        }

    }
}
