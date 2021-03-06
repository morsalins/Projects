﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuList menu = new MenuList();
            
            adjustConsole();
            menu.showMenu();
            menu.selectMenu();
            
            return;
        }

        /// <summary>
        /// adjustConsole() function will adjust and manage the console screen
        /// such as foreground background color, window size, cursor visibility 
        /// </summary>
        private static void adjustConsole()
        {
            Console.Title = "Tetris Game";
            ChangeConsoleColors.to_Console_Default_BackgroundColor();
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
            Console.SetWindowSize(80, 45);
            Console.CursorVisible = false;
        }
    }
}
