using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class BoxShape : TetrisShapes
    {
        public BoxShape(int x, int y)
            : base(x, y, x, x+1, y, y+1)
        {
            allRotations = new char[][][]{
                              new char[][]{new char[]{shapeChar, shapeChar},
                                           new char[]{shapeChar, shapeChar}
                                          }
                                         };
            allDirection = new int[1, 4]{{0, 1, 0, 1}};
        }

        public override bool canRotate()
        {
            return false;
        }

        public override bool canMoveLeft()
        {
            return left - 1 >= Bucket.leftEnd
                && !Bucket.Grid[top - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                && !Bucket.Grid[(top + 1) - Bucket.baseRow, (left - 1) - Bucket.baseCol];
        }

        public override bool canMoveRight()
        {
            return right + 1 <= Bucket.rightEnd
                && !Bucket.Grid[top - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                && !Bucket.Grid[(top + 1) - Bucket.baseRow, (right + 1) - Bucket.baseCol];
        }

        public override bool canMoveDown(int steps)
        {
            int x = 1;
            bool Continue = true;

            while (x <= steps && Continue)
            {
                if (bottom + x < Bucket.bottomEnd
                    && !Bucket.Grid[(bottom + x) - Bucket.baseRow, left - Bucket.baseCol]
                    && !Bucket.Grid[(bottom + x) - Bucket.baseRow, right - Bucket.baseCol])
                {
                    x++;
                }
                else Continue = false;
            }
            downsteps = x - 1;
            return downsteps != 0;
        } 
    }
}
