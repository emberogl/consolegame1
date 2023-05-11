namespace Tetris
{
    internal static class Controller
    {
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
                    MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.R:
                    Rotate();
                    break;
                default:
                    _();
                    break;
            }
        }

        private static void MoveLeft()
        {
            if (GameLoop.RunningTetriminoInstance != null && GameLoop.RunningTetriminoInstance.IsActive)
            {
                if (!HasCollided(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!, 0, -1))
                {
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.X -= 1;
                    if (IsOutOfBound(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!))
                    {
                        GameLoop.RunningTetriminoInstance.X += 1;
                    }
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Game.Print(Game.Board!);
                    _();
                }
                else
                {
                    _();
                }
            }
        }

        private static void MoveRight()
        {
            if (GameLoop.RunningTetriminoInstance != null && GameLoop.RunningTetriminoInstance.IsActive)
            {
                if (!HasCollided(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!, 0, 1))
                {
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.X += 1;
                    if (IsOutOfBound(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!))
                    {
                        GameLoop.RunningTetriminoInstance.X -= 1;
                    }
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Game.Print(Game.Board!);
                    _();
                }
                else
                {
                    _();
                }
            }
        }
        private static void MoveDown()
        {
            if (GameLoop.RunningTetriminoInstance != null)
            {
                if (GameLoop.RunningTetriminoInstance.IsActive)
                {
                    if (!HasCollided(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!, 1, 0))
                    {
                        Game.Score += 2;
                        Game.UpdateScoreDisplay();
                        GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                        GameLoop.RunningTetriminoInstance.Y += 1;
                        if (IsOutOfBound(GameLoop.RunningTetriminoInstance.Shape!, Game.Board!))
                        {
                            GameLoop.RunningTetriminoInstance.Y -= 1;
                        }
                        GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                        Game.Print(Game.Board!);
                        _();
                    }
                    else
                    {
                        TetriminoManager.CycleComplete();
                        GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                        GameLoop.RunningTetriminoInstance.Y += 1;
                        GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                        _();
                    }
                }
            }
        }

        public static void Rotate()
        {
            if (GameLoop.RunningTetriminoInstance != null)
            {
                if (GameLoop.RunningTetriminoInstance.IsActive)
                {
                    int Rows = GameLoop.RunningTetriminoInstance.Shape!.GetLength(0);
                    int Columns = GameLoop.RunningTetriminoInstance.Shape.GetLength(1);
                    Stack<int[,]> Stack = new();
                    int[,] RotatedTetrimino = new int[Columns, Rows];
                    for (int row = 0; row < Rows; row++)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            RotatedTetrimino[col, Rows - 1 - row] = GameLoop.RunningTetriminoInstance.Shape[row, col];
                        }
                    }
                    Stack.Push(GameLoop.RunningTetriminoInstance.Shape);
                    Stack.Push(RotatedTetrimino);
                    if (IsOutOfBound(RotatedTetrimino, Game.Board!) || HasCollided(RotatedTetrimino, Game.Board!, 0, 0))
                    {
                        Stack.Pop();
                    }
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.Shape = Stack.Peek();
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Game.Print(Game.Board!);
                    _();
                }
            }

        }

        public static bool IsOutOfBound(int[,] tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.GetLength(1); col++)
                {
                    if (tetrimino[row, col] == 1)
                    {
                        int Col = GameLoop.RunningTetriminoInstance!.X + col;
                        if (Col < 0 || Col >= board.GetLength(1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool HasCollided(int[,] tetrimino, string[,] board, int rowOffset, int colOffset)
        {
            string[,] boardCopy = (string[,])board.Clone();
            GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance!, boardCopy);

            for (int row = 0; row < tetrimino.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.GetLength(1); col++)
                {
                    if (tetrimino[row, col] != 0)
                    {
                        int boardRow = GameLoop.RunningTetriminoInstance!.Y + row + rowOffset;
                        int boardCol = GameLoop.RunningTetriminoInstance!.X + col + colOffset;

                        if (boardRow < 0 || boardRow >= boardCopy.GetLength(0) || boardCol < 0 || boardCol >= boardCopy.GetLength(1))
                        {
                            return true;
                        }
                        else if (boardCopy[boardRow, boardCol] == Game.TetriminoASCII)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }





    }
}