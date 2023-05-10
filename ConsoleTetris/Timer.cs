using System.Diagnostics;

namespace Tetris
{
    internal class Timer
    {
        public static void ElapseTimer()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                TimeSpan elapsedTime = stopwatch.Elapsed;
                Game.Elapse = $"{(int)elapsedTime.TotalHours:00}:{elapsedTime:mm\\:ss}";
                Game.UpdateElapseTimer();
            }
        }
    }
}