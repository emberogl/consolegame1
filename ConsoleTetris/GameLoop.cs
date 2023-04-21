namespace Tetris
{
    internal class GameLoop
    {
        public static Tetrimino? RunningTetriminoInstance { get; set; }
        public static void LoopBegin()
        {
            DrawBoard();
            Tetrimino tetrimino = new(Tetrimino.Select.Next(1, 6), -1) { IsActive = true };
            RunningTetriminoInstance = tetrimino;
            while (RunningTetriminoInstance.IsActive)
            {
                EraseTetriminoFromBoard(RunningTetriminoInstance, Game.Board!);
                RunningTetriminoInstance.Y += 1;
                DrawTetriminoOnBoard(RunningTetriminoInstance, Game.Board!);
                Game.Print(Game.Board!);
                Thread.Sleep(1000);
                if (Controller.HasCollided(RunningTetriminoInstance, Game.Board!))
                {
                    Console.WriteLine("has collided");
                    RunningTetriminoInstance.Y -= 4;
                }
            }
        }
        public static void DrawTetriminoOnBoard(Tetrimino tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.Shape.GetLength(1); col++)
                {
                    if (tetrimino.Shape[row, col] == 1)
                    {
                        int Row = tetrimino.Y + row;
                        int Col = tetrimino.X + col;

                        if (!Controller.IsOutOfBound(tetrimino, board))
                        {
                            board[Row, Col] = Game.TetriminoASCII;
                        }
                    }
                }
            }
        }
        public static void EraseTetriminoFromBoard(Tetrimino tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.Shape.GetLength(1); col++)
                {
                    if (tetrimino.Shape[row, col] == 1)
                    {
                        int Row = tetrimino.Y + row;
                        int Col = tetrimino.X + col;

                        if (Col >= 0 && Col < board.GetLength(1) && Row >= 0 && Row < board.GetLength(0))
                        {
                            board[Row, Col] = Game.BoardASCII;
                        }
                    }
                }
            }
        }

        public static void DrawBoard()
        {
            Game.Board = new string[Game.displayRow, Game.displayCol];
            for (int row = 0; row < Game.displayRow - 1; row++)
            {
                for (int col = 0; col < Game.displayCol; col++)
                {
                    Game.Board[row, col] = Game.BoardASCII;
                }
            }
            for (int col = 0; col < Game.displayCol; col++)
            {
            }
        }
    }
}