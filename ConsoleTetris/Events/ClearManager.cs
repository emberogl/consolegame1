using Tetris.Inits;
using Tetris.Tasks;
using Tetris.Tetrimino_;

namespace Tetris.Events
{
    internal class ClearManager
    {
        public static bool LineCleared { get; set; } = false;
        public static void Clear() // Metoden her er ansvarlig for at fjerne tetriminoer på board og så lave de "ødelagte" tetriminoer om til ny objekt
        {
            do
            {
                LineCleared = false;
                for (int row = Game.Board!.GetLength(0) - 2; row >= 0; row--) // Start fra bunden
                {
                    bool isRowFilled = true;
                    for (int col = 0; col < Game.Board!.GetLength(1); col++)
                    {
                        if (col >= GameLoop.RunningTetriminoInstance?.X && col < GameLoop.RunningTetriminoInstance?.X + GameLoop.RunningTetriminoInstance?.GetSecondDimensionLength()
                            && row >= GameLoop.RunningTetriminoInstance?.Y && row < GameLoop.RunningTetriminoInstance?.Y + GameLoop.RunningTetriminoInstance?.GetFirstDimensionLength()
                            && GameLoop.RunningTetriminoInstance?.Shape![row - GameLoop.RunningTetriminoInstance.Y, col - GameLoop.RunningTetriminoInstance.X] == 1)
                            // For at finde ud af om en række er kun brikker
                        {
                            isRowFilled = false;
                            break;
                        }
                        // Hvis [x, y] er ikke en Tetrimino
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

                        foreach (PlacedTetrimino tetrimino in Game.PlacedTetriminos.ToArray()) // Kopier liste for iteration
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

                                        // Laver array til øverste halve og nederste halve af tetrimino
                                        int[,] upperShape = new int[i, tetrimino.Shape.GetLength(1)];
                                        int[,] lowerShape = new int[tetrimino.Shape.GetLength(0) - i - 1, tetrimino.Shape.GetLength(1)];

                                        // Kopier øverste halve og nederste halve
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

                                        // Tilføj den originale Tetrimino til fjernelse-listen
                                        tetriminosToRemove.Add(tetrimino);
                                        LineCleared = true;
                                        break; // Hold op med at tjekke andre rækker når en opsplitning er opdaget
                                    }
                                }
                            }
                        }

                        // Fjern Tetriminoer fra spillet
                        foreach (PlacedTetrimino tetrimino in tetriminosToRemove)
                        {
                            Game.PlacedTetriminos.Remove(tetrimino);
                        }

                        // Tilføj de nye tetriminoer ind i spillet
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
            } while (LineCleared);
        }
    }
}