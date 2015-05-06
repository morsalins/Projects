using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    /// <summary>
    /// A class that will manage and control entire Gameplay option and feature
    /// when the user play the game.
    /// </summary>
    class Play
    {
        /// <summary>
        /// currentShape will refer the shape that is currently on screen.
        /// </summary>
        TetrisShapes currentShape;
        
        /// <summary>
        /// pressedKey contain the user input.
        /// </summary>
        ConsoleKeyInfo pressedKey;
        
        /// <summary>
        /// randomShape a variable to generate a shape randomly.
        /// </summary>
        Random randomShape = new Random();
        
        /// <summary>
        /// stopwatch to measure the falling time interval.
        /// </summary>
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        /// <summary>
        /// startingCol & startingRow store the position from where the shape start falling.
        /// </summary>
        private int startingCol;
        private int startingRow;
        
        /// <summary>
        /// fallingTimeInterval store the time inteval of shape moving down.
        /// </summary>
        private int fallingTimeInterval;

        /// <summary>
        /// These four sidebar variable will manage the boundary line of sidebar info panel
        /// where level, score and next shape info are shown.
        /// </summary>
        private int sidebarLeft;
        private int sidebarRight;
        private int sidebarTop;
        private int sidebarBottom;

        /// <summary>
        /// fallingShape: True if shape can fall down. Otherwise False.
        /// gameOver: True if game is over. Otherwise False.
        /// userQuit: True if user quits the game by pressing Escape. Otherwise False.
        /// </summary>
        public bool fallingShape;
        public bool gameOver;
        public bool userQuit;

        /// <summary>
        /// score: manage and store the scoring. Increasing of score will increase the Level.
        /// level: manage and store the level increasing. Increasing the level will decrease the fallingTimeInterval.
        /// </summary>
        public static int score;
        public static int level;

        public Play()
        {
            startingCol = (Bucket.leftEnd + Bucket.rightEnd) / 2;
            startingRow = Bucket.topEnd + 1;
            fallingTimeInterval = 1000;
            sidebarLeft = Bucket.rightEnd + 5;
            sidebarRight = sidebarLeft + 20;
            sidebarTop = Bucket.topEnd;
            sidebarBottom = sidebarTop + 14;
        }

        /// <summary>
        /// startGame() a function that will build bucket and sidebar, 
        /// change the console foreground background color by calling the respective function.
        /// Then let user play the game by calling play() function.
        /// After playing it changes the score, level and console color to their initial value.
        /// </summary>
        public void startGame()
        {
            Bucket.buildBucket();
            score = 0;
            level = 1;
            buildSidebar();
            
            ChangeConsoleColors.to_Bucket_BackgroundColor();
            ChangeConsoleColors.to_Bucket_ForegroundColor();

            playGame();
            
            ChangeConsoleColors.to_Console_Default_BackgroundColor();
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        /// <summary>
        /// playGame() this function will control the entire game playing cases.
        /// This function will generate shape, print it in console screen, move the shape according
        /// to user inputs, move down the shape by one step after every time interval, update the sidebar 
        /// info, check for game over and end the game if it is over.
        /// 
        /// The game will continue until game is not over and uset doesn't quit. (see Lines from 104 to 183).
        /// In this loop it will generate shape, display it in console if game is not over and start the stopwatch
        /// to measure the time interval of falling shape down and also...
        /// there is another loop in it which manage shape falling (see Lines from 130 to 181).
        /// This loop will continue until the shape can move down without facing any obstacle or reach the bottom boundary.
        /// while falling it will check for any available user's input (Console.KeyAvailable) in input stream and if find any
        /// then do the respective job by calling respective function. 
        /// Also it will check for time interval in stopwacth to move shape downwards or not.
        /// If stop watch time reach or pass the time inteval it will move the shape one step down by calling the movedown()
        /// function then reset and restart the stopwatch. 
        /// If a shape can't move down any more step it will exit the loop then update grid by calling updateGrid(),
        /// update sidebar by calling updateSidebar()
        /// 
        /// The first loop will exit when game over condition is reached or user quit it. 
        /// Then this Function end the game and return to it's Caller function.
        /// </summary>
        private void playGame()
        {
            gameOver = false;
            userQuit = false;
            TetrisShapes nextShape = generateShape(randomShape.Next(7));

            while (!gameOver && !userQuit)
            {
                fallingShape = true;
                currentShape = nextShape;
                currentShape.PrintShape(currentShape.getShape(0), currentShape.left, currentShape.top);
                nextShape = generateShape(randomShape.Next(7));

                if (!isGameOver(currentShape.getShape(0), currentShape.left, currentShape.top))
                {
                    ChangeConsoleColors.to_info_ForegroundColor();
                    ChangeConsoleColors.to_info_BackgroundColor();
                    nextShape.PrintShape(nextShape.getShape(0), ((sidebarLeft + sidebarRight) / 2) - 2, sidebarTop + 3);
                    ChangeConsoleColors.to_Bucket_ForegroundColor();
                    ChangeConsoleColors.to_Bucket_BackgroundColor();
                    stopwatch.Start();
                }
                else
                {
                    showGameOverMessage();
                    fallingShape = false;
                    gameOver = true;
                }

                while (fallingShape && !userQuit)
                {
                    if (Console.KeyAvailable)
                    {
                        pressedKey = Console.ReadKey(true);

                        if (pressedKey.Key == ConsoleKey.UpArrow && currentShape.canRotate())
                        {
                            currentShape.RotateShape();
                        }
                        else if (pressedKey.Key == ConsoleKey.LeftArrow && currentShape.canMoveLeft())
                        {
                            currentShape.moveLeft();
                        }
                        else if (pressedKey.Key == ConsoleKey.RightArrow && currentShape.canMoveRight())
                        {
                            currentShape.moveRight();
                        }
                        else if (pressedKey.Key == ConsoleKey.DownArrow)
                        {
                            if (currentShape.canMoveDown(3))
                            {
                                currentShape.moveDown();
                            }
                            else
                            {
                                fallingShape = false;
                                Bucket.updateGrid(currentShape.getShape(currentShape.currentRotation), currentShape.left, currentShape.top, TetrisShapes.shapeChar);
                            }
                        }
                        else if (pressedKey.Key == ConsoleKey.Escape)
                        {
                            fallingShape = false;
                            userQuit = true;
                        }
                    }
                    
                    if (stopwatch.ElapsedMilliseconds >= fallingTimeInterval - ((level - 1) * 100))
                    {
                        if (currentShape.canMoveDown(1))
                        {
                            currentShape.moveDown();
                        }
                        else
                        {
                            fallingShape = false;
                            Bucket.updateGrid(currentShape.getShape(currentShape.currentRotation), currentShape.left, currentShape.top, TetrisShapes.shapeChar);
                        }
                        stopwatch.Reset();
                        stopwatch.Start();
                    }
                }
                updateSidebar(nextShape);
            }
        }
        
        /// <summary>
        /// generateShape(int) a function which will create an object of any shape
        /// randomly and return it to it's caller function.
        /// 
        /// parameters:
        ///     int s: indicating which shape shoulde be created. s is a number between (0 - 6)
        ///     which is generated randomly.
        ///     
        /// return:
        ///     A TetrisShapes type object which is actually a shape from any of these 7 shapes. 
        /// </summary>
        private TetrisShapes generateShape(int s)
        {
            if (s == 0) return new BoxShape(startingCol, startingRow);
            else if (s == 1) return new LineShape(startingCol, startingRow);
            else if (s == 2) return new ZShape(startingCol, startingRow);
            else if (s == 3) return new SShape(startingCol, startingRow);
            else if (s == 4) return new LShape(startingCol, startingRow);
            else if (s == 5) return new JShape(startingCol, startingRow);
            else if (s == 6) return new TShape(startingCol, startingRow);
            return null;
        }

        /// <summary>
        /// buildSidebar() building the sidbar which will show info about
        /// next shape, score, level.
        /// </summary>
        private void buildSidebar()
        {
            ChangeConsoleColors.to_info_ForegroundColor();
            ChangeConsoleColors.to_info_BackgroundColor();

            Console.SetCursorPosition(sidebarLeft, sidebarTop);
            for (int i = sidebarLeft; i <= sidebarRight; i++)
            {
                Console.Write('_');
            }

            for (int i = sidebarTop + 1; i <= sidebarBottom; i++)
            {
                Console.SetCursorPosition(sidebarLeft-1, i);
                Console.Write('|');
            }

            for (int i = sidebarTop + 1; i <= sidebarBottom; i++)
            {
                Console.SetCursorPosition(sidebarRight+1, i);
                Console.Write('|');
            }

            Console.SetCursorPosition(sidebarLeft, sidebarBottom);
            for (int i = sidebarLeft; i <= sidebarRight; i++)
            {
                Console.Write('_');
            }

            Console.SetCursorPosition( ((sidebarLeft + sidebarRight) / 2) - 2, sidebarTop + 1);
            Console.Write("Next");
            
            Console.SetCursorPosition(sidebarLeft + 2, sidebarTop + 9);
            Console.Write("Score: {0}", score);
            
            Console.SetCursorPosition(sidebarLeft + 2, sidebarTop + 11);
            Console.Write("Level: {0}", level);

            Console.SetCursorPosition(((sidebarLeft + sidebarRight) / 2) - 8, sidebarBottom - 1);
            Console.Write("Press Esc to Quit.");

            ChangeConsoleColors.to_Bucket_ForegroundColor();
            ChangeConsoleColors.to_Bucket_BackgroundColor();
        }

        /// <summary>
        /// updateSidebar(TetrisShapes) update the sidebar info (next shape, score, level)
        /// after a shape completely fall down.
        /// 
        /// parameters:
        ///     TetrisShapes shape: indicating the shape which need to be removed from sidebar info panel.
        /// </summary>
        private void updateSidebar(TetrisShapes shape)
        {
            ChangeConsoleColors.to_info_ForegroundColor();
            ChangeConsoleColors.to_info_BackgroundColor();
            
            shape.removeShape(shape.getShape(0), ((sidebarLeft + sidebarRight) / 2) - 2, sidebarTop + 3);

            Console.SetCursorPosition(sidebarLeft, sidebarTop + 9);
            Console.Write(new string(' ', sidebarRight - sidebarLeft + 1));
            
            Console.SetCursorPosition(sidebarLeft, sidebarTop + 11);
            Console.Write(new string(' ', sidebarRight - sidebarLeft + 1));

            Console.SetCursorPosition(sidebarLeft + 2, sidebarTop + 9);
            Console.Write("Score: {0}", score);

            Console.SetCursorPosition(sidebarLeft + 2, sidebarTop + 11);
            Console.Write("Level: {0}", level);

            ChangeConsoleColors.to_Bucket_ForegroundColor();
            ChangeConsoleColors.to_Bucket_BackgroundColor();
        }

        /// <summary>
        /// Just updating the score and level according the game rules.
        /// Static because these function needs to call from other class without creating any object.
        /// </summary>
        public static void updateScore()
        {
            score++;
            if (score % 5 == 0) level++;
        }

        /// <summary>
        /// Just Checking for whether the game is over or not.
        /// 
        /// parameters:
        ///     char[][] arr: A 2D shape for which the game over condition is checking.
        ///     int left: The left position of bucket grid from where checking will start.
        ///     int top:  The top position of bucket grid from where checking will start.
        ///     
        /// return: True if game is Over. Otherwise False.
        /// </summary>
        private bool isGameOver(char[][] arr, int left, int top)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == TetrisShapes.shapeChar && Bucket.Grid[(top + i) - Bucket.baseRow, (left + j) - Bucket.baseCol])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Will show/display the score and a game over message when game is over. 
        /// </summary>
        private void showGameOverMessage()
        {
            ChangeConsoleColors.to_info_BackgroundColor();
            ChangeConsoleColors.to_info_ForegroundColor();

            Thread.Sleep(500);
            Console.Clear();
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 4, Console.WindowTop + 5);
            Console.WriteLine("Game Over!");
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 6, Console.WindowTop + 7);
            Console.Write("Your Score: {0}", score);
            
            Console.SetCursorPosition((Console.WindowWidth / 2) - 12, Console.WindowTop + 9);
            Console.Write("Press Esc key to get back....");

            ChangeConsoleColors.to_Bucket_BackgroundColor();
            ChangeConsoleColors.to_Bucket_ForegroundColor();

            while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
