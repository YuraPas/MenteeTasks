using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer
{
    public class DataSerializeBLL : IDataSerializeBLL
    {
        private IFileService _fileService;

        public DataSerializeBLL(IFileService fileService)
        {
            _fileService = fileService;
        }

        public void SerializeCounties(IEnumerable<Airport> airports)
        {
            var countries = airports.Select(airport => airport.Country);

            _fileService.SerializeToFile(countries, "countries.json");
        }

        public void SerializeAirports(IEnumerable<Airport> airports)
        {
            var airportsToSerialize = airports.Select(airport => (AirportSerialize)airport);

            _fileService.SerializeToFile(airportsToSerialize, "airports.json");
        }

        public void SerializeCities(IEnumerable<Airport> airports)
        {
            var cities = airports.Select(airport => (CitySerialize)airport.City);

            _fileService.SerializeToFile(cities, "cities.json");
        }
    }
}
