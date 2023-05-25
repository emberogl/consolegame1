using Tetris.Inits;

namespace Tetris.Tetrimino_
{
    internal class TetriminoQueue
    {
        public static List<Tetrimino>? Queue { get; set; } = new List<Tetrimino>();
        public static Tetrimino? FirstInQueue { get; set; }
        public static bool FirstRunTrue { get; set; } = false;
        public static void LoadQueue()
        {
            if (Queue!.Count == 0)
            {
                while (Queue.Count < 3)
                {
                    Queue.Add(Tetrimino.NewTetrimino(Tetrimino.Select.Next(1, 6), -1));
                }
            }
            else if (Queue.Count == 2)
            {
                Queue.Add(Tetrimino.NewTetrimino(Tetrimino.Select.Next(1, 6), -1));
            }
        }
        public static void StartQueue()
        {
            if (!FirstRunTrue)
            {
                LoadQueue();
                FirstRunTrue = true;
            }
            Tetrimino LastInQueue = Queue![0];
            Queue.RemoveAt(0);
            FirstInQueue = LastInQueue;
            LoadQueue();
            Game.UpdateQueue();
        }
    }
}