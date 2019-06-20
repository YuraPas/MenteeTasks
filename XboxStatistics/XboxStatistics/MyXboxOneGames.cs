using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using XboxStatistics.Models;

namespace XboxStatistics
{
    public class MyXboxOneGames
    {
        public GameTitle[] MyGames { get; }
        public Dictionary<int, XboxOneGame> GameDetails { get; }
        public Dictionary<int, Stat[]> GameStats { get; }
        public Dictionary<int, Achievement[]> Achievements { get; }

        public MyXboxOneGames()
        {
            Directory.SetCurrentDirectory(@"..\..\Data");
            var all = JsonConvert.DeserializeObject<XboxOneGameCollection>(File.ReadAllText("all.json"));
            MyGames = all.Titles.Where(t => t.TitleType.EndsWith("Game")).ToArray();
            GameDetails = new Dictionary<int, XboxOneGame>();
            GameStats = new Dictionary<int, Stat[]>();
            Achievements = new Dictionary<int, Achievement[]>();
            foreach (var game in MyGames)
            {
                var details = ReadFile<XboxOneGame>($"{game.HexId}.details.json");
                var stats = ReadFile<GameStats>($"{game.HexId}.stats.json");
                var achievements = ReadFile<Achievement[]>($"{game.HexId}.achievements.json");
                if (details != null) GameDetails.Add(game.TitleId, details);
                if (stats != null)
                {
                    var statList = stats.Groups.SelectMany(g => g.StatlistsCollection.SelectMany(c => c.Stats)).ToList();
                    statList.AddRange(stats.StatlistsCollection.SelectMany(c => c.Stats));
                    GameStats.Add(game.TitleId, statList.ToArray());
                }
                if (achievements != null) Achievements.Add(game.TitleId, achievements);
            }
        }

        private static T ReadFile<T>(string name)
        {
            if (!File.Exists(name)) return default(T);
            var json = File.ReadAllText(name);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}