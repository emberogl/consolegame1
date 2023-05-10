using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    internal class Delta
    {
        public static void TimeDelta()
        {
            for (double i = 0.0; i < 1; i += 0.1)
            {
                Thread.Sleep(Game.DeltaValue);
                Game.Time = i;
                Game.UpdateTimer();
                Game.Print(Game.Board!);
            }
            Game.Time = 0;
        }
    }
}
