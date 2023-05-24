namespace Tetris
{
    class Game
    {
        //---------------------------------------------//
        public static int DisplayRow { get; } = 21;
        public static int DisplayCol { get; } = 10;
        //---------------------------------------------//
        public static string[,] ScoreDisplay { get; set; } = new string[DisplayRow, 50];
        public static string[,] TimeDisplay { get; set; } = new string[DisplayRow, 50];
        public static string[,] ElapseDisplay { get; set; } = new string[DisplayRow, 50];
        public static Tetrimino[,] QueueDisplay { get; set; } = new Tetrimino[DisplayRow, 10];
        //---------------------------------------------//
        public static double? Time { get; set; } = 0.0;
        public static string? Elapse { get; set; }
        public static int DeltaValue { get; set; } = 50;
        //---------------------------------------------//
        public static string BoardASCII { get; } = "  ";
        public static string TetriminoASCII { get; } = "[]";
        public static string[,]? Board { get; set; }
        public static int Score { get; set; } = 0;
        public static int Lines { get; set; } = 0;
        public static List<PlacedTetrimino> PlacedTetriminos { get; set; } = new List<PlacedTetrimino>();

        //---------------------------------------------//
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Tetris";
            Console.SetWindowSize(90, DisplayRow);
            ConsoleFontSize.SetConsoleFontSize(30);
            Console.CursorVisible = false;
            InitializeScoreDisplay();
            InitializeTimer();
            InitializeElapseTimer();
            Task.Run(() => Gravity.Watch());
            Task.Run(() => GameLoop.LoopBegin());
            Controller._();
        }
        private static void InitializeTimer()
        {
            for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    ScoreDisplay[row, col] = BoardASCII;
                }
            }
            UpdateTimer();
        }
        private static void InitializeScoreDisplay()
        {
            for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                        ScoreDisplay[row, col] = BoardASCII;
                }
            }
            UpdateScoreDisplay();
        }
        private static void InitializeElapseTimer()
        {
            for (int row = 0; row < 1; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    ElapseDisplay[row, col] = BoardASCII;
                }
            }
            UpdateElapseTimer();
        }

        public static void UpdateQueue()
        {
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    QueueDisplay[5 + row, col] = null!;
                }
            }
            for (int row = 0; row < 15; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    int offset = 0;
                    foreach (var tetrimino in TetriminoQueue.Queue!)
                    {
                        for (int Row = 0; Row < tetrimino.GetFirstDimensionLength(); Row++)
                        {
                            for (int Col = 0; Col < tetrimino.GetSecondDimensionLength(); Col++)
                            {
                                if (tetrimino.Shape![Row, Col] == 1)
                                {
                                    QueueDisplay[6 + Row + offset, Col] = tetrimino;
                                }
                            }
                        }
                        offset += 4;
                    }
                }
            }

        }

        public static void UpdateScoreDisplay()
        {
            string scoreString = $"Score: {Score} Lines: {Lines}";

            for (int col = 0; col < scoreString.Length; col++)
            {
                ScoreDisplay[2, col] = scoreString[col].ToString();
            }
        }
        public static void UpdateTimer()
        {
            string timer = $"Delta: {Time:F1}";

            for (int col = 0; col < timer.Length; col++)
            {
                TimeDisplay[18, col] = timer[col].ToString();
            }
        }
        public static void UpdateElapseTimer()
        {
           string elapse = $"Elapse: {Elapse}";

            for (int col = 0; col < elapse.Length; col++)
            {
                ElapseDisplay[19, col] = elapse[col].ToString();
            }
        }
        private static readonly object _lock = new();
        public static void Print(string[,] board, bool printboard = false, bool printqueue = false, bool printscore = false, bool printelapse = false, bool printdelta = false)
        {
            lock (_lock)
            {
                if (printboard)
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
                                foreach (PlacedTetrimino placedTetrimino in PlacedTetriminos.ToList())
                                {
                                    if (placedTetrimino != null)
                                    {
                                        for (int i = 0; i < placedTetrimino.Shape.GetLength(0); i++)
                                        {
                                            for (int j = 0; j < placedTetrimino.Shape.GetLength(1); j++)
                                            {
                                                if (placedTetrimino.Shape[i, j] == 1 && placedTetrimino.Y + i == row && placedTetrimino.X + j == col)
                                                {
                                                    Console.ForegroundColor = placedTetrimino.Color;
                                                    break;
                                                }
                                            }
                                        }
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
                }
                if (printqueue)
                {
                    for (int row = 6; row < 16; row++)
                    {
                        for (int col = 0; col < QueueDisplay.GetLength(1); col++)
                        {
                            Console.SetCursorPosition(DisplayCol * 2 + 10 + (col * 2), row);
                            if (QueueDisplay[row, col] != null)
                            {
                                Console.ForegroundColor = QueueDisplay[row, col].Color;
                                Console.Write(TetriminoASCII);
                            }
                            else
                            {
                                Console.Write(BoardASCII);
                            }
                            Console.ResetColor();
                        }
                    }
                }
                if (printscore)
                {
                    for (int row = 0; row < ScoreDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(DisplayCol * 2 + 6, row);

                        for (int col = 0; col < ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(ScoreDisplay[row, col]);
                        }
                    }
                }
                if (printdelta)
                {
                    for (int row = 0; row < TimeDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(DisplayCol * 2 + 6, row);

                        for (int col = 0; col < ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(TimeDisplay[row, col]);
                        }
                    }
                }
                if (printelapse)
                {
                    for (int row = 0; row < ElapseDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(DisplayCol * 2 + 6, row);

                        for (int col = 0; col < ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(ElapseDisplay[row, col]);
                        }
                    }
                }
            }
        }
    }
}