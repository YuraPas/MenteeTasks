using DataAccessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AirportContext _context;

        public IAirportRepository Airports { get; private set; }
        public ICityRepository Cities { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public ILocationRepository Locations { get; private set; }


        public UnitOfWork(AirportContext context)
        {
            _context = context;
            Airports = new AirportRepository(_context);
            Cities = new CityRepository(_context);
            Countries = new CountryRepository(_context);
            Locations = new LocationRepository(_context);


        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
