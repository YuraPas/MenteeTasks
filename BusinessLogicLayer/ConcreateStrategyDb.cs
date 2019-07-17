using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer
{
    public class ConcreateStrategyDb : IStrategy
    {
        private IServiceBLL service;

        public ConcreateStrategyDb(IServiceBLL service)
        {
            this.service = service;
        }
        public List<Airport> AssignGatheredData(List<Airport> airports, List<City> cities, List<Country> countries)
        {
            int range = airports.Count();
            foreach (var item in airports)
            {
                item.City = cities.Where(c => c.CityId == item.CityId).First();
                item.Country = countries.Where(c => c.CountryId == item.CountryId).First();
            }

            var newAirports = service.AddLocationToAirport(airports);

            return newAirports;
        }
    }
}
