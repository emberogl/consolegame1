﻿namespace Tetris
{
    internal class GameLoop
    {
        public static Tetrimino? RunningTetriminoInstance { get; set; }
        public static void LoopBegin()
        {
            Task.Run(() => Timer.ElapseTimer());
            DrawBoard();
            Game.UpdateScoreDisplay();
            TetriminoQueue.StartQueue();
            Tetrimino tetrimino = TetriminoQueue.FirstInQueue!; tetrimino.IsActive = true;
            RunningTetriminoInstance = tetrimino;
            while (true)
            {
                EraseTetriminoFromBoard(RunningTetriminoInstance, Game.Board!);
                RunningTetriminoInstance.Y += 1;
                DrawTetriminoOnBoard(RunningTetriminoInstance, Game.Board!);
                Game.Print(Game.Board!);
                Delta.TimeDelta();
                if (Controller.HasCollided(RunningTetriminoInstance.Shape!, Game.Board!, 1, 0))
                {
                    TetriminoManager.CycleComplete();
                }
            }
        }
        public static void DrawTetriminoOnBoard(Tetrimino tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.Shape?.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.Shape.GetLength(1); col++)
                {
                    if (tetrimino.Shape[row, col] == 1)
                    {
                        int Row = tetrimino.Y + row;
                        int Col = tetrimino.X + col;

                        if (!Controller.IsOutOfBound(tetrimino.Shape, board))
                        {
                            board[Row, Col] = Game.TetriminoASCII;
                        }
                    }
                }
            }
        }
        public static void EraseTetriminoFromBoard(Tetrimino tetrimino, string[,] board)
        {
            for (int row = 0; row < tetrimino.Shape!.GetLength(0); row++)
            {
                for (int col = 0; col < tetrimino.Shape.GetLength(1); col++)
                {
                    if (tetrimino.Shape[row, col] == 1)
                    {
                        int Row = tetrimino.Y + row;
                        int Col = tetrimino.X + col;

                        if (Row >= 0 && Row < board.GetLength(0))
                        {
                            board[Row, Col] = Game.BoardASCII;
                        }
                    }
                }
            }
        }

        public static void DrawBoard()
        {
            Game.Board = new string[Game.DisplayRow, Game.DisplayCol];
            for (int row = 0; row < Game.DisplayRow; row++)
            {
                for (int col = 0; col < Game.DisplayCol; col++)
                {
                    Game.Board[row, col] = Game.BoardASCII;
                }
            }
            for (int col = 0; col < Game.DisplayCol; col++)
            {
                Game.Board[Game.DisplayRow - 1, col] = "[]";            
            }
        }
    }
}