using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDataProcessor
    {
        List<Airport> ProccessFile(string pathToFile, List<TimeZoneInformation> timeZoneAirports, ref int rowsIgnored);
    }
}
