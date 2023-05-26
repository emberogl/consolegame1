using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using Tetris.Inits;
using Tetris.Tasks;
using Tetris.Timers;

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

        public static void CheckGameEnd()
        {
            if (Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 1, 0) &&
                Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, -1, 0))
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
                    string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                    string filePath = Path.Combine(path, "ScoreData.json");
                    using (StreamWriter sw = new(filePath))
                    {
                        sw.Write(jsonString);
                    }
                }
                catch (Exception) { }

                Game.Lines = 0; Game.Score = 0; Game.PlacedTetriminos.Clear(); Game.Time = 0.0; Game.DeltaValue = 50;
                Game.Board = null; Game.Elapse = null; Delta.Velocity = 0;

                try { Process.Start(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                      Path.GetFileName(Process.GetCurrentProcess().MainModule!.FileName!)));
                } catch (Exception) { }
                Environment.Exit(0);
            }
        }
    }
}