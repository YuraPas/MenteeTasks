using System;
using XboxStatistics.Extensions;

namespace XboxStatistics.Models
{
    public class GameTitle
    {
        public DateTime LastUnlock { get; set; }
        public int TitleId { get; set; }

        private string _hexId;
        public string HexId
        {
            get
            {
                if (_hexId != null) return _hexId;
                var a = BitConverter.GetBytes(TitleId);
                a.SwapEndian(4);
                _hexId = a.ToHex();
                return _hexId;
            }
        }

        public string ServiceConfigId { get; set; }
        public string TitleType { get; set; }
        public string Platform { get; set; }
        public string Name { get; set; }
        public int EarnedAchievements { get; set; }
        public int CurrentGamerscore { get; set; }
        public int MaxGamerscore { get; set; }
    }

}
