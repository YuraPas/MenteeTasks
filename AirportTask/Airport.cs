using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTask
{
    public class Airport
    {
        private string fullName;
        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string FullName
        {
            get
            {
                return Name + " Airport";
            }
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public string IATACode { get; set; }
        public string ICAOCode { get; set; }

        public string TimeZoneName { get; set; }

        public Location Location { get; set; }
        public City City { get; set; }
        public Country Country { get; set; }

        public static explicit operator AirportSerialize(Airport airport)
        {
            return new AirportSerialize
            {
                Id = airport.Id,
                IATACode = airport.IATACode,
                ICAOCode = airport.ICAOCode,
                Name = airport.Name,
                CityId = airport.CityId,
                CountryId = airport.CountryId,
                TimeZoneName = airport.TimeZoneName,
                Location = airport.Location
            };
        }

        public static explicit operator Airport(AirportSerialize airport)
        {
            return new Airport
            {
                Id = airport.Id,
                IATACode = airport.IATACode,
                ICAOCode = airport.ICAOCode,
                Name = airport.Name,
                CityId = airport.CityId,
                CountryId = airport.CountryId,
                TimeZoneName = airport.TimeZoneName,
                Location = airport.Location
            };
        }
    }
}
