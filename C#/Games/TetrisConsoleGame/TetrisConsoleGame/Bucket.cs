using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class Bucket
    {
        public int leftEnd;
        public int rightEnd;
        public int topEnd;
        public int bottomEnd;

        public Bucket()
        {
            // Default Constructor
        }

        public Bucket(int l, int r, int t, int b)
        {
            this.leftEnd = l;
            this.rightEnd = r;
            this.topEnd = t;
            this.bottomEnd = b;
        }

        public void buildBucket()
        {
            Console.Clear();

            Console.SetCursorPosition(this.leftEnd, this.topEnd);
            for (int i = 1; i <= this.rightEnd - this.leftEnd + 1; i++)
            {
                Console.Write('#');
            }
            // Build the top Boundary

            Console.SetCursorPosition(this.leftEnd, this.bottomEnd);
            for (int i = 1; i <= this.rightEnd - this.leftEnd + 1; i++)
            {
                Console.Write('#');
            }
            // Build the Botom Boundary

            Console.SetCursorPosition(this.leftEnd, this.topEnd);
            for (int i = 1; i <= (this.bottomEnd-1) - (this.topEnd+1) + 1; i++)
            {
                Console.SetCursorPosition(this.leftEnd, this.topEnd + i);
                Console.Write('#');
            }
            // Build the left boundary

            Console.SetCursorPosition(this.rightEnd, this.topEnd);
            for (int i = 1; i <= (this.bottomEnd - 1) - (this.topEnd + 1) + 1; i++)
            {
                Console.SetCursorPosition(this.rightEnd, this.topEnd + i);
                Console.Write('#');
            }
            // Build the right boundary
        }     
    }
}
