using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IDataInstantiator
    {
        Location GetLocation(double longitude, double latitude, double altitude);

        Country GetCountry(string name);

        City GetCity(string name, string timeZone);

        Airport GetAirport(int id, string name, string IATACode, string ICAOCode,
                                        Func<double, double, double, Location> MethodName,
                                        double longitude, double latitude, double altitude, string timeZone,
                                        City city, Country country, int cityId, int countryId);
    }
}
