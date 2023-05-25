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
            public int highscore { get; set; }
            public int highlines { get; set; }
            public int lastscore { get; set; }
            public int lastlines { get; set; }
        }

        public static void CheckGameEnd()
        {
            if (Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, 1, 0) &&
                Controller.HasCollided(GameLoop.RunningTetriminoInstance?.Shape!, Game.Board!, -1, 0))
            {

                Game.Cts!.Cancel();

                Scores scores = new() { highscore = 0, highlines = 0, lastscore = 0, lastlines = 0 };


                if (Game.Score > Menu.HighScore)
                {
                    scores.highscore = Game.Score;
                    scores.lastscore = Game.Score;
                }
                else
                {
                    scores.highscore = Menu.HighScore;
                    scores.lastscore = Game.Score;
                }

                if (Game.Lines > Menu.HighLines)
                {
                    scores.highlines = Game.Lines;
                    scores.lastlines = Game.Lines;
                }
                else
                {
                    scores.highlines = Menu.HighLines;
                    scores.lastlines = Game.Lines;
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

                Menu.Main();
            }
        }
    }
}