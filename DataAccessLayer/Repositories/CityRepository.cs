using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(AirportContext context) : base(context)
        {
        }

        public Tuple<City,int> GetCityWithMostAirports(List<Airport> airports)
        {
            var cityWithMostAirports =
                            airports.GroupBy(airport => airport.City.Name)
                                            .Select(city => new
                                            {
                                                CityName = city.Key,
                                                AirportsCount = city.Count(),
                                            })
                                            .OrderByDescending(city => city.AirportsCount)
                                            .First();

             var cityObject = airports.Where(airport => airport.City.Name == cityWithMostAirports.CityName)
                           .Select(airport => airport.City)
                           .First();

            return Tuple.Create(cityObject, cityWithMostAirports.AirportsCount);
        }
    }
}
