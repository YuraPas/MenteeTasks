using DataAccessLayer.Models;
using System.Collections.Generic;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDataSerializeBLL
    {
        void SerializeCounties(IEnumerable<Airport> airports);
        void SerializeAirports(IEnumerable<Airport> airports);
        void SerializeCities(IEnumerable<Airport> airports);
    }
}
