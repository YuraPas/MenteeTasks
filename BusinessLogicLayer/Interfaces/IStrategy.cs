using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IStrategy
    {
        List<Airport> AssignGatheredData(List<Airport> airports, List<City> cities, List<Country> countries);
    }
}
