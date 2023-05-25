using Tetris.Events;
using Tetris.Inits;
using Tetris.Tasks;

namespace Tetris.Tetrimino_
{
    internal class TetriminoManager
    {
        public static void CycleComplete()
        {
            JSON.CheckGameEnd();
            TetriminoQueue.StartQueue();
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
                    }
                }
            }

            Game.PlacedTetriminos.Add(new PlacedTetrimino(currentTetrimino.X, currentTetrimino.Y, currentTetrimino.Color, currentTetrimino.Shape));

            Tetrimino newTetrimino = TetriminoQueue.FirstInQueue!;
            newTetrimino.IsActive = true;
            GameLoop.RunningTetriminoInstance = newTetrimino;
            Game.UpdateQueue();
            Printer.Print(Game.Board!, printqueue: true);
        }

    }
    public class PlacedTetrimino
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }
        public int[,] Shape { get; set; }
        public PlacedTetrimino(int x, int y, ConsoleColor color, int[,] shape)
        {
            X = x;
            Y = y;
            Color = color;
            Shape = shape;
        }
    }
}
