using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class LineShape : TetrisShapes
    {
        public LineShape(int x, int y)
            : base(x, y, x - 1, x + 2, y, y)
        {
            allRotations = new char[][][]{
                             new char[][]{new char[]{shapeChar, shapeChar, shapeChar, shapeChar},
                                          new char[]{shapeNull, shapeNull, shapeNull, shapeNull},
                                          new char[]{shapeNull, shapeNull, shapeNull, shapeNull},
                                          new char[]{shapeNull, shapeNull, shapeNull, shapeNull},
                                        },
                             new char[][]{new char[]{shapeChar, shapeNull, shapeNull, shapeNull},
                                          new char[]{shapeChar, shapeNull, shapeNull, shapeNull},
                                          new char[]{shapeChar, shapeNull, shapeNull, shapeNull},
                                          new char[]{shapeChar, shapeNull, shapeNull, shapeNull},
                                        }
                                      };

            allDirection = new int[2, 4] {{ -1, 2, 0, 0 }, 
                                          {0, 0, -1, 2 }
                                         };
        }

        public override bool canRotate()
        {
            int nextRotation = (currentRotation + 1) % allRotations.Length;

            if (nextRotation == 1)
            {
                return centerY + 2 < Bucket.bottomEnd - 1 && centerY - 1 > Bucket.topEnd
                    && !Bucket.Grid[(centerY - 1) - Bucket.baseRow, centerX - Bucket.baseCol]
                    && !Bucket.Grid[(centerY + 1) - Bucket.baseRow, centerX - Bucket.baseCol]
                    && !Bucket.Grid[(centerY + 2) - Bucket.baseRow, centerX - Bucket.baseCol];
            }

            else if (nextRotation == 0)
            {
                return centerX + 2 <= Bucket.rightEnd && centerX - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[centerY - Bucket.baseRow, (centerX - 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (centerX + 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (centerX + 2) - Bucket.baseCol];
            }
            return false;
        }

        public override bool canMoveLeft()
        {
            if (currentRotation == 0)
            {
                return left - 1 >= Bucket.leftEnd && !Bucket.Grid[top - Bucket.baseRow, (left - 1) - Bucket.baseCol];
            }

            else if (currentRotation == 1)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[(centerY + 1) - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (left - 1) - Bucket.baseCol];
            }
            return false;
        }

        public override bool canMoveRight()
        {
            if (currentRotation == 0)
            {
                return right + 1 <= Bucket.rightEnd && !Bucket.Grid[top - Bucket.baseRow, (right + 1) - Bucket.baseCol];
            }

            else if (currentRotation == 1)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[(centerY + 1) - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (right + 1) - Bucket.baseCol];
            }
            return false;
        }

        public override bool canMoveDown(int steps)
        {
            int x = 1;
            bool Continue = true;

            if (currentRotation == 0)
            {
                while (x <= steps && Continue)
                {
                    if (bottom + x < Bucket.bottomEnd
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, left - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, centerX - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, (centerX + 1) - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, right - Bucket.baseCol])
                    {
                        x++;
                    }
                    else Continue = false;
                }
            }
            else if (currentRotation == 1)
            {
                while (x <= steps && Continue)
                {
                    if (bottom + x < Bucket.bottomEnd && !Bucket.Grid[(bottom + x) - Bucket.baseRow, left - Bucket.baseCol])
                    {
                        x++;
                    }
                    else Continue = false;
                }
            }
            downsteps = x - 1;
            return downsteps != 0;
        }
    }
}
