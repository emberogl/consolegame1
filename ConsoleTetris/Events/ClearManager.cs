using Tetris.Inits;
using Tetris.Tasks;
using Tetris.Tetrimino_;

namespace Tetris.Events
{
    internal class ClearManager
    {
        public static bool lineCleared { get; set; } = false;
        public static void Clear()
        {
            do
            {
                lineCleared = false;
                for (int row = Game.Board!.GetLength(0) - 2; row >= 0; row--)
                {
                    bool isRowFilled = true;
                    for (int col = 0; col < Game.Board!.GetLength(1); col++)
                    {
                        if (col >= GameLoop.RunningTetriminoInstance?.X && col < GameLoop.RunningTetriminoInstance?.X + GameLoop.RunningTetriminoInstance?.GetSecondDimensionLength()
                            && row >= GameLoop.RunningTetriminoInstance?.Y && row < GameLoop.RunningTetriminoInstance?.Y + GameLoop.RunningTetriminoInstance?.GetFirstDimensionLength()
                            && GameLoop.RunningTetriminoInstance?.Shape![row - GameLoop.RunningTetriminoInstance.Y, col - GameLoop.RunningTetriminoInstance.X] == 1)
                        {
                            isRowFilled = false;
                            break;
                        }
                        if (Game.Board![row, col] != Game.TetriminoASCII)
                        {
                            isRowFilled = false;
                            break;
                        }
                    }

                    if (isRowFilled)
                    {
                        List<PlacedTetrimino> newTetriminos = new();
                        List<PlacedTetrimino> tetriminosToRemove = new();

                        foreach (PlacedTetrimino tetrimino in Game.PlacedTetriminos.ToArray()) // Copy the list for iteration
                        {
                            if (tetrimino != null)
                            {
                                for (int i = 0; i < tetrimino.Shape.GetLength(0); i++)
                                {
                                    if (tetrimino.Y + i == row)
                                    {
                                        for (int col = 0; col < Game.Board!.GetLength(1); col++)
                                        {
                                            Game.Board[row, col] = Game.BoardASCII;
                                        }

                                        int[,] upperShape = new int[i, tetrimino.Shape.GetLength(1)];
                                        int[,] lowerShape = new int[tetrimino.Shape.GetLength(0) - i - 1, tetrimino.Shape.GetLength(1)];

                                        Array.Copy(tetrimino.Shape, 0, upperShape, 0, i * tetrimino.Shape.GetLength(1));
                                        Array.Copy(tetrimino.Shape, (i + 1) * tetrimino.Shape.GetLength(1), lowerShape, 0, (tetrimino.Shape.GetLength(0) - i - 1) * tetrimino.Shape.GetLength(1));

                                        if (upperShape.GetLength(0) > 0)
                                        {
                                            newTetriminos.Add(new PlacedTetrimino(tetrimino.X, tetrimino.Y, tetrimino.Color, upperShape));
                                        }

                                        if (lowerShape.GetLength(0) > 0)
                                        {
                                            newTetriminos.Add(new PlacedTetrimino(tetrimino.X, tetrimino.Y + i + 1, tetrimino.Color, lowerShape));
                                        }

                                        // Add the original tetrimino to the removal list.
                                        tetriminosToRemove.Add(tetrimino);
                                        lineCleared = true;
                                        break; // Stop checking other rows once a split is detected
                                    }
                                }
                            }
                        }

                        // Remove the tetriminos from the game.
                        foreach (PlacedTetrimino tetrimino in tetriminosToRemove)
                        {
                            Game.PlacedTetriminos.Remove(tetrimino);
                        }

                        // Add the newly created tetriminos to the game.
                        foreach (PlacedTetrimino newTetrimino in newTetriminos)
                        {
                            Game.PlacedTetriminos.Add(newTetrimino);
                        }

                        Game.Score += 100;
                        Game.Lines += 1;
                        Game.UpdateScoreDisplay();
                        Printer.Print(Game.Board!, printscore: true);
                    }
                }
            } while (lineCleared);
        }
    }
}