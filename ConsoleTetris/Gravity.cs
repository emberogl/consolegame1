namespace Tetris
{
    internal class Gravity
    {
        public static void Watch() 
        { 
            while (true) 
            {
                if (Game.PlacedTetriminos.Count != 0) 
                {
                    foreach (PlacedTetrimino tetrimino in Game.PlacedTetriminos)
                    {
                        if (!HasCollided(tetrimino, Game.Board!, 1, 0))
                        {
                            GameLoop.EraseTetriminoFromBoard(tetrimino, Game.Board!);
                            tetrimino.Y += 1;
                            GameLoop.DrawTetriminoOnBoard(tetrimino, Game.Board!);
                        }
                    }
                    Thread.Sleep(500);
                }
            }
        }
        public static bool HasCollided(PlacedTetrimino tetriminoInstance, string[,] board, int rowOffset, int colOffset)
        {
            string[,] boardCopy = (string[,])board.Clone();
            GameLoop.EraseTetriminoFromBoard(tetriminoInstance, boardCopy);

            for (int row = 0; row < tetriminoInstance.Shape.GetLength(0); row++)
            {
                for (int col = 0; col < tetriminoInstance.Shape.GetLength(1); col++)
                {
                    if (tetriminoInstance.Shape[row, col] != 0)
                    {
                        int boardRow = tetriminoInstance.Y + row + rowOffset;
                        int boardCol = tetriminoInstance.X + col + colOffset;

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
