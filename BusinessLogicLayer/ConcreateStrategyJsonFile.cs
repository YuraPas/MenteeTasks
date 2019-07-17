using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class ConcreateStrategyJsonFile : IStrategy
    {
        public List<Airport> AssignGatheredData(List<Airport> airports, List<City> cities, List<Country> countries)
        {
            int range = airports.Count();

            for (int i = 0; i < range; i++)
            {
                airports[i].City = cities[i];
                airports[i].Country = countries[i];
            }

            return airports;
        }
    }
}
