using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;

namespace BusinessLogicLayer
{
    public class DataProcessor : IDataProcessor
    {
        IDataDAL dataDAL;
        IDataInstantiator dataInstantiator;
        ICustomLogger logger;
        IServiceBLL service;
        IFileService fileParser;

        public DataProcessor(IDataDAL dataDAL, IDataInstantiator dataInstantiator, IServiceBLL service, ICustomLogger logger, IFileService fileParser)
        {
            this.dataDAL = dataDAL;
            this.dataInstantiator = dataInstantiator;
            this.service = service;
            this.logger = logger;
            this.fileParser = fileParser;
        }

        public List<Airport> ProccessFile(string pathToFile, List<TimeZoneInformation> timeZoneAirports, ref int rowsIgnored)
        {
            List<Airport> result = new List<Airport>();

            foreach (var line in fileParser.GetAllLinesFromFile(pathToFile))
            {
                try
                {
                    string[] items = service.SplitLine(line);

                    int currentId = items[0].ToInt();
                    string airportName = items[1];

                    var timeZone = GetAirportsTimeZone(timeZoneAirports, currentId);

                    if (!service.IsValid(items, timeZone))
                    {
                        logger.LogInfo($"Invalid row: {currentId} - {airportName}");
                        rowsIgnored++;

                        continue;
                    }

                    Airport airport = TransformLineToAirportObject(items, timeZone);

                    result.Add(airport);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                }
            }

            return result;
        }

        public List<Airport> ProccessData()
        {
            var airports = dataDAL.GetAllAirports();
            var cities = dataDAL.GetAllCities();
            var countries = dataDAL.GetAllCountries();

            airports = service.AssignGatheredData(airports, cities, countries);

            return airports;
        }

        private string GetAirportsTimeZone(List<TimeZoneInformation> timeZoneAirports, int searchId)
        {
            var timeZone = timeZoneAirports.Where(c => c.AirportId == searchId)
                                           .Select(c => c.TimeZoneInfoId)
                                           .FirstOrDefault();

            return timeZone;
        }

        private Airport TransformLineToAirportObject(string[] items, string timeZone)
        {
            int currentId = items[0].ToInt();
            string airportName = items[1];
            string cityName = items[2];
            string countryName = items[3];
            string IATACode = items[4];
            string ICAOCode = items[5];
            double longitude = items[6].ToDouble();
            double latitude = items[7].ToDouble();
            double altitude = items[8].ToDouble();

            Country country = dataInstantiator.GetCountry(countryName);

            City city = dataInstantiator.GetCity(cityName, timeZone);

            Func<double, double, double, Location> GetLocation = dataInstantiator.GetLocation;
            Airport airport = dataInstantiator.GetAirport(currentId, airportName, IATACode,
                                                          ICAOCode, GetLocation, longitude,
                                                          latitude, altitude, timeZone,
                                                          city, country, currentId, currentId);

            return airport;
        }
    }
}
