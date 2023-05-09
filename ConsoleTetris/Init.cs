namespace Tetris
{
    class Game
    {
        public static int DisplayRow { get; } = 20 + 1;
        public static int DisplayCol { get; } = 10;
        public static string[,] ScoreDisplay { get; set; } = new string[DisplayRow, 50];
        public static string BoardASCII { get; } = "  ";
        public static string TetriminoASCII { get; } = "[]";
        public static string[,]? Board { get; set; }
        public static int Score { get; set; } = 0;
        public static List<PlacedTetrimino> PlacedTetriminos { get; set; } = new List<PlacedTetrimino>();

        //---------------------------------------------//
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Tetris";
            Console.SetWindowSize(40, 21);
            ConsoleFontSize.SetConsoleFontSize(30);
            Console.CursorVisible = false;
            InitializeScoreDisplay();
            Task.Run(() => GameLoop.LoopBegin());
            Controller._();
        }

        private static void InitializeScoreDisplay()
        {
            for (int row = 0; row < 5; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                        ScoreDisplay[row, col] = BoardASCII;
                }
            }
            UpdateScoreDisplay();
        }

        public static void UpdateScoreDisplay()
        {
            string scoreString = $"Score: {Score}";

            for (int col = 0; col < scoreString.Length; col++)
            {
                ScoreDisplay[2, col] = scoreString[col].ToString();
            }
        }

        private static readonly object _lock = new();

        public static void Print(string[,] board)
        {
            lock (_lock)
            {
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == TetriminoASCII && GameLoop.RunningTetriminoInstance != null &&
                            row >= GameLoop.RunningTetriminoInstance.Y &&
                            row < GameLoop.RunningTetriminoInstance.Y + GameLoop.RunningTetriminoInstance.Shape!.GetLength(0) &&
                            col >= GameLoop.RunningTetriminoInstance.X &&
                            col < GameLoop.RunningTetriminoInstance.X + GameLoop.RunningTetriminoInstance.Shape.GetLength(1) &&
                            GameLoop.RunningTetriminoInstance.Shape[row - GameLoop.RunningTetriminoInstance.Y, col - GameLoop.RunningTetriminoInstance.X] == 1)
                        {
                            Console.ForegroundColor = GameLoop.RunningTetriminoInstance.Color;
                        }
                        else
                        {
                            foreach (PlacedTetrimino placedTetrimino in PlacedTetriminos)
                            {
                                if (placedTetrimino.X == col && placedTetrimino.Y == row)
                                {
                                    Console.ForegroundColor = placedTetrimino.Color;
                                    break;
                                }
                            }
                        }

                        Console.SetCursorPosition(col * 2, row);
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                    }

                        Console.SetCursorPosition(DisplayCol * 2, row);
                        Console.Write("[]");
                    
                }

                for (int row = 0; row < ScoreDisplay.GetLength(0); row++)
                {
                    Console.SetCursorPosition(DisplayCol * 2 + 6, row);

                    for (int col = 0; col < ScoreDisplay.GetLength(1); col++)
                    {
                        Console.Write(ScoreDisplay[row, col]);
                    }
                }
            }
        }

    }
}