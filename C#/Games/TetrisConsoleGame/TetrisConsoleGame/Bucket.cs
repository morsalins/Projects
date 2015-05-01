using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    public static class Bucket
    {
        public static int leftEnd = 10;
        public static int rightEnd = 50;
        public static int topEnd = 5;
        public static int bottomEnd = 40;

        private const char horizontalWall = '-';
        private const char verticalWall = '|';

        public static bool[,] bucketGrid = new bool[bottomEnd - topEnd + 1, rightEnd - leftEnd + 1];
        public static int baseRow = topEnd + 1;
        public static int baseCol = leftEnd;

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
            resetBucketGrid();
        }

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

        private static void resetBucketGrid()
        {
            for (int i = 0; i < bucketGrid.GetLength(0); i++)
            {
                for (int j = 0; j < bucketGrid.GetLength(1); j++)
                {
                    bucketGrid[i, j] = false;
                }
            }
        }

        public static void updateGrid(char[][] arr, int left, int top, char ch)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == ch)
                    {
                        Bucket.bucketGrid[(top + i) - baseRow, (left + j) - baseCol] = true;
                    }
                }
            }
        }
    }
}
