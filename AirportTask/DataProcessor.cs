using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AirportTask.SerializeModels;
using System.IO;

namespace AirportTask
{
    public class DataProcessor
    {
        static readonly string basePath = @"C:\Users\Рома\Downloads\";
        static Func<double, double, double, Location> GetLocation = DataInstantiator.GetLocation;

        public DataProcessor()
        {

        }

        public List<Airport> ProccessFile(string pathToFile, List<TimeZoneInformation> timeZoneAirports, ref int rowsIgnored)
        {
            List<Airport> result = new List<Airport>();

            foreach (var line in FileParser.GetAllLinesFromFile(pathToFile))
            {
                string[] items = TransformData(line);
                int currentId = items[0].ToInt();
                string airportName = items[1];

                var timeZone = timeZoneAirports.Where(c => c.AirportId == currentId)
                                                .Select(c => c.TimeZoneInfoId)
                                                .FirstOrDefault();
                if (!IsValid(items, timeZone))
                {
                    CustomLogger logger = new CustomLogger();
                    logger.LogInfo($"Invalid row: {currentId} - {airportName}");

                    rowsIgnored++;
                    continue;
                }
                
                
                string cityName = items[2];
                string countryName = items[3];
                string IATACode = items[4];
                string ICAOCode = items[5];
                double longitude = items[6].ToDouble();
                double latitude = items[7].ToDouble();
                double altitude = items[8].ToDouble();


                Country country = DataInstantiator.GetCountry(currentId, countryName);

                City city = DataInstantiator.GetCity(currentId, cityName, currentId, timeZone, country);

                Airport airport = DataInstantiator.GetAirport(currentId, airportName, IATACode,
                                                              ICAOCode, GetLocation, longitude,
                                                              latitude, altitude, timeZone,
                                                              city, country, currentId, currentId);


                result.Add(airport);
            }

            return result;
        }

        public List<Airport> ProccessDataFromSerializeFiles()
        {
            var airports = FileParser.ProccessJsonFile<Airport>(basePath + "airports.json");
            var cities = FileParser.ProccessJsonFile<City>(basePath + "cities.json");
            var countries = FileParser.ProccessJsonFile<Country>(basePath + "countries.json");

            int range = airports.Count();

            for (int i = 0; i < range; i++)
            {
                airports[i].City = cities[i];
                airports[i].City.Country = countries[i];
                airports[i].Country = countries[i];
            }

            return airports;
        }

        public bool SerializedFilesExist()
        {
            
            if (File.Exists(basePath + "countries.json") &&
                File.Exists(basePath + "cities.json") &&
                File.Exists(basePath + "airports.json"))
            {
                return true;
            }

            return false;
        }

        private static string[] TransformData(string line)
        {
            Regex pattern = new Regex("[ \" \\ ]");
            string newLine = pattern.Replace(line, "");

            return newLine.Split(",", StringSplitOptions.None);

        }

        private static bool IsValid(string[] items, string timeZone)
        {
            string IATACode = items[4];
            string ICAOCode = items[5];

            if (Regex.IsMatch(IATACode, @"(^$)") || IATACode == @"\N" ||
                Regex.IsMatch(ICAOCode, @"(^$)") || ICAOCode == @"\N" ||
                items.Length != 11 ||
                timeZone == null)
            {
                return false;
            }

            return true;
        }
    }
}
