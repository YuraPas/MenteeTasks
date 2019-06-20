namespace XboxStatistics.Models
{
    public class Parentalrating
    {
        public string RatingId { get; set; }
        public int LegacyRatingId { get; set; }
        public string Rating { get; set; }
        public string RatingSystem { get; set; }
        public int RatingMinimumAge { get; set; }
        public LocalizedDetails LocalizedDetails { get; set; }
        public RatingDescriptor[] RatingDescriptors { get; set; }
    }
}