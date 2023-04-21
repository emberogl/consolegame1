namespace Tetris
{
    internal class Tetrimino
    {
        public static Random Select = new();
        public int[,] Shape;
        public bool IsActive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        // Ved gemning og placering af tetrimino kræves det at konsol skal huske hvilken farve tetriminoen havde
        public static ConsoleColor Color { get; set; }
        public Tetrimino(int x, int y)
        {
            X = x;
            Y = y;
            Shape = GetShape();
        }
        private int[,] GetShape()
        {
            int[,] ShapeI = new int[,] { { 1, 1, 1, 1 } };
            int[,] ShapeJ = new int[,] { { 1, 0, 0 }, { 1, 1, 1 } };
            int[,] ShapeL = new int[,] { { 0, 0, 1 }, { 1, 1, 1 } };
            int[,] ShapeO = new int[,] { { 1, 1 }, { 1, 1 } };
            int[,] ShapeS = new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
            int[,] ShapeT = new int[,] { { 0, 1, 0 }, { 1, 1, 1 } };
            int[,] ShapeZ = new int[,] { { 1, 1, 0 }, { 0, 1, 1 } };
            int[][,] Shapes = { ShapeI, ShapeJ, ShapeL, ShapeO, ShapeS, ShapeT, ShapeZ };
            var SelectedShape = Shapes[Select.Next(0, 6)];
            if (SelectedShape == ShapeI)
            {
                 Color = ConsoleColor.Cyan;
            }
            else if (SelectedShape == ShapeJ)
            {
                Color = ConsoleColor.Blue;
            }
            else if (SelectedShape == ShapeL)
            {
                Color = ConsoleColor.Magenta;
            }
            else if (SelectedShape == ShapeO)
            {
                Color = ConsoleColor.Yellow;
            }
            else if (SelectedShape == ShapeS)
            {
                Color = ConsoleColor.Green;
            }
            else if (SelectedShape == ShapeT)
            {
                Color = ConsoleColor.DarkMagenta;
            }
            else if (SelectedShape == ShapeZ)
            {
                Color = ConsoleColor.Red;
            }
            return SelectedShape;
        }
    }
}