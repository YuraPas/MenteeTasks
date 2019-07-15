using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using DataAccessLayer.SerializeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer
{
    public class DbDataDAL : IDataDAL
    {
        private AirportContext _context;

        public DbDataDAL()
        {
            _context = new AirportContext();
        }

        public List<Airport> GetAllAirports()
        {
            return _context.Airports.ToList();
        }

        public List<City> GetAllCities()
        {
            return _context.Cities.ToList();
        }

        public List<Country> GetAllCountries()
        {
            return _context.Countries.ToList();
        }
    }
}
