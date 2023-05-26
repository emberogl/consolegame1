using Tetris.Tetrimino_;
using Tetris.Tasks;

namespace Tetris.Inits
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
        public static void Start()
        {
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
    }
}