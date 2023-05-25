using System.Reflection;
using System.Text.Json;
using Tetris.Console_;
using Tetris.Events;

namespace Tetris.Inits
{
    internal class Menu
    {
        public static char[] logo = "TETRIS".ToArray();
        public static char[] play = "[PLAY]".ToArray();
        public static char[] exit = "[EXIT]".ToArray();
        public static int selectedButton { get; set; } = 0;
        public static int LastScore { get; set; } = 0;
        public static int LastLines { get; set; } = 0;
        public static int HighScore { get; set; } = 0;
        public static int HighLines { get; set; } = 0;
        public static int rows = 16;
        public static int cols = 40;
        public static string[,] UI = new string[rows, cols];
        public static void Main()
        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Tetris";
            Console.SetWindowSize(90, Game.DisplayRow);
            ConsoleFontSize.SetConsoleFontSize(30);
            Console.CursorVisible = false;
            Initialize();
            ScoreScan();
            Thread.Sleep(100000);
        }
        public static void Initialize()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    UI[row, col] = Game.BoardASCII;
                }
            }
            for (int col = 0; col < cols; col++)
            {
                UI[rows - 1, col] = Game.TetriminoASCII;
            }

            for (int col = 0; col < logo.Length; col++)
            {
                UI[3, 16 + col] = logo[col].ToString();
            }
            for (int col = 0; col < logo.Length; col++)
            {
                UI[8, 12 + col] = play[col].ToString();
            }
            for (int col = 0; col < logo.Length; col++)
            {
                UI[8, 20 + col] = exit[col].ToString();
            }
        }
        public static void ScoreScan()
        {
            try
            {
                string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
                string filePath = Path.Combine(path, "ScoreData.json");
                string json;

                using (StreamReader sr = new(filePath))
                {
                    json = sr.ReadToEnd();
                }
                JsonDocument doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("highscore", out JsonElement highscorejson))
                {
                    HighScore = highscorejson.GetInt32();
                }
                if (doc.RootElement.TryGetProperty("highlines", out JsonElement highlinesjson))
                {
                    HighLines = highlinesjson.GetInt32();
                }
                if (doc.RootElement.TryGetProperty("lastscore", out JsonElement lastscorejson))
                {
                    LastScore = lastscorejson.GetInt32();
                }
                if (doc.RootElement.TryGetProperty("lastlines", out JsonElement lastlinesjson))
                {
                    LastLines = lastlinesjson.GetInt32();
                }
            }
            catch (Exception) { }
            string highscore = $"HIGHSCORE: {HighScore} POINTS {HighLines} LINES";
            string lastscore = $"LAST SCORE: {LastScore} POINTS {LastLines} LINES";
            for (int col = 0; col < highscore.Length; col++)
            {
                UI[11, 1 + col] = highscore[col].ToString();
            }
            for (int col = 0; col < lastscore.Length; col++)
            {
                UI[13, 1 + col] = lastscore[col].ToString();
            }
            Printer.Print(UI, printmenu: true);
            _();
        }

        public static void _()
        {
            while (Console.KeyAvailable)
            {
                Console.ReadKey(true);
            }

            var CKey = Console.ReadKey(true);
            switch (CKey.Key)
            {
                case ConsoleKey.LeftArrow:
                    selectedButton = 0;
                    Printer.Print(UI, printmenu: true);
                    _();
                    break;
                case ConsoleKey.RightArrow:
                    selectedButton = 1;
                    Printer.Print(UI, printmenu: true);
                    _();
                    break;
                case ConsoleKey.Enter:
                    if (selectedButton == 0)
                    {
                        Console.Clear();
                        Game.Start();
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
                default:
                    _();
                    break;
            }
        }
    }
}