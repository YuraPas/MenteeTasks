using System;

namespace XboxStatistics.Models
{
    public class Progression
    {
        public Requirement[] Requirements { get; set; }
        public DateTime TimeUnlocked { get; set; }
    }
}