using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(AirportContext context) : base(context)
        {
        }

        public IEnumerable<Tuple<City, int>> GetAllCountriesWithNumberOfAirportsThenHave(List<Airport> airports)
        {
            var airportsInCountry = airports.GroupBy(airport => airport.Country.Name)
                                                    .OrderBy(group => group.Key)
                                                    .Select(group => new
                                                    {
                                                        CountryName = group.Key,
                                                        Count = group.Count(),
                                                    });

            IEnumerable<Tuple<City, int>> dataTuple = airportsInCountry.Select(item => new Tuple<City,int>(airports.Where(a => a.City.Name == item.CountryName).Select(a => a.City).First(), item.Count)).AsEnumerable();

            
                                                    //Tuple.Create(airports.Where(airport => airport.City.Name == group.Key)
                                                    //                     .Select(airport => airport.City)
                                                    //                     .First(), group.Count()));

            return dataTuple;
        }
    }
}
