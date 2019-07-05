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
        private IDataDAL dataDAL;
        private IDataInstantiator dataInstantiator;
        ICustomLogger logger;
        IServiceBLL service;

        public DataProcessor(IDataDAL dataDAL, IServiceBLL service)
        {
            this.dataDAL = dataDAL;
            this.service = service;
        }

        public List<Airport> ProccessFile(string pathToFile, List<TimeZoneInformation> timeZoneAirports, ref int rowsIgnored)
        {
            List<Airport> result = new List<Airport>();

            foreach (var line in FileParser.GetAllLinesFromFile(pathToFile))
            {
                string[] items = service.TransformData(line);
                int currentId = items[0].ToInt();
                string airportName = items[1];

                var timeZone = dataDAL.GetAirportsTimeZone(timeZoneAirports, currentId);

                if (!service.IsValid(items, timeZone))
                {
                    
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

                Country country = dataInstantiator.GetCountry(currentId, countryName);

                City city = dataInstantiator.GetCity(currentId, cityName, currentId, timeZone, country);

                Func<double, double, double, Location> GetLocation = dataInstantiator.GetLocation;
                Airport airport = dataInstantiator.GetAirport(currentId, airportName, IATACode,
                                                              ICAOCode, GetLocation, longitude,
                                                              latitude, altitude, timeZone,
                                                              city, country, currentId, currentId);

                result.Add(airport);
            }

            return result;
        }
    }
}
