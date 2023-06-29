using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using Tetris.Inits;
using Tetris.Tasks;

namespace Tetris.Events
{
    internal class JSON
    {
        public class Scores
        {
            public int Highscore { get; set; }
            public int Highlines { get; set; }
            public int Lastscore { get; set; }
            public int Lastlines { get; set; }
        }
        // Gemmer score og lines i dokumenter som json
        public static readonly string jsonPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ScoreData.json");
        public static void CheckGameEnd()
        {
            // Tjekker om spillet er færdig
            if (Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 1, 0) &&
                Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 0, 1) &&
                GameLoop.RunningTetriminoInstance?.Y == 0
                ||
                Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 1, 0) &&
                Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 0, -1) &&
                GameLoop.RunningTetriminoInstance?.Y == 0)
            {
                        Scores scores = new() { Highscore = 0, Highlines = 0, Lastscore = 0, Lastlines = 0 };


                        if (Game.Score > Menu.HighScore)
                        {
                            scores.Highscore = Game.Score;
                            scores.Lastscore = Game.Score;
                        }
                        else
                        {
                            scores.Highscore = Menu.HighScore;
                            scores.Lastscore = Game.Score;
                        }

                        if (Game.Lines > Menu.HighLines)
                        {
                            scores.Highlines = Game.Lines;
                            scores.Lastlines = Game.Lines;
                        }
                        else
                        {
                            scores.Highlines = Menu.HighLines;
                            scores.Lastlines = Game.Lines;
                        }

                        string jsonString = JsonSerializer.Serialize(scores);

                        try
                        {
                            using StreamWriter sw = new(jsonPath);
                            sw.Write(jsonString);
                        }
                        catch (Exception) { }

                        try
                        {
                            Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
                              Path.GetFileName(Environment.ProcessPath!)));
                        }
                        catch (Exception) { }
                        Environment.Exit(0);
            }
        }
    }
}