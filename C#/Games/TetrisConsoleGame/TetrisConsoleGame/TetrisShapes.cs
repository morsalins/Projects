using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    public abstract class TetrisShapes
    {
        public int centerX;
        public int centerY;
        public int left;
        public int right;
        public int top;
        public int bottom;
        public int currentRotation;

        public const char shapeChar = '#';
        public const char shapeSpace = ' ';
        protected int downsteps;

        protected char[][][] allRotations;
        protected int[,] allDirection;

        public TetrisShapes()
        {
            // Default Constructor
        }

        public TetrisShapes(int x, int y, int l, int r, int t, int b)
        {
            this.centerX = x;
            this.centerY = y;
            this.left = l;
            this.right = r;
            this.top = t;
            this.bottom = b;
            this.currentRotation = 0;
        }

        public abstract bool canRotate();
        public abstract bool canMoveLeft();
        public abstract bool canMoveRight();
        public abstract bool canMoveDown(int steps);

        public void RotateShape()
        {
            removeShape(allRotations[currentRotation]);
            currentRotation = (currentRotation + 1) % allRotations.Length;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            top = centerY + allDirection[currentRotation, 2]; bottom = centerY + allDirection[currentRotation, 3];
            PrintShape(allRotations[currentRotation]);      
        }

        public void moveLeft()
        {
            removeShape(allRotations[currentRotation]);
            centerX--;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            PrintShape(allRotations[currentRotation]);
        }

        public void moveRight()
        {
            removeShape(allRotations[currentRotation]);
            centerX++;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            PrintShape(allRotations[currentRotation]);
        }

        public void moveDown()
        {
            removeShape(allRotations[currentRotation]);
            centerY += downsteps;
            top = centerY + allDirection[currentRotation, 2]; bottom = centerY + allDirection[currentRotation, 3];
            PrintShape(allRotations[currentRotation]);
        }

        public char[][] getShape(int x)
        {
            return allRotations[x];
        }

        public void removeShape(char[][] arr)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != shapeSpace)
                        Console.Write(" ");
                    else Console.CursorLeft++;
                }
                Console.SetCursorPosition(left, Console.CursorTop + 1);
            }
        }

        public void PrintShape(char[][] arr)
        {
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != shapeSpace)
                        Console.Write(arr[i][j]);
                    else Console.CursorLeft++;
                }
                Console.SetCursorPosition(left, Console.CursorTop + 1);
            }
        }
    }
}
