using System;

namespace XboxStatistics.Models
{
    public class GameItem
    {
        public string MediaGroup { get; set; }
        public string MediaItemType { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ReducedDescription { get; set; }
        public string ReducedName { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int TitleId { get; set; }
        public string VuiDisplayName { get; set; }
        public Genre[] Genres { get; set; }
        public string DeveloperName { get; set; }
        public Image[] Images { get; set; }
        public string PublisherName { get; set; }
        public DateTime Updated { get; set; }
        public string ParentalRating { get; set; }
        public string ParentalRatingSystem { get; set; }
        public string SortName { get; set; }
        public Capability[] Capabilities { get; set; }
        public int KValue { get; set; }
        public string KValueNamespace { get; set; }
        public int HexTitleId { get; set; }
        public int AllTimeRatingCount { get; set; }
        public int ThirtyDaysRatingCount { get; set; }
        public int SevenDaysRatingCount { get; set; }
        public float AllTimeAverageRating { get; set; }
        public float ThirtyDaysAverageRating { get; set; }
        public float SevenDaysAverageRating { get; set; }
        public Legacyid[] LegacyIds { get; set; }
        public Availability[] Availabilities { get; set; }
        public string ResourceAccess { get; set; }
        public bool IsRetail { get; set; }
        public string ManualUrl { get; set; }
        public Parentalrating[] ParentalRatings { get; set; }
    }
}