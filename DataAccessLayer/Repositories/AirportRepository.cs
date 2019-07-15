using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using GeoCoordinatePortable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class AirportRepository : Repository<Airport>, IAirportRepository
    {
        public AirportRepository(AirportContext context) : base(context)
        {
        }

        public Airport GetAirportInfoByIATACode(List<Airport> airports, string IATACode)
        {
            return airports.Where(c => c.IATACode == IATACode).First();
        }

        public Airport GetClosestAirportByInputCoordinates(List<Airport> airports,double latitude, double longitude, double altitude)
        {
            var inputLocation = new GeoCoordinate(latitude, longitude, altitude);

            var nearestAirport = airports.Where(x => x.Location.Latitude <= 90 && x.Location.Latitude >= -90 &&
                                                             x.Location.Longitude <= 90 && x.Location.Longitude >= -90)
                                                 .OrderBy(x => new GeoCoordinate(x.Location.Latitude,
                                                                          x.Location.Longitude,
                                                                          x.Location.Altitude)
                                                                          .GetDistanceTo(inputLocation))
                                                 .First();
            return nearestAirport;
        }
    }
}
