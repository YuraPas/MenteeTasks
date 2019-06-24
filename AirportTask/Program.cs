using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AirportTask;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog;

namespace AirportTask
{
    class Program
    {

        static readonly string pathToDatFile = @"C:\Users\Рома\Downloads\airports.dat";
        static readonly string pathToJsonFile = @"C:\Users\Рома\Downloads\timezoneinfo.json";
        static string basePath = @"C:\Users\Рома\Downloads\";

        static Regex pattern = new Regex("[ \" \\ ]"); // \N check problems here
        static Logger logger = LogManager.GetLogger("Custom Logger");
        static Func<double, double, double, Location> MethodName = GetLocation;
        static JsonSerializer serializer = new JsonSerializer();


        static void Main(string[] args)
        {

            int rowsIgnored = 0;
            List<Airport> Airports = new List<Airport>();

            if (File.Exists(basePath + "countries.json") && File.Exists(basePath + "cities.json") &&
                File.Exists(basePath + "airports.json"))
            {
                Airports = ProccessJsonFile<Airport>(basePath + "airports.json");
                
                2int range = Airports.Count();

                var cities = ProccessJsonFile<City>(basePath + "cities.json");
                var countries = ProccessJsonFile<Country>(basePath + "countries.json");

                for (int i = 0; i < range; i++)
                {
                    Airports[i].City = cities[i];
                    Airports[i].City.Country = countries[i];
                    Airports[i].Country = countries[i];
                }
                

            }

            else
            {

                List<TimeZoneInfo> timeZoneAirports = ProccessJsonFile<TimeZoneInfo>(pathToJsonFile);
                Airports = ProccessFile(pathToDatFile, timeZoneAirports, ref rowsIgnored);

                logger.Info($"Rows ignored: {rowsIgnored}");

                Console.WriteLine($"{Airports.Count} entries avavailable");

                #region Serialization

                SerializeCities(Airports);
                SerializeCounties(Airports);
                SerializeAirports(Airports);

                Console.WriteLine("Serialization completed");
            }
            #endregion

            #region List all the countries by name in an ascending order, and display the number of airports they have

            var airportsInCountry = Airports.GroupBy(c => c.Country.Name).OrderBy(g => g.Key).Select(g => new
            {
                ContryName = g.Key,
                Count = g.Count()
            }).ToList();
            Console.WriteLine(new string('*', 50));

            foreach (var country in airportsInCountry)
            {
                Console.WriteLine($"{country.ContryName} has {country.Count} airports");
            }
            #endregion

            Console.WriteLine(new string('*', 50));

            #region Find the city which has got the most airports. If there are more than one cities with the same amount, display all of them.

            var cityWithMostAirpots = Airports.GroupBy(c => c.City.Name).Select(c => new
            {
                CityName = c.Key,
                Count = c.Count()
            }).OrderByDescending(c => c.Count);

            // OPTION 1
            var maxAirpotsCity = cityWithMostAirpots.Select(c => c).First();

            Console.WriteLine($"City {maxAirpotsCity.CityName} has {maxAirpotsCity.Count} airports");

            /*  OPTION 2
            var maxCities = cityWithMostAirpots.Where(c => c.Count == cityWithMostAirpots.Max(y => y.Count)).Select(c => c).ToList();
            foreach (var city in maxCities)
            {
                Console.WriteLine($"City {city.CityName} has {city.Count} airports");

            }
            */
            #endregion

            Console.WriteLine(new string('*', 50));

            # region Let the user enter an IATA code and display the following information about the given airport
            //TODO: обробляти стрінку, доробити запит одним словом  (validate that the string entered is correct)
            //local time


            Console.Write("Enter IATA code: ");
            string inputIATACode = Console.ReadLine();

            var searchedAirport = Airports.Where(c => c.IATACode == inputIATACode).First();

            Console.WriteLine($"Name - {searchedAirport.FullName}, Country - {searchedAirport.Country.Name}, " +
                              $"City - {searchedAirport.City.Name} ");

            #endregion

            Console.WriteLine(new string('*', 50));

            #region Let the user enter a GPS coordinate and find out which airport is the closest to that coordinate and display its Name and City.

            //ORDER: Latitude, Longitude, Altitude
            var inputLocation = new GeoCoordinate(69.241186, 41.217860, 1400);
            var nearest = Airports.Where(x => x.Location.Latitude <= 90 && x.Location.Latitude >= -90 &&
                                         x.Location.Longitude <= 90 && x.Location.Longitude >= -90).Select(x => new
                                         {
                                             coordinate = new GeoCoordinate(x.Location.Latitude, x.Location.Longitude, x.Location.Altitude),
                                             name = x.Name,
                                             city = x.City
                                         }).OrderBy(x => x.coordinate.GetDistanceTo(inputLocation))
                       .First();
            Console.WriteLine($"{nearest.city.Name} {nearest.name} ");
            #endregion


            Console.ReadKey();

        }

