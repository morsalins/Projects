using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class Play
    {
        public Bucket bucket;
        public int score = 0;

        public Play()
        {
            // Default Constructor
        }

        public void playGame()
        {
            bucket = new Bucket(15, 55, 5, 40);
            bucket.buildBucket();
            buildScorline();

            ConsoleKeyInfo k;

            do
            {
                k = Console.ReadKey();
                if (k.Key == ConsoleKey.DownArrow)
                {
                    updateScore();
                    showScore();
                }
            } while (k.Key != ConsoleKey.Escape);
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
    }
}
