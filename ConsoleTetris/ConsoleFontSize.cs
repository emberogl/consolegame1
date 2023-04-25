using System.Runtime.InteropServices;

namespace Tetris
{
    internal class ConsoleFontSize
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct CONSOLE_FONT_INFOEX
        {
            public uint cbSize;
            public uint nFont;
            public COORD dwFontSize;
            public int FontFamily;
            public int FontWeight;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string FaceName;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

        private const int STD_OUTPUT_HANDLE = -11;

        public static void SetConsoleFontSize(short fontSize)
        {
            IntPtr hConsole = GetStdHandle(STD_OUTPUT_HANDLE);

            CONSOLE_FONT_INFOEX fontInfo = new CONSOLE_FONT_INFOEX();
            fontInfo.cbSize = (uint)Marshal.SizeOf(fontInfo);
            fontInfo.nFont = 0;
            fontInfo.dwFontSize.X = fontSize;
            fontInfo.dwFontSize.Y = fontSize;
            fontInfo.FontFamily = 54;
            fontInfo.FontWeight = 400;
            fontInfo.FaceName = "Consolas";

            SetCurrentConsoleFontEx(hConsole, false, ref fontInfo);
        }
    }
}
