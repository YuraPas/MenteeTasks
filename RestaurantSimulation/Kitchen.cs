using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Kitchen
    {
        public IFood AddExtras(IFood mainFood, IEnumerable<string> extras)
        {
            foreach(var extra in extras)
            {
                //figure out how to work with Ienumarable here
                //Abstarct factory pattern can be used here
                if(extra == "Ketchup")
                {
                    return new Ketchup(mainFood);
                }
                else if (extra == "Mustard")
                {
                    return new Mustard(mainFood);
                }
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
            if (food == "HotDog")
            {
                return new HotDog();
            }
            else if (food == "Chips")
            {
                return new Chips();
            }

            return null;
        }
    }
}
