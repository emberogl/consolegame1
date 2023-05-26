using Tetris.Tasks;
using Tetris.Tetrimino_;
using Tetris.Inits;

namespace Tetris.Events
{
    internal class Printer
    {
        private static readonly object _lock = new();
        public static void Print(string[,] board, bool printboard = false, bool printqueue = false, bool printscore = false, bool printelapse = false, bool printdelta = false, bool printmenu = false)
        {
            lock (_lock)
            {
                if (printboard)
                {
                    for (int row = 0; row < board.GetLength(0); row++)
                    {
                        for (int col = 0; col < board.GetLength(1); col++)
                        {
                            if (board[row, col] == Game.TetriminoASCII && GameLoop.RunningTetriminoInstance != null &&
                                row >= GameLoop.RunningTetriminoInstance.Y &&
                                row < GameLoop.RunningTetriminoInstance.Y + GameLoop.RunningTetriminoInstance?.Shape!.GetLength(0) &&
                                col >= GameLoop.RunningTetriminoInstance!.X &&
                                col < GameLoop.RunningTetriminoInstance.X + GameLoop.RunningTetriminoInstance?.Shape!.GetLength(1) &&
                                GameLoop.RunningTetriminoInstance!.Shape![row - GameLoop.RunningTetriminoInstance.Y, col - GameLoop.RunningTetriminoInstance.X] == 1)
                            {
                                Console.ForegroundColor = GameLoop.RunningTetriminoInstance.Color;
                            }
                            else
                            {
                                foreach (PlacedTetrimino placedTetrimino in Game.PlacedTetriminos.ToList())
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
                        Console.SetCursorPosition(Game.DisplayCol * 2, row);
                        Console.Write("[]");
                    }
                }
                if (printqueue)
                {
                    for (int row = 6; row < 16; row++)
                    {
                        for (int col = 0; col < Game.QueueDisplay.GetLength(1); col++)
                        {
                            Console.SetCursorPosition(Game.DisplayCol * 2 + 10 + col * 2, row);
                            if (Game.QueueDisplay[row, col] != null)
                            {
                                Console.ForegroundColor = Game.QueueDisplay[row, col].Color;
                                Console.Write(Game.TetriminoASCII);
                            }
                            else
                            {
                                Console.Write(Game.BoardASCII);
                            }
                            Console.ResetColor();
                        }
                    }
                }
                if (printscore)
                {
                    for (int row = 0; row < Game.ScoreDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(Game.DisplayCol * 2 + 6, row);

                        for (int col = 0; col < Game.ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(Game.ScoreDisplay[row, col]);
                        }
                    }
                }
                if (printdelta)
                {
                    for (int row = 0; row < Game.TimeDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(Game.DisplayCol * 2 + 6, row);

                        for (int col = 0; col < Game.ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(Game.TimeDisplay[row, col]);
                        }
                    }
                }
                if (printelapse)
                {
                    for (int row = 0; row < Game.ElapseDisplay.GetLength(0); row++)
                    {
                        Console.SetCursorPosition(Game.DisplayCol * 2 + 6, row);

                        for (int col = 0; col < Game.ScoreDisplay.GetLength(1); col++)
                        {
                            Console.Write(Game.ElapseDisplay[row, col]);
                        }
                    }
                }
                if (printmenu)
                {
                    for (int row = 0; row < Menu.UI!.GetLength(0); row++)
                    {
                        for (int col = 0; col < Menu.UI!.GetLength(1); col++)
                        {
                            if (board[row, col] == "T") { Console.ForegroundColor = ConsoleColor.Green; }
                            if (board[row, col] == "E") { Console.ForegroundColor = ConsoleColor.Red; }
                            if (board[row, col] == "R") { Console.ForegroundColor = ConsoleColor.Blue; }
                            if (board[row, col] == "I") { Console.ForegroundColor = ConsoleColor.Yellow; }
                            if (board[row, col] == "S") { Console.ForegroundColor = ConsoleColor.Magenta; }
                            if (int.TryParse(board[row, col], out _)) { Console.ForegroundColor = ConsoleColor.Cyan; }
                            Console.SetCursorPosition(col * 2, row);
                            Console.Write(board[row, col]);
                            Console.ResetColor();
                        }
                        Console.SetCursorPosition(Menu.cols * 2, row);
                        Console.Write("[]");
                    }
                    string rowAsString = "";
                    for (int col = 0; col < board.GetLength(1); col++)
                    {
                        rowAsString += board[8, col];
                    }
                    if (Menu.SelectedButton == 0)
                    {
                        if (rowAsString.Contains("[PLAY]"))
                        {
                            for (int col = 0; col < Menu.logo.Length; col++)
                            {
                                Console.SetCursorPosition(24 + col * 2, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(board[8, 12 + col]);
                            }
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        if (rowAsString.Contains("[EXIT]"))
                        {
                            for (int col = 0; col < Menu.exit.Length; col++)
                            {
                                Console.SetCursorPosition(40 + col * 2, 8);
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write(board[8, 20 + col]);
                            }
                            Console.ResetColor();
                        }
                    }
                }
            }
        }
    }
}
