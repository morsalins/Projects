using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    public static class ChangeConsoleColors
    {
        public static ConsoleColor defaultBackground = ConsoleColor.Black;
        public static ConsoleColor defaultForeground = ConsoleColor.White;
        public static ConsoleColor navigatorForeground = ConsoleColor.Cyan;
        public static ConsoleColor bucketBackground = ConsoleColor.White;
        public static ConsoleColor bucketForeground = ConsoleColor.Black;

        public static void to_Console_Default_BackgroundColor()
        {
            Console.BackgroundColor = defaultBackground;
        }

        public static void to_Console_Default_ForegroundColor()
        {
            Console.ForegroundColor = defaultForeground;
        }

        public static void to_Navigator_ForegroundColor()
        {
            Console.ForegroundColor = navigatorForeground;
        }

        public static void to_Bucket_BackgroundColor()
        {
            Console.BackgroundColor = bucketBackground;
        }

        public static void to_Bucket_ForegroundColor()
        {
            Console.ForegroundColor = bucketForeground;
        }

        public static void change_LineColor(int left, int top, string line, ConsoleColor lineColor)
        {
            Console.ForegroundColor = lineColor;
            Console.SetCursorPosition(left, top);
            Console.Write(line);
            to_Console_Default_ForegroundColor();
        }
    }
}
