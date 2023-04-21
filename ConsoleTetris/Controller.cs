namespace Tetris
{
    internal static class Controller
    {
        public static void _()
        {
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
            if (GameLoop.RunningTetriminoInstance != null)
            {
                if (GameLoop.RunningTetriminoInstance.IsActive)
                {
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.X -= 1;
                    if (IsOutOfBound(GameLoop.RunningTetriminoInstance, Game.Board!))
                    {
                        GameLoop.RunningTetriminoInstance.X += 1;
                    }
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Task.Run(() => Game.Print(Game.Board!));
                    _();
                }
            }
        }

        private static void MoveRight()
        {
            if (GameLoop.RunningTetriminoInstance != null)
            {
                if (GameLoop.RunningTetriminoInstance.IsActive)
                {
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.X += 1;
                    if (IsOutOfBound(GameLoop.RunningTetriminoInstance, Game.Board!))
                    {
                        GameLoop.RunningTetriminoInstance.X -= 1;
                    }
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Task.Run(() => Game.Print(Game.Board!));
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
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.Y += 1;
                    if (IsOutOfBound(GameLoop.RunningTetriminoInstance, Game.Board!))
                    {
                        GameLoop.RunningTetriminoInstance.Y -= 1;
                    }
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Task.Run(() => Game.Print(Game.Board!));
                    _();
                }
            }
        }

        public static void Rotate()
        {
            if (GameLoop.RunningTetriminoInstance != null)
            {
                if (GameLoop.RunningTetriminoInstance.IsActive)
                {
                    int Rows = GameLoop.RunningTetriminoInstance.Shape.GetLength(0);
                    int Columns = GameLoop.RunningTetriminoInstance.Shape.GetLength(1);
                    int[,] RotatedTetrimino = new int[Columns, Rows];
                    for (int row = 0; row < Rows; row++)
                    {
                        for (int col = 0; col < Columns; col++)
                        {
                            
                        // Her burde der være en condition til hvis det er muligt at rotere uden at go out of bounds of index of game.Board
                           RotatedTetrimino[col, Rows - 1 - row] = GameLoop.RunningTetriminoInstance.Shape[row, col];
                        }
                    }
                    GameLoop.EraseTetriminoFromBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    GameLoop.RunningTetriminoInstance.Shape = RotatedTetrimino;
                    GameLoop.DrawTetriminoOnBoard(GameLoop.RunningTetriminoInstance, Game.Board!);
                    Task.Run(() => Game.Print(Game.Board!));
                    _();
                }
            }

        }

        public static bool IsOutOfBound(Tetrimino tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.Shape.GetLength(1); col++)
                {
                    if (tetrimino.Shape[row, col] == 1)
                    {
                        int Col = tetrimino.X + col;
                        if (Col < 0 || Col >= board.GetLength(1))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static bool HasCollided(Tetrimino tetrimino, string[,] board)
        {
            // Mangler at tjekke hvis der sker en kollision med Game.Edge eller en liste af placerede tetriminoer
            return false;
        }
    }
}