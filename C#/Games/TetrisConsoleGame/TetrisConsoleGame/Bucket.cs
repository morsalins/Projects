using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    /// <summary>
    /// A calss to manage and control the game play area/field.
    /// Here we are calling it Bucket.
    /// </summary>
    public static class Bucket
    {
        /// <summary>
        /// These four Fields are the boundary line of the Bucket.
        /// </summary>
        public static int leftEnd = 10;
        public static int rightEnd = 34;
        public static int topEnd = 5;
        public static int bottomEnd = 34;

        /// <summary>
        /// The characters which is used to build the vertical and horizontal wall. 
        /// </summary>
        private const char horizontalWall = '-';
        private const char verticalWall = '|';

        /// <summary>
        /// totalChar is the number denoting the maximum character a row can contain 
        /// and after which the score is updated.
        /// </summary>
        private static int totalChar = rightEnd - leftEnd + 1;

        /// <summary>
        /// totalCharAtGridRow an array that conatin and updating the number of 
        /// total characters in every row of Grid. 
        /// </summary>
        private static int[] totalCharAtGridRow = new int[bottomEnd - topEnd + 1]; 
        
        /// <summary>
        /// Grid a 2d array to tracking any specific position of bucket 
        /// whether it is empty or not. False if a position is empty. Otherwise True.
        /// </summary>
        public static bool[,] Grid = new bool[bottomEnd - topEnd + 1, rightEnd - leftEnd + 1];
        
        /// <summary>
        /// gridChar a 2d array which stores the entire characters diplaying in bucket at a time.  
        /// </summary>
        private static char[,] gridChar = new char[bottomEnd - topEnd + 1, rightEnd - leftEnd + 1];
        
        public static int baseRow = topEnd + 1;
        public static int baseCol = leftEnd;

        /// <summary>
        /// Just building the bucket.
        /// </summary>
        public static void buildBucket()
        {
            Console.Clear();

            Console.SetCursorPosition(leftEnd, topEnd);
            for (int i = 1; i <= rightEnd - leftEnd + 1; i++)
            {
                Console.Write(horizontalWall);
            }
            // Build the top Boundary

            Console.SetCursorPosition(leftEnd, bottomEnd);
            for (int i = 1; i <= rightEnd - leftEnd + 1; i++)
            {
                Console.Write(horizontalWall);
            }
            // Build the Botom Boundary

            Console.SetCursorPosition(leftEnd, topEnd);
            for (int i = 1; i < bottomEnd - topEnd; i++)
            {
                Console.SetCursorPosition(leftEnd-1, topEnd + i);
                Console.Write(verticalWall);
            }
            // Build the left boundary

            Console.SetCursorPosition(rightEnd, topEnd);
            for (int i = 1; i < bottomEnd - topEnd; i++)
            {
                Console.SetCursorPosition(rightEnd+1, topEnd + i);
                Console.Write(verticalWall);
            }
            // Build the right boundary

            colorBucketArea();
            initializeGrid();
        }

        /// <summary>
        /// Just coloring the bucket Area.
        /// </summary>
        private static void colorBucketArea()
        {
            ChangeConsoleColors.to_Bucket_BackgroundColor();

            for (int i = 1; i < bottomEnd - topEnd; i++)
            {
                Console.SetCursorPosition(leftEnd, topEnd+i);

                for (int j = 1; j <= rightEnd - leftEnd + 1; j++)
                {
                    Console.Write(" ");
                }
            }

            ChangeConsoleColors.to_Console_Default_BackgroundColor();
        }

        /// <summary>
        /// Set the array fields to its initial value.
        /// </summary>
        private static void initializeGrid()
        {
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    Grid[i, j] = false;
                }
            }

            for (int i = 0; i < gridChar.GetLength(0); i++)
            {
                for (int j = 0; j < gridChar.GetLength(1); j++)
                {
                    gridChar[i, j] = ' ';
                }
            }

            for (int i = 0; i < totalCharAtGridRow.GetLength(0); i++)
            {
                totalCharAtGridRow[i] = 0;
            }
        }

        /// <summary>
        /// updating the grid after a shape completely fall down.
        /// Also updating the gridChar and totalCharAtGridRow fields.
        /// when any row of grid is fill up it will update score, 
        /// set 0 to totalCharAtGridRow[that row] and upadate bucket area. 
        /// 
        /// parameters:
        ///     char[][] arr: denoting the 2D shape for which the grid will be updated.
        ///     int left: left position of console window from updating will start.
        ///     int top: top position of console window from updating will start.
        ///     char ch: Denoting a character for which the grid will be updated.
        /// </summary>
        public static void updateGrid(char[][] arr, int left, int top, char ch)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == ch)
                    {
                        Grid[(top + i) - baseRow, (left + j) - baseCol] = true;
                        gridChar[(top + i) - baseRow, (left + j) - baseCol] = ch;
                        totalCharAtGridRow[(top + i) - baseRow]++;
                    }
                }
                if ((top + i) - baseRow < totalCharAtGridRow.GetLength(0) && totalCharAtGridRow[(top + i) - baseRow] == totalChar)
                {
                    Play.updateScore();
                    totalCharAtGridRow[(top + i) - baseRow] = 0;
                    updateBucketArea(top + i);
                }
            }
        }

        /// <summary>
        /// When any row of grid is fill up it will upadate bucket area. 
        /// Remove the line o that row.
        /// Then swap every upper row line to it's next row.
        /// 
        /// parameters:
        ///     int row: The Top position of console window. Also indicating the row from where updating will start.
        /// </summary>
        private static void updateBucketArea(int row)
        {
            for (int i = row; i > topEnd + 1; i--)
            {
                for (int j = 0; j < gridChar.GetLength(1); j++)
                {
                    gridChar[i - baseRow, j] = ' ';
                }
               
                Console.SetCursorPosition(leftEnd, i);
                
                for (int j = leftEnd; j <= rightEnd; j++)
                {
                    gridChar[i - baseRow, j - baseCol] = gridChar[(i - 1) - baseRow, j - baseCol];
                    Grid[i - baseRow, j - baseCol] = Grid[(i - 1) - baseRow, j - baseCol];
                    Console.Write(gridChar[i - baseRow, j - baseCol]);
                }
                totalCharAtGridRow[i - baseRow] = totalCharAtGridRow[(i - 1) - baseRow];
            }
            
            Console.SetCursorPosition(leftEnd, topEnd + 1);
            Console.Write(new string(' ', rightEnd - leftEnd + 1));
            
            for (int j = 0; j < Grid.GetLength(1); j++)
            {
                gridChar[(topEnd + 1) - baseRow, j] = ' ';
                Grid[(topEnd + 1) - baseRow, j] = false;
                totalCharAtGridRow[(topEnd + 1) - baseRow] = 0;
            }
        }
    }
}
