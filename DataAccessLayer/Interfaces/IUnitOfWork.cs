using System;

namespace DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAirportRepository Airports { get; }
        ICityRepository Cities { get; }
        ICountryRepository Countries { get; }
        ILocationRepository Locations{ get; }
        int Complete();
    }
}
