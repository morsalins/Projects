using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    /// <summary>
    /// An abstract class to manage and control all the 7 shapes.
    /// All 7 shapes inheret this class.
    /// </summary>
    public abstract class TetrisShapes
    {
        /// <summary>
        /// centerX is the position of center part from left side of console window. 
        /// </summary>
        public int centerX;

        /// <summary>
        /// centerY is the position of center part from top side of console window.
        /// </summary>
        public int centerY;

        /// <summary>
        /// left boundary of shape.
        /// </summary>
        public int left;

        /// <summary>
        /// right boundary of shape.
        /// </summary>
        public int right;

        /// <summary>
        /// top boundary of shape.
        /// </summary>
        public int top;

        /// <summary>
        /// bottom boundary of shape.
        /// </summary>
        public int bottom;

        /// <summary>
        /// Tracking the current rotation state of current shape.
        /// </summary>
        public int currentRotation;

        /// <summary>
        /// shapchar is char which will be used as a shape cell to make the shape.
        /// </summary>
        public const char shapeChar = 'O';
        public const char shapeSpace = ' ';
        
        /// <summary>
        /// A number between (1 -3) to indicate how much step a shape will move down.
        /// </summary>
        protected int downsteps;

        /// <summary>
        /// A 3D array to contain all the rotation format of a shape.
        /// A shape is a 2d object and it has different rotations.
        /// Every row of allRotations will contain some 2D array which are actually 2D shapes
        /// of different rotation format.
        /// </summary>
        protected char[][][] allRotations;
        
        /// <summary>
        /// A 2d Array which contains the boundary (left, right, top, bottom) direction
        /// with respect to centerX, centerY.
        /// Each row of allDirection indicates the rotation number of each shape and
        /// that row contains the boundary direction of that rotation of a shape. 
        /// </summary>
        protected int[,] allDirection;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        public TetrisShapes()
        {
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

        /// <summary>
        /// canRotate() is function which decides whether a shape
        /// can be rotate on its current position or not.
        /// It is an abstract function so every derived class 
        /// implement this function by their own way.
        /// See each derived class implementation for details.
        /// 
        /// return: True if can rotate. Otherwise False.
        /// </summary>
        public abstract bool canRotate();
        
        /// <summary>
        /// canMoveLeft() is function which decides whether a shape
        /// can move Left from its current position or not.
        /// It is an abstract function so every derived class 
        /// implement this function by their own way.
        /// See each derived class implementation for details.
        /// 
        /// return: True if can Move Left. Otherwise False.
        /// </summary>
        public abstract bool canMoveLeft();
        
        /// <summary>
        /// canMoveRight() is function which decides whether a shape
        /// can move right from its current position or not.
        /// It is an abstract function so every derived class 
        /// implement this function by their own way.
        /// See each derived class implementation for details.
        /// 
        /// return: True if can move right. Otherwise False.
        /// </summary>
        public abstract bool canMoveRight();

        /// <summary>
        /// canMoveDown() is function which decides whether a shape
        /// can move down from its current position or not.
        /// It is an abstract function so every derived class 
        /// implement this function by their own way.
        /// See each derived class implementation for details.
        /// 
        /// parameters:
        ///     int steps: A number between(1 - 3) which denotes how much step a shape can move Down.
        /// 
        /// return: True if can move down. Otherwise False.
        /// </summary>
        public abstract bool canMoveDown(int steps);


        /// <summary>
        /// if Rotate possible then rotate the shape.
        /// Remove/clear the current shape from console screen.
        /// change the currentRotation state.
        /// update boundary line.
        /// Print new rotation of the shape in console screen.
        /// </summary>
        public void RotateShape()
        {
            removeShape(allRotations[currentRotation], left, top);
            currentRotation = (currentRotation + 1) % allRotations.Length;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            top = centerY + allDirection[currentRotation, 2]; bottom = centerY + allDirection[currentRotation, 3];
            PrintShape(allRotations[currentRotation], left, top);      
        }

        /// <summary>
        /// if move left possible then move left the shape.
        /// Remove/clear the current shape from console screen.
        /// update the centerX.
        /// update boundary line.
        /// Print the shape in new position in console screen.
        /// </summary>
        public void moveLeft()
        {
            removeShape(allRotations[currentRotation], left, top);
            centerX--;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            PrintShape(allRotations[currentRotation], left, top);
        }

        /// <summary>
        /// if move right possible then move right the shape.
        /// Remove/clear the current shape from console screen.
        /// update the centerX.
        /// update boundary line.
        /// Print the shape in new position in console screen.
        /// </summary>
        public void moveRight()
        {
            removeShape(allRotations[currentRotation], left, top);
            centerX++;
            left = centerX + allDirection[currentRotation, 0]; right = centerX + allDirection[currentRotation, 1];
            PrintShape(allRotations[currentRotation], left, top);
        }

        /// <summary>
        /// if move down possible then move down the shape.
        /// Remove/clear the current shape from console screen.
        /// update the centerY.
        /// update boundary line.
        /// Print the shape in new position in console screen.
        /// </summary>
        public void moveDown()
        {
            removeShape(allRotations[currentRotation], left, top);
            centerY += downsteps;
            top = centerY + allDirection[currentRotation, 2]; bottom = centerY + allDirection[currentRotation, 3];
            PrintShape(allRotations[currentRotation], left, top);
        }

        /// <summary>
        /// Get a specific shape.
        /// 
        /// parameters:
        ///     int x: A number which denotes the rotation number of a shape.
        ///     
        /// return: A 2D shape of that rotation.
        /// </summary>
        public char[][] getShape(int x)
        {
            return allRotations[x];
        }

        /// <summary>
        /// Remove/Clear a shape from console screen.
        /// 
        /// parameters:
        ///     char[][] arr: A 2D shape that needs to be removed.
        ///     int l: Left position of console window from where removing process start.
        ///     int t: Top position of console window from where removing process start.
        /// </summary>
        public void removeShape(char[][] arr, int l, int t)
        {
            Console.SetCursorPosition(l, t);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != shapeSpace)
                        Console.Write(" ");
                    else Console.CursorLeft++;
                }
                Console.SetCursorPosition(l, Console.CursorTop + 1);
            }
        }

        /// <summary>
        /// Print/Display a shape in console screen.
        /// 
        /// parameters:
        ///     char[][] arr: A 2D shape which will be printed.
        ///     int l: Left position of console window from where printing process start.
        ///     int t: Top position of console window from where printing process start.
        /// </summary>
        public void PrintShape(char[][] arr, int l, int t)
        {
            Console.SetCursorPosition(l, t);
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] != shapeSpace)
                        Console.Write(arr[i][j]);
                    else Console.CursorLeft++;
                }
                Console.SetCursorPosition(l, Console.CursorTop + 1);
            }
        }
    }
}
