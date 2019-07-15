using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Name { get; set; }
        public string ThreeLetterISOCode { get; set; }
        public string TwoLetterISOCode { get; set; }
    }
}
