using DataAccessLayer.SerializeModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string TimeZoneName { get; set; }


        public static explicit operator CitySerialize(City city)
        {
            return new CitySerialize
            {
                CityId = city.CityId,
                Name = city.Name,
                TimeZoneName = city.TimeZoneName
            };
        }
    }
}
