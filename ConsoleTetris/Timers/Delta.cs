using System.Diagnostics;
using Tetris.Events;
using Tetris.Inits;

namespace Tetris.Timers
{
    internal class Delta
    {
        public static double Velocity { get; set; }
        public static void TimeDelta()
        {
            Velocity = Math.Max(-Game.Lines / 100.0, -1.0);
            Stopwatch stopwatch = new();
            stopwatch.Start();

            double lastTime = 0.0;

            while (true)
            {
                Thread.Sleep(Game.DeltaValue);

                double elapsedTime = stopwatch.Elapsed.TotalSeconds;
                double deltaTime = elapsedTime - lastTime - Velocity;
                Game.Time += deltaTime;

                if (Game.Time >= 1.0)
                {
                    break;
                }

                lastTime = elapsedTime;

                Game.UpdateTimer();
                Printer.Print(Game.Board!, printdelta: true);
            }

            Game.Time = 0.0;
        }
    }
}