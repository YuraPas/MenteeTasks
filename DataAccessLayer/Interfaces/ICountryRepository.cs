using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        IEnumerable<Tuple<City, int>> GetAllCountriesWithNumberOfAirportsThenHave(List<Airport> airports);
    }
}
