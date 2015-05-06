using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TetrisConsoleGame
{
    /// <summary>
    /// A class to control and manage every main menu of the program.
    /// </summary>
    class MenuList
    {
        private Play play;
        private ConsoleKeyInfo input;
        private bool gameRunning = true;

        private string[] menuNames = { "1.Play", "2.Credits", "3.Help", "4.Exit"};

        /// <summary>
        /// // Deault Constructor.
        /// </summary>
        public MenuList()
        {
        }
        
        /// <summary>
        /// showMenu() a function to print/display the main menu in Console screen. 
        /// </summary>
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

        /// <summary>
        /// selectMenu() will select the menu and do the specified job of that menu
        /// by calling the specified function according to the user's input.
        /// </summary>
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
                    }
                    else if (Console.CursorTop == 3)
                    {
                        showCredits();
                        showMenu();
                    }
                    else if (Console.CursorTop == 4)
                    {
                        showHelp();
                        showMenu();
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
                }
            
            } while (gameRunning);
        }

        /// <summary>
        /// gameExitQuery() a function which ask user if he/she really wants to quit after selecting the Exit menu.
        /// if user press 'y' this function will make 'gameRunning' variable to 'false'. Which will end the execution
        /// of this program. Otherwise it will do nothing and return to caller function if any other key pressed.
        /// </summary>
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
            }
        }

        /// <summary>
        /// moveNavigator(string) a function to manage the movement of the navigator('->') through menu.
        /// 
        /// parameters:
        ///     direction: Indicating the direction (Up or Down) where the navigator will move.
        /// </summary>
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

        /// <summary>
        /// clearNavigator(int, int) a function to clear/remove the navigator from current position.
        /// 
        /// parameters:
        ///     left: indicating the left posotion of the console window.
        ///     top: indicating the top posotion of the console window.
        /// </summary>
        private void clearNavigator(int left, int top)
        {
            Console.SetCursorPosition(left, top);
            Console.Write("  ");
        }


        /// <summary>
        /// showNavigator(int, int) a function to show/display the navigator in console screen.
        /// 
        /// parameters:
        ///     left: indicating the left posotion of the console window.
        ///     top: indicating the top posotion of the console window.
        /// </summary>
        private void showNavigator(int left, int top)
        {
            ChangeConsoleColors.to_Navigator_ForegroundColor();
            Console.SetCursorPosition(left, top);
            Console.Write("->");
            Console.SetCursorPosition(left-1, top);
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        private void showCredits()
        {
            Console.Clear();

            ChangeConsoleColors.to_info_BackgroundColor();
            ChangeConsoleColors.to_info_ForegroundColor();

            Console.SetCursorPosition(10, 5);
            Console.Write("Mohaimin Morsalin");

            Console.SetCursorPosition(10, Console.CursorTop + 1);
            Console.Write("United International University (CSE 121)");

            Console.SetCursorPosition(10, Console.CursorTop + 1);
            Console.Write("Dhaka, Bangladesh.");

            Console.SetCursorPosition(10, Console.CursorTop + 1);
            Console.Write("Email: mim120291@gmail.com");

            Console.SetCursorPosition((Console.WindowWidth / 2) - 12, Console.CursorTop + 2);
            Console.Write("press any key to get back ......");
            Console.ReadKey();

            ChangeConsoleColors.to_Console_Default_BackgroundColor();
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        private void showHelp()
        {
            Console.Clear();

            ChangeConsoleColors.to_info_ForegroundColor();
            ChangeConsoleColors.to_info_BackgroundColor();

            Console.SetCursorPosition(0, 5);
            Console.Write("\tControls:\n");
            Console.Write("\tUp: Rotate\n");
            Console.Write("\tLeft: Move Left\n");
            Console.Write("\tRight: Move Right\n");
            Console.Write("\tDown: Move Down by three lines\n\n");

            Console.Write("\tRules:\n");
            Console.Write("\tJust try to fill up every row.\n"
                        + "\tPoints are earned one by one by filling up every row.\n"
                        + "\tLevel increased by earning every 5 Points.\n"
                        + "\tThe falling Down speed of every shape will be increased\n"
                        + "\twith the increment of Level\n");

            Console.SetCursorPosition((Console.WindowWidth / 2) - 12, Console.CursorTop + 2);
            Console.Write("press any key to get back ......");
            Console.ReadKey();

            ChangeConsoleColors.to_Console_Default_BackgroundColor();
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        /// <summary>
        /// invalidInputMessage(int, int) a function to show/display the warning after invalid input in console screen.
        /// 
        /// parameters:
        ///     left: indicating the left posotion of the console window.
        ///     top: indicating the top posotion of the console window.
        /// </summary>
        private void invalidInputMessage(int left, int top)
        {
            Console.Clear();
            ChangeConsoleColors.to_error_ForegroundColor();
            Console.SetCursorPosition(left, top);
            Console.WriteLine("{0} is not a valid input!!!", input.KeyChar);
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }
    }
}
