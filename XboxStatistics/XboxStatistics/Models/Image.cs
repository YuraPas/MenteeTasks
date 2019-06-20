namespace XboxStatistics.Models
{
    public class Image
    {
        public string ID { get; set; }
        public string Url { get; set; }
        public string ResizeUrl { get; set; }
        public string[] Purposes { get; set; }
        public string Purpose { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Order { get; set; }
    }
}