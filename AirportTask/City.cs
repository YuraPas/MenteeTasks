using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTask
{
   public class City
    {
        public int CountryId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get; set; }
        public Country Country { get; set; }

        public static explicit operator CitySerialize(City city)
        {
            return new CitySerialize
            {
                Id = city.Id,
                Name = city.Name,
                CountryId = city.CountryId,
                TimeZoneName = city.TimeZoneName
            };
        }
    }
}
