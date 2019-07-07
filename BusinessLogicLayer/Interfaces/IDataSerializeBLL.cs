using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDataSerializeBLL
    {
        void SerializeCounties(IEnumerable<Airport> airports);
        void SerializeAirports(IEnumerable<Airport> airports);
        void SerializeCities(IEnumerable<Airport> airports);
        List<Airport> ProccessDataFromSerializeFiles();
    }
}
