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

        public DataSerializeBLL(IDataDAL dataDAL)
        {
            this.dataDAL = dataDAL;
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

            airports = AssignGatheredData(airports, cities, countries);

            return airports;
        }

        public bool SerializedFilesExist()
        {
            if (FileExists("countries.json") &&
                FileExists("cities.json") &&
                FileExists("airports.json"))
            {
                return true;
            }

            return false;
        }

        private bool FileExists(string fileName)
        {
            string basePath = @"C:\Users\Рома\Downloads\";

            if (File.Exists(basePath + fileName))
            {
                return true;
            }

            return false;
        }

        private List<Airport> AssignGatheredData(List<Airport> airports, List<City> cities, List<Country> countries)
        {
            int range = airports.Count();

            for (int i = 0; i < range; i++)
            {
                airports[i].City = cities[i];
                airports[i].City.Country = countries[i];
                airports[i].Country = countries[i];
            }

            return airports;
        }
    }
}
