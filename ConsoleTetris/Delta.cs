using System.Diagnostics;

namespace Tetris
{
    internal class Delta
    {
        public static double Velocity { get; set; } = 0;
        public static void TimeDelta()
        {
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
                Game.Print(Game.Board!);
            }

            Game.Time = 0.0;
        }
    }
}