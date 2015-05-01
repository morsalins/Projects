using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisConsoleGame
{
    class MenuList
    {
        private Play play;
        private ConsoleKeyInfo input;
        private bool gameRunning = true;

        private string[] menuNames = { "1.Play", "2.Highscores", "3.Help", "4.Exit"};

        public MenuList()
        {
            // Deault Constructor.
        }
        
        public void showMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(30, 0); Console.WriteLine("TETRIS GAME");
            Console.SetCursorPosition(30, 2); Console.WriteLine(menuNames[0]);
            Console.SetCursorPosition(30, 3); Console.WriteLine(menuNames[1]);
            Console.SetCursorPosition(30, 4); Console.WriteLine(menuNames[2]);
            Console.SetCursorPosition(30, 5); Console.WriteLine(menuNames[3]);
            
            Console.SetCursorPosition(20, 8); Console.WriteLine("Press Arrow Keys: Navigate Options.");
            Console.SetCursorPosition(20, 9); Console.WriteLine("Press Enter: Select Menu.");

            showNavigator(27, 2);
            ChangeConsoleColors.change_LineColor(30, 2, menuNames[0], ChangeConsoleColors.navigatorForeground);
        }

        public void selectMenu()
        {
            do
            {
                input = Console.ReadKey();
                
                if (input.Key == ConsoleKey.DownArrow || input.Key == ConsoleKey.RightArrow)
                {
                    moveNavigator("Down");
                }
                else if (input.Key == ConsoleKey.UpArrow || input.Key == ConsoleKey.LeftArrow)
                {
                    moveNavigator("Up");
                }
                else if (input.Key == ConsoleKey.Enter)
                {
                    if (Console.CursorTop == 2)
                    {
                        play = new Play();
                        play.startGame();
                        showMenu();
                        showNavigator(27, 2);
                    }
                    else if (Console.CursorTop == 3)
                    {

                    }
                    else if (Console.CursorTop == 4)
                    {

                    }
                    else if (Console.CursorTop == 5)
                    {
                        gameExitQuery();
                    }
                }
                else
                {
                    invalidInputMessage(30, 10);
                    Thread.Sleep(1000);
                    showMenu();
                    showNavigator(27, 2);
                }
            
            } while (gameRunning);
        }

        private void gameExitQuery()
        {
            Console.Clear();
            Console.WriteLine("Do you really want to Exit (y/any key)");
            input = Console.ReadKey();
            if (input.Key == ConsoleKey.Y)
            {
                gameRunning = false;
            }
            else
            {
                showMenu();
                showNavigator(27, 2);
            }
        }

        private void moveNavigator(string direction)
        {
            clearNavigator(27, Console.CursorTop);
            ChangeConsoleColors.change_LineColor(30, Console.CursorTop, menuNames[Console.CursorTop - 2], ChangeConsoleColors.defaultForeground);

            if (direction == "Down")
            {
                Console.CursorTop++;
                if (Console.CursorTop > 5) Console.CursorTop = 2;
            }
            else if (direction == "Up")
            {
                Console.CursorTop--;
                if (Console.CursorTop < 2) Console.CursorTop = 5;
            }

            ChangeConsoleColors.change_LineColor(30, Console.CursorTop, menuNames[Console.CursorTop - 2], ChangeConsoleColors.navigatorForeground);
            showNavigator(27, Console.CursorTop);
        }

        private void clearNavigator(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("  ");
        }

        private void showNavigator(int left, int top)
        {
            ChangeConsoleColors.to_Navigator_ForegroundColor();
            Console.SetCursorPosition(left, top);
            Console.Write("->");
            Console.SetCursorPosition(left-1, top);
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        private void invalidInputMessage(int left, int top)
        {
            Console.Clear();
            Console.SetCursorPosition(left, top);
            Console.WriteLine("{0} is not a valid input!!!", input.KeyChar);
        }
    }
}
