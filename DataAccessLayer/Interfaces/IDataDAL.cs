using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using System.Collections.Generic;

namespace DataAccessLayer.Interfaces
{
    public interface IDataDAL
    {
        List<City> GetAllCities();

        List<Country> GetAllCountries();

        List<Airport> GetAllAirports();

        void SerializeToFile<T>(IEnumerable<T> elements, string path);

        string GetAirportsTimeZone(List<TimeZoneInformation> timeZoneAirports, int searchId);
    }
}
