using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Tuple<City, int> GetCityWithMostAirports(List<Airport> airports);
    }
}
