using System;
using System.Collections.Generic;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer;
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
        IFileParser fileParser;

        public DataProcessor(IDataInstantiator dataInstantiator, IDataDAL dataDAL, IServiceBLL service, ICustomLogger logger, IFileParser fileParser)
        {
            this.dataInstantiator = dataInstantiator;
            this.dataDAL = dataDAL;
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

                    var timeZone = dataDAL.GetAirportsTimeZone(timeZoneAirports, currentId);

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

            Country country = dataInstantiator.GetCountry(currentId, countryName);

            City city = dataInstantiator.GetCity(currentId, cityName, currentId, timeZone, country);

            Func<double, double, double, Location> GetLocation = dataInstantiator.GetLocation;
            Airport airport = dataInstantiator.GetAirport(currentId, airportName, IATACode,
                                                          ICAOCode, GetLocation, longitude,
                                                          latitude, altitude, timeZone,
                                                          city, country, currentId, currentId);

            return airport;
        }
    }
}
