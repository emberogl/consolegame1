namespace Tetris
{
    internal class TetriminoManager
    {
        public static void CycleComplete()
        {
            Tetrimino currentTetrimino = GameLoop.RunningTetriminoInstance!;

            for (int row = 0; row < currentTetrimino.Shape!.GetLength(0); row++)
            {
                for (int col = 0; col < currentTetrimino.Shape.GetLength(1); col++)
                {
                    if (currentTetrimino.Shape[row, col] == 1)
                    {
                        int Row = currentTetrimino.Y + row;
                        int Col = currentTetrimino.X + col;

                        Game.Board![Row, Col] = Game.TetriminoASCII;
                        Game.PlacedTetriminos.Add(new PlacedTetrimino(Col, Row, currentTetrimino.Color));
                    }
                }
            }

            Tetrimino newTetrimino = Tetrimino.NewTetrimino(Tetrimino.Select.Next(1, 6), -1);
            newTetrimino.IsActive = true;
            GameLoop.RunningTetriminoInstance = newTetrimino;
        }

    }
    public class PlacedTetrimino
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }

        public PlacedTetrimino(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }
    }
}
