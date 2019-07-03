using System;
using System.Collections.Generic;
using System.Linq;
using AirportTask.SerializeModels;
using GeoCoordinatePortable;

namespace AirportTask
{
    class Program
    {
        static readonly string pathToDatFile = @"C:\Users\Рома\Downloads\airports.dat";
        static readonly string pathToJsonFile = @"C:\Users\Рома\Downloads\timezoneinfo.json";


        static void Main(string[] args)
        {
            List<Airport> Airports = new List<Airport>();
            DataProcessor dataProcessor = new DataProcessor();

            if (dataProcessor.SerializedFilesExist())
            {
                Airports = dataProcessor.ProccessDataFromSerializeFiles();
            }
            else
            {
                int rowsIgnored = 0;
                CustomLogger logger = new CustomLogger();

                var timeZoneAirports = FileParser.ProccessJsonFile<TimeZoneInformation>(pathToJsonFile);
                Airports = dataProcessor.ProccessFile(pathToDatFile, timeZoneAirports, ref rowsIgnored);

                logger.LogInfo($"Rows ignored: {rowsIgnored}");

                Console.WriteLine($"{Airports.Count} entries avavailable");

                #region Serialization

                DataSerializer serializer = new DataSerializer();

                serializer.SerializeCities(Airports);
                serializer.SerializeCounties(Airports);
                serializer.SerializeAirports(Airports);

                Console.WriteLine("Serialization completed");
                #endregion
            }

            #region LINQ queries

            Console.WriteLine(new string('*', 50));
            #region List all the countries by name in an ascending order, and display the number of airports they have

            var airportsInCountry = Airports.GroupBy(c => c.Country.Name)
                                            .OrderBy(g => g.Key)
                                            .Select(g => new
                                            {
                                                ContryName = g.Key,
                                                Count = g.Count()
                                            }).ToList();


            foreach (var country in airportsInCountry)
            {
                Console.WriteLine($"{country.ContryName} has {country.Count} airports");
            }

            #endregion

            Console.WriteLine(new string('*', 50));

            #region Find the city which has got the most airports. If there are more than one cities with the same amount, display all of them.

            var cityWithMostAirpots = Airports.GroupBy(c => c.City.Name)
                                              .Select(c => new
                                              {
                                                  CityName = c.Key,
                                                  Count = c.Count()
                                              })
                                              .OrderByDescending(c => c.Count);

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

            #region Let the user enter an IATA code and display the following information about the given airport

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
                                         x.Location.Longitude <= 90 && x.Location.Longitude >= -90)
                                  .Select(x => new
                                  {
                                      coordinate = new GeoCoordinate(x.Location.Latitude, x.Location.Longitude, x.Location.Altitude),
                                      name = x.Name,
                                      city = x.City
                                  })
                                  .OrderBy(x => x.coordinate.GetDistanceTo(inputLocation))
                                  .First();

            Console.WriteLine($"{nearest.city.Name} {nearest.name} ");
            #endregion

            #endregion

            Console.ReadKey();

        }
    }

}



