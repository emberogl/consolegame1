namespace Tetris
{
    // Jeg har indtil videre ikke implementeret en system for at gemme placerede
    // tetriminoer og så tjekke om der sker en kollision med dem

    // Controller.cs indeholder Rotate() metoden
    class Game
    {
        public static int displayRow { get; } = 21;
        public static int displayCol { get; } = 10;
        public static string BoardASCII { get; } = "  ";
        public static string TetriminoASCII { get; } = "[]";
        //---------------------------------------------//
        public static string[,]? Board { get; set; }
        public static string[,]? Edge { get; set; }
        public static void Main()
        {
            Edge = new string[1, displayCol];
            for (int col = 0; col < displayCol; col++)
            {
                Edge[0, col] = "‾‾";
            }
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Tetris";
            Console.CursorVisible = false;
            Task.Run(() => GameLoop.LoopBegin());
            Controller._();
        }

        private static object _lock = new object();

        public static void Print(string[,] board)
        {
            lock (_lock)
            {
                Console.Clear();
                for (int row = 0; row < board.GetLength(0); row++)
                {
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        if (board[row, col] == TetriminoASCII)
                        {
                            Console.ForegroundColor = Tetrimino.Color;
                        }
                        Console.Write(board[row, col]);
                        Console.ResetColor();
                    }
                    if (board[row, displayCol - 1] != "‾‾")
                    {
                        Console.Write("|");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}