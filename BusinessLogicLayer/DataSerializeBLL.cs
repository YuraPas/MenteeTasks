using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BusinessLogicLayer
{
    public class DataSerializeBLL : IDataSerializeBLL
    {
        private IDataDAL dataDAL;
        private IServiceBLL serviceBLL;

        public DataSerializeBLL(IDataDAL dataDAL, IServiceBLL serviceBLL)
        {
            this.dataDAL = dataDAL;
            this.serviceBLL = serviceBLL;
        }
        public void SerializeCounties(IEnumerable<Airport> airports)
        {
            var countries = airports.Select(airport => airport.Country);

            dataDAL.SerializeToFile(countries, "countries.json");
        }

        public void SerializeAirports(IEnumerable<Airport> airports)
        {
            var airportsToSerialize = airports.Select(airport => (AirportSerialize)airport);

            dataDAL.SerializeToFile(airportsToSerialize, "airports.json");
        }

        public void SerializeCities(IEnumerable<Airport> airports)
        {
            var cities = airports.Select(airport => (CitySerialize)airport.City);

            dataDAL.SerializeToFile(cities, "cities.json");
        }

        public List<Airport> ProccessDataFromSerializeFiles()
        {
            var airports = dataDAL.GetAllAirports();
            var cities = dataDAL.GetAllCities();
            var countries = dataDAL.GetAllCountries();

            airports = serviceBLL.AssignGatheredData(airports, cities, countries);

            return airports;
        }

     
    }
}
