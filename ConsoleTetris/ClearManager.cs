namespace Tetris
{
    internal class ClearManager
    {
        public static void Clear()
        {
            List<string> Buffer = new();
            for (int row = Game.Board!.GetLength(0) - 2; row >= 0; row--)
            {
                for (int col = 0; col < Game.Board!.GetLength(1); col++)
                {
                    Buffer.Add(Game.Board![row, col]);
                }
                if (Buffer.Distinct().Count() == 1 && Buffer[0] == Game.TetriminoASCII)
                {
                    Game.Score += 100;
                    Game.Lines += 1;
                    Game.UpdateScoreDisplay();
                    Game.Print(Game.Board!, printscore: true);
                    for (int i = 0; i < Game.DisplayCol; i++)
                    {
                        Game.Board[row, i] = Game.BoardASCII;
                    }
                }
                Buffer.Clear();
            }
        }
    }
}