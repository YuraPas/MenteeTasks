using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IServiceBLL
    {
        string[] SplitLine(string line);
        bool IsValid(string[] items, string timeZone);
        bool SerializedFilesExist();
        List<Airport> AddLocationToAirport(List<Airport> airports);
    }
}
