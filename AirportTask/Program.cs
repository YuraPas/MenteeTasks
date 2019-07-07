using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLogicLayer;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using GeoCoordinatePortable;
using Microsoft.Extensions.DependencyInjection;
using PresentationLayer.Interfaces;

namespace PresentationLayer
{
    public class Program
    {
        private static readonly string PathToDatFile = @"C:\Users\Рома\Downloads\airports.dat";
        private static readonly string PathToJsonFile = @"C:\Users\Рома\Downloads\timezoneinfo.json";
        private static IServiceProvider serviceProvider;
        private static IUserInterface userInterface = new ConsoleUserInterface();
        private static IDataSerializeBLL serializeBLL;

        public static void Main(string[] args)
        {
            RegisterServices();

            List<Airport> airports = new List<Airport>();
            serializeBLL = serviceProvider.GetService<IDataSerializeBLL>();
            var dataProcessor = serviceProvider.GetService<IDataProcessor>();
            var serviceBLL = serviceProvider.GetService<IServiceBLL>();
            var fileParser = serviceProvider.GetService<IFileParser>();

            if (serviceBLL.SerializedFilesExist())
            {
                airports = serializeBLL.ProccessDataFromSerializeFiles();
            }
            else
            {
                var timeZoneAirports = fileParser.ProccessJsonFile<TimeZoneInformation>(PathToJsonFile);

                int rowsIgnored = 0;
                airports = dataProcessor.ProccessFile(PathToDatFile, timeZoneAirports, ref rowsIgnored);

                ICustomLogger logger = new CustomLogger();
                logger.LogInfo($"Rows ignored: {rowsIgnored}");

                userInterface.Write($"{airports.Count} entries avavailable");

                SerializeAllData(airports);
                userInterface.Write("Serialization completed");
            }

            OutputSeparationLine();
            OutputAllCountriesWithNumberOfAirportsThenHave(airports);

            OutputSeparationLine();
            CityWithMostAirports(airports);

            OutputSeparationLine();
            DisplayAirportInfoByIATACode(airports);

            OutputSeparationLine();
            ClosestAirportByCoordinates(airports);

            DisposeServices();

            userInterface.ReadKey();
        }

        private static void SerializeAllData(List<Airport> airports)
        {
            serializeBLL.SerializeCities(airports);
            serializeBLL.SerializeCounties(airports);
            serializeBLL.SerializeAirports(airports);
        }

        #region LINQ queries

        private static void ClosestAirportByCoordinates(List<Airport> airports)
        {
            var inputLocation = new GeoCoordinate(latitude: 69.241186,longitude: 41.217860,altitude: 1400);
            var nearest = airports.Where(x => x.Location.Latitude <= 90 && x.Location.Latitude >= -90 &&
                                         x.Location.Longitude <= 90 && x.Location.Longitude >= -90)
                                  .Select(x => new
                                  {
                                      coordinate = new GeoCoordinate(x.Location.Latitude, x.Location.Longitude, x.Location.Altitude),
                                      name = x.Name,
                                      city = x.City,
                                  })
                                  .OrderBy(x => x.coordinate.GetDistanceTo(inputLocation))
                                  .First();

            userInterface.Write($"{nearest.city.Name} {nearest.name} ");
        }

        private static void DisplayAirportInfoByIATACode(List<Airport> airports)
        {
            Console.Write("Enter IATA code: ");
            string inputIATACode = Console.ReadLine();

            var searchedAirport = airports.Where(c => c.IATACode == inputIATACode).First();

            Console.WriteLine($"Name - {searchedAirport.FullName}, Country - {searchedAirport.Country.Name}, " +
                              $"City - {searchedAirport.City.Name} ");
        }

        private static void CityWithMostAirports(List<Airport> airports)
        {
            var cityWithMostAirpots = airports.GroupBy(c => c.City.Name)
                                              .Select(c => new
                                              {
                                                  CityName = c.Key,
                                                  Count = c.Count(),
                                              })
                                              .OrderByDescending(c => c.Count);

            var maxAirpotsCity = cityWithMostAirpots.Select(c => c).First();

            userInterface.Write($"City {maxAirpotsCity.CityName} has {maxAirpotsCity.Count} airports");
        }

        private static void OutputAllCountriesWithNumberOfAirportsThenHave(List<Airport> airports)
        {
            var airportsInCountry = airports.GroupBy(c => c.Country.Name)
                                           .OrderBy(g => g.Key)
                                           .Select(g => new
                                           {
                                               ContryName = g.Key,
                                               Count = g.Count(),
                                           })
                                           .ToList();

            airportsInCountry.ForEach(country => userInterface.Write($"{country.ContryName} has {country.Count} airports"));
        }

        #endregion

        private static void OutputSeparationLine()
        {
            userInterface.Write(new string('*', 50));
        }

        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            var builder = new ContainerBuilder();

            ConfigureServices(builder);

            builder.Populate(services);
            var appContainer = builder.Build();

            serviceProvider = new AutofacServiceProvider(appContainer);
        }

        private static void DisposeServices()
        {
            if (serviceProvider == null)
            {
                return;
            }

            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterType<DataSerializeBLL>().As<IDataSerializeBLL>();
            builder.RegisterType<ServiceBLL>().As<IServiceBLL>();
            builder.RegisterType<DataProcessor>().As<IDataProcessor>();
            builder.RegisterType<DataDAL>().As<IDataDAL>();
            builder.RegisterType<DataInstantiator>().As<IDataInstantiator>();
            builder.RegisterType<CustomLogger>().As<ICustomLogger>();
            builder.RegisterType<FileParser>().As<IFileParser>();
        }
    }
}