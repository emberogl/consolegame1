namespace Tetris
{
    internal class TetriminoManager
    {
        public static void CycleComplete()
        {
            Console.WriteLine("has collided");
            GameLoop.RunningTetriminoInstance!.Y -= 4;
            //this is only for debugging
        }
    }
}
