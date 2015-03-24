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

        public MenuList()
        {
            // Deault Constructor.
        }
        
        public void showMenu()
        {
            Console.Clear();
            Console.SetCursorPosition(30, 0);
            Console.WriteLine("TETRIS GAME");
            Console.SetCursorPosition(30, 2);
            Console.WriteLine("1.Play");
            Console.SetCursorPosition(30, 3);
            Console.WriteLine("2.Highscores");
            Console.SetCursorPosition(30, 4);
            Console.WriteLine("3.Help");
            Console.SetCursorPosition(30, 5);
            Console.WriteLine("4.Exit");
        }

        public void selectMenu()
        {
            Console.CursorVisible = false;
            showNavigator(27, 2);

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
                        play.playGame();
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
            
            showNavigator(27, Console.CursorTop);
        }

        private void clearNavigator(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("  ");
        }

        private void showNavigator(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("->");
            Console.SetCursorPosition(left-1, top);
        }

        private void invalidInputMessage(int left, int top)
        {
            Console.Clear();
            Console.SetCursorPosition(left, top);
            Console.WriteLine("{0} is not a valid input!!!", input.KeyChar);
        }
    }
}
