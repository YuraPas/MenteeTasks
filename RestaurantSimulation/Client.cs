using RestaurantSimulation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantSimulation
{
    public class Client // : IObservable<Order>
    {
        public double Happiness { get; set; }
        public string Name { get; }

        public Client(string name, double happiness)
        {
            Name = name;
            Happiness = happiness;
        }

        public void Eat(IFood food)
        {
            Console.WriteLine($"Client {Name} with {Happiness} happiness starts eating " +
                $"{food.MainFoodName} with {food.ExtraFoodName} ");
            Thread.Sleep(1500);
            Console.WriteLine("Csam csam nyam nyam");
            Happiness = food.CalculateHappiness(Happiness);
            Console.WriteLine($"Food eaten, Client [name={Name}, happiness={Happiness}]");
        }

        //public IDisposable Subscribe(IObserver<Order> observer)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
