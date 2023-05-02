namespace Tetris
{
    public abstract class Tetrimino
    {
        public readonly static Random Select = new();
        public int[,]? Shape;
        public bool IsActive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color { get; set; }

        protected Tetrimino(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Tetrimino NewTetrimino(int x, int y)
        {
            int randomTetrimino = Select.Next(0, 7);

            return randomTetrimino switch
            {
                0 => new I(x, y),
                1 => new J(x, y),
                2 => new L(x, y),
                3 => new O(x, y),
                4 => new S(x, y),
                5 => new T(x, y),
                6 => new Z(x, y),
                _ => throw new ArgumentException("Invalid tetrimino value")
            };
        }
    }

    public class I : Tetrimino
    {
        public I(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 1, 1, 1, 1 } };
            Color = ConsoleColor.Cyan;
        }
    }

    public class J : Tetrimino
    {
        public J(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 1, 0, 0 }, { 1, 1, 1 } };
            Color = ConsoleColor.Blue;
        }
    }

    public class L : Tetrimino
    {
        public L(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 0, 0, 1 }, { 1, 1, 1 } };
            Color = ConsoleColor.Magenta;
        }
    }

    public class O : Tetrimino
    {
        public O(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 1, 1 }, { 1, 1 } };
            Color = ConsoleColor.Yellow;
        }
    }

    public class S : Tetrimino
    {
        public S(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
            Color = ConsoleColor.Green;
        }
    }

    public class T : Tetrimino
    {
        public T(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 0, 1, 0 }, { 1, 1, 1 } };
            Color = ConsoleColor.DarkMagenta;
        }
    }

    public class Z : Tetrimino
    {
        public Z(int x, int y) : base(x, y)
        {
            Shape = new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
            Color = ConsoleColor.Red;
        }
    }
}
