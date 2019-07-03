using AirportTask.SerializeModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AirportTask
{
    public class DataSerializer
    {
        private JsonSerializer serializer;
        private readonly string basePath = @"C:\Users\Рома\Downloads\";

        public DataSerializer()
        {
            serializer = new JsonSerializer();
        }

        public void SerializeCounties(IEnumerable<Airport> airports)
        {
            var countries = airports.Select(airport => airport.Country);

            SerializeToFile(countries, basePath + "countries.json");
        }

        public void SerializeAirports(IEnumerable<Airport> airports)
        {
            var airportsToSerialize = airports.Select(airport => (AirportSerialize)airport);

            SerializeToFile(airportsToSerialize, basePath + "airports.json");
        }

        public void SerializeCities(IEnumerable<Airport> airports)
        {
            var cities = airports.Select(airport => (CitySerialize)airport.City);

            SerializeToFile(cities, basePath + "cities.json");
        }

        private void SerializeToFile<T>(IEnumerable<T> elements, string path)
        {
            using (StreamWriter file = File.AppendText(path))
            {
                serializer.Serialize(file, elements);
            }
        }
    }
}
