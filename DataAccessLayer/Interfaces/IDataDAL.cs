using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IDataDAL
    {
        List<City> GetAllCities();
        List<Country> GetAllCountries();
        List<Airport> GetAllAirports();
    }
}