        private static void SerializeCounties(List<Airport> airports)
        {
            var countries = airports.Select(c => c.Country).ToList();

            SerializeToFile(basePath + "countries.json", countries);

        }

        private static void SerializeAirports(List<Airport> airports)
        {
            var airportsList = airports.Select(c => (AirportSerialize)c).ToList();

            SerializeToFile(basePath + "airports.json", airportsList);
        }

        private static void SerializeCities(List<Airport> airports)
        {

            var cities = airports.Select(c => (CitySerialize)c.City).ToList();

            SerializeToFile(basePath + "cities.json", cities);

        }

        private static void SerializeToFile<T>(string path, List<T> elements)
        {
            using (StreamWriter file = File.AppendText(path))
            {

                serializer.Serialize(file, elements);
            }
        }

        private static List<T> ProccessJsonFile<T>(string pathToJsonFile)
        {
            using (var jsonReader = new StreamReader(pathToJsonFile))
            {
                string jsonLines = jsonReader.ReadToEnd();
                var data = JsonConvert.DeserializeObject<List<T>>(jsonLines);

                return data;

            }
        }

        private static List<Airport> ProccessFile(string path, List<TimeZoneInfo> timeZoneAirports, ref int rowsIgnored)
        {
            List<Airport> result = new List<Airport>();
            string line;
            int currentId;

            using (var fileReader = new StreamReader(path))
            {
                while ((line = fileReader.ReadLine()) != null)
                {

                    string[] items = TransformData(line);

                    currentId = items[0].ToInt();

                    var timeZone = timeZoneAirports.Where(c => c.AirportId == currentId).Select(c => c.TimeZoneInfoId).FirstOrDefault();

                    if (IsCorrect(items, timeZone) != true)
                    {
                        logger.Info($"{items[0]} - {items[1]} - {items[2]} - {items[3]} - {items[4]} - {items[5]} - {items[6]}");
                        rowsIgnored++;

                        continue;
                    }


                    Country country = GetCountry(currentId, items[3]);

                    City city = GetCity(currentId, items[2], items[0].ToInt(), timeZone, country);

                    Airport airport = GetAirport(currentId, items[1], items[4], items[5], MethodName, items[6].ToDouble(),
                                                 items[7].ToDouble(), items[8].ToDouble(), timeZone, city, country, currentId, currentId);


                    result.Add(airport);
                }

            }

            return result;
        }

        private static string[] TransformData(string line)
        {
            string newLine = pattern.Replace(line, "");

            return newLine.Split(",", StringSplitOptions.None);

        }

        private static bool IsCorrect(string[] items, string timeZone)
        {
            if (Regex.IsMatch(items[4], @"(^$)") ||
                Regex.IsMatch(items[5], @"(^$)") ||
                items.Length != 11 ||
                timeZone == null ||
                items[4] == @"\N" ||
                items[5] == @"\N")
            {
                return false;
            }

            return true;
        }




        #region Classes instantiation

        public static Location GetLocation(double longitude, double latitude, double altitude)
        {
            Location instance = new Location()
            {
                Longitude = longitude,
                Latitude = latitude,
                Altitude = altitude
            };

            return instance;
        }

        public static Country GetCountry(int id, string name)
        {
            Country instance = new Country()
            {
                Id = id,
                Name = name
            };

            return instance;
        }

        public static City GetCity(int id, string name, int countryId, string timeZone, Country country)
        {
            City instance = new City()
            {
                Id = id,
                Name = name,
                CountryId = countryId,
                TimeZoneName = timeZone,
                Country = country
            };

            return instance;
        }

        public static Airport GetAirport(int id, string name, string IATACode, string ICAOCode,
                                        Func<double, double, double, Location> MethodName, double longitude, double latitude, double altitude, string timeZone,
                                        City city, Country country, int cityId, int countryId)
        {
            Airport instance = new Airport()
            {


                Id = id,
                Name = name,
                IATACode = IATACode,
                ICAOCode = ICAOCode,
                Location = MethodName(longitude, latitude, altitude),
                TimeZoneName = timeZone,
                City = city,
                Country = country,
                CityId = cityId,
                CountryId = countryId
            };

            return instance;
        }

    }
    #endregion

    //Map values from Json file
    public class TimeZoneInfo
    {
        public int AirportId { get; set; }
        public string TimeZoneInfoId { get; set; }
    }

    public class CitySerialize
    {
        public int CountryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get; set; }
    }

    public class AirportSerialize
    {
        public int Id { get; set; }
        public string IATACode { get; set; }
        public string ICAOCode { get; set; }
        public string Name { get; set; }
        public string FullName
        {
            get
            {
                return Name + " Airport";
            }
        }
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string TimeZoneName { get; set; }
        public Location Location { get; set; }

    }
}



