using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAirportRepository : IRepository<Airport>
    {
        Airport GetAirportInfoByIATACode(List<Airport> airports, string IATACode);
        Airport GetClosestAirportByInputCoordinates(List<Airport> airports, double latitude, double longitude, double altitude);
    }
}
