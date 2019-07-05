using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class DataDAL : IDataDAL
    {
        private readonly string basePath = @"C:\Users\Рома\Downloads\";
        private JsonSerializer serializer;

        public DataDAL()
        {
            serializer = new JsonSerializer();
        }

        public List<City> GetAllCities()
        {
            var cities = FileParser.ProccessJsonFile<City>(basePath + "cities.json");

            return cities;
        }

        public List<Country> GetAllCountries()
        {
            var countries = FileParser.ProccessJsonFile<Country>(basePath + "countries.json");

            return countries;
        }

        public List<Airport> GetAllAirports()
        {
            var airports = FileParser.ProccessJsonFile<Airport>(basePath + "airports.json");

            return airports;
        }

        public void SerializeToFile<T>(IEnumerable<T> elements, string jsonFileName)
        {
            using (StreamWriter file = File.AppendText(basePath + jsonFileName))
            {
                serializer.Serialize(file, elements);
            }
        }

        public string GetAirportsTimeZone(List<TimeZoneInformation> timeZoneAirports, int searchId)
        {
            var timeZone = timeZoneAirports.Where(c => c.AirportId == searchId)
                                           .Select(c => c.TimeZoneInfoId)
                                           .FirstOrDefault();

            return timeZone;
        }
    }
}
