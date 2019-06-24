using System;
using System.Collections.Generic;
using System.Text;

namespace AirportTask
{
    public class Airport
    {
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
    }
}
