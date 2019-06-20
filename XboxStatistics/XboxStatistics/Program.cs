using System;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            //Question("How many days did I play?", HowManyDaysDidIPlay);
            //Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
          
          
             Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);

            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }

        #region Completed

        static string HowManyGamesDoIHave()
        {
            //throw new NotImplementedException();
            int games = Xbox.MyGames.Count();

            return (games.ToString());
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore

            int completedGames = Xbox.MyGames.Where(g => g.CurrentGamerscore == g.MaxGamerscore).Count();

            return (completedGames.ToString());
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"

            var gamesRareAchievement = (from item in Xbox.Achievements
                                        from achievenemt in item.Value
                                        join game in Xbox.MyGames on achievenemt.ServiceConfigId equals game.ServiceConfigId
                                        where achievenemt.Rarity.CurrentCategory == "Rare"
                                        select game.Name).Distinct().ToList();


            return string.Join(",", gamesRareAchievement);
        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            var gamesRareAchievement = (from item in Xbox.Achievements
                                        from achievenemt in item.Value
                                        join game in Xbox.MyGames on achievenemt.ServiceConfigId equals game.ServiceConfigId
                                        where (item.Value.AsEnumerable().Count(row => row.Rarity.CurrentCategory == "Rare") > 0)
                                        group achievenemt by new
                                        {
                                            name = game.Name,
                                            count = item.Value.AsEnumerable().Count(row => row.Rarity.CurrentCategory == "Rare")
                                        }).ToList();
            var top3games = gamesRareAchievement.OrderByDescending(x => x.Key.count).Select(m => m.Key.name).Take(3).ToList();

            return string.Join(",", top3games);
        }

        static string HowMuchGamescoreDoIHave()
        {
            int gamerScore = Xbox.MyGames.Sum(g => g.CurrentGamerscore);

            return gamerScore.ToString();
        }

        #endregion

       
        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            int achievementsEarned = (from achievement in Xbox.Achievements
                                      from item in achievement.Value
                                      where item.ProgressState == "Achieved"
                                      select item).Count();


            return achievementsEarned.ToString();
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
             var stats = Xbox.MyGames.OrderBy(g=> g.Name).Select(g => g.Name ).ToList(); //.Where(g => g.Name == "Binding of Isaac")

            return string.Empty;
        }


        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            throw new NotImplementedException();
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            return String.Empty;
        }

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            throw new NotImplementedException();
        }

        static string WhichIsMyRarestAchievement()
        {
            return String.Empty;
        }
    }
}