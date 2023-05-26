using System.Diagnostics;
using Tetris.Events;
using Tetris.Inits;

namespace Tetris.Timers
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
                Printer.Print(Game.Board!, printelapse: true);
            }
        }
    }
}