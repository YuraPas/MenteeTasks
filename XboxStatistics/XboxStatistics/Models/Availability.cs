namespace XboxStatistics.Models
{
    public class Availability
    {
        public string AvailabilityId { get; set; }
        public string ContentId { get; set; }
        public Device[] Devices { get; set; }
        public string OfferDisplayData { get; set; }
        public string SignedOffer { get; set; }
    }
}