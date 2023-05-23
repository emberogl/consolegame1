namespace Tetris
{
    internal class ClearManager
    {
        public static void Clear()
        {
            for (int row = Game.Board!.GetLength(0) - 2; row >= 0; row--)
            {
                bool isRowFilled = true;
                for (int col = 0; col < Game.Board!.GetLength(1); col++)
                {
                    if (Game.Board![row, col] != Game.TetriminoASCII)
                    {
                        isRowFilled = false;
                        break;
                    }
                }

                if (isRowFilled)
                {
                    for (int col = 0; col < Game.Board!.GetLength(1); col++)
                    {
                        Game.Board[row, col] = Game.BoardASCII;
                    }

                    foreach (PlacedTetrimino tetrimino in Game.PlacedTetriminos)
                    {
                        for (int i = 0; i < tetrimino.Shape.GetLength(0); i++)
                        {
                            if (tetrimino.Y + i == row)
                            {
                                for (int j = 0; j < tetrimino.Shape.GetLength(1); j++)
                                {
                                    if (tetrimino.Shape[i, j] == 1)
                                    {
                                        tetrimino.Shape[i, j] = 0;
                                    }
                                }
                            }
                        }
                    }

                    Game.Score += 100;
                    Game.Lines += 1;
                    Game.UpdateScoreDisplay();
                    Game.Print(Game.Board!, printscore: true);
                }
            }
        }
    }
}
