using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using System;

namespace DataAccessLayer
{
    public class DataInstantiator : IDataInstantiator
    {

        public Location GetLocation(double longitude, double latitude, double altitude)
        {
            Location instance = new Location()
            {
                Longitude = longitude,
                Latitude = latitude,
                Altitude = altitude
            };

            return instance;
        }

        public Country GetCountry(int id, string name)
        {
            Country instance = new Country()
            {
                Id = id,
                Name = name
            };

            return instance;
        }

        public City GetCity(int id, string name, int countryId, string timeZone, Country country)
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

        public Airport GetAirport(int id, string name, string IATACode, string ICAOCode,
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
}
