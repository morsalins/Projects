using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class Play
    {
        TetrisShapes currentShape;
        ConsoleKeyInfo pressedKey;
        Random randomShape = new Random();
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
        
        private int startingCol = (Bucket.leftEnd + Bucket.rightEnd) / 2;
        private int startingRow = Bucket.topEnd + 1;
        private int fallingTimeInterval;
        public int score;

        public bool fallingShape;
        public bool gameOver;
        public bool userQuit;

        public Play()
        {
            fallingTimeInterval = 1000;
            score = 0;
        }

        public void startGame()
        {
            Bucket.buildBucket();
            buildScorline();
            ChangeConsoleColors.to_Bucket_BackgroundColor();
            ChangeConsoleColors.to_Bucket_ForegroundColor();
            //currentShape = new BoxShape(startingCol, startingRow);
            //currentShape.PrintShape();
            //Console.ReadKey();
            playGame();
            ChangeConsoleColors.to_Console_Default_BackgroundColor();
            ChangeConsoleColors.to_Console_Default_ForegroundColor();
        }

        private void playGame()
        {
            gameOver = false;
            userQuit = false;
            //TetrisShapes nextShape = generateShape(randomShape.Next(7));

            while (!gameOver && !userQuit)
            {
                fallingShape = true;
                //currentShape = generateShape(randomShape.Next(7));
                currentShape = new TShape(startingCol, startingRow);
                //int selected = randomShape.Next(7);
                //nextShape = generateShape(selected);
                currentShape.PrintShape(currentShape.getShape(currentShape.currentRotation));
                stopwatch.Start();

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
                    
                    if (stopwatch.ElapsedMilliseconds >= fallingTimeInterval)
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
            }
        }
        
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

        public void buildScorline()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write("Score: {0}/10", score);
        }
        
        public void updateScore()
        {
            score++;
        }

        public void showScore()
        {
            Console.SetCursorPosition(7, 0);
            Console.Write(score + "/10");
        }

        private void PutTaskDelay()
        {
            var t = Task.Run(async delegate
            {
                await Task.Delay(1000);
                return 42;
            });
            
            t.Wait();
        }
    }
}
