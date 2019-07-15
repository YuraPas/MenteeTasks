using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System.Collections.Generic;

namespace DataAccessLayer
{
    public class JsonFileDataDAL : IDataDAL
    {
        private IFileService fileService;
        
        public JsonFileDataDAL(IFileService fileParser)
        {
            fileService = fileParser;
        }

        public List<City> GetAllCities()
        {
            var cities = fileService.ProccessJsonFile<City>("cities.json");

            return cities;
        }

        public List<Country> GetAllCountries()
        {
            var countries = fileService.ProccessJsonFile<Country>("countries.json");

            return countries;
        }

        public List<Airport> GetAllAirports()
        {
            var airports = fileService.ProccessJsonFile<Airport>("airports.json");

            return airports;
        }
    }
}
