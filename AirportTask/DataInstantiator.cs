using System;

namespace AirportTask
{
    public class DataInstantiator
    {
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
}
