using System;
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
            Console.ReadKey();
            return;
        }

        private static void adjustConsole()
        {
            Console.Title = "Tetris Game";
            Console.SetWindowSize(80, 45);
        }
    }
}
