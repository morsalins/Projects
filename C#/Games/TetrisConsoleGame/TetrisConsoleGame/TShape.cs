using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisConsoleGame
{
    class TShape : TetrisShapes
    {

        public TShape(int x, int y)
            : base(x, y, x - 1, x + 1, y, y + 1)
        {
            allRotations = new char[][][]{
                            new char[][]{new char[]{shapeChar, shapeChar, shapeChar},
                                         new char[]{shapeNull, shapeChar, shapeNull},
                                         new char[]{shapeNull, shapeNull, shapeNull},
                                        },
                            new char[][]{new char[]{shapeNull, shapeChar, shapeNull},
                                         new char[]{shapeChar, shapeChar, shapeNull},
                                         new char[]{shapeNull, shapeChar, shapeNull}
                                        },
                            new char[][]{new char[]{shapeNull, shapeChar, shapeNull},
                                         new char[]{shapeChar, shapeChar, shapeChar},
                                         new char[]{shapeNull, shapeNull, shapeNull}
                                        },
                            new char[][]{new char[]{shapeChar, shapeNull, shapeNull},
                                         new char[]{shapeChar, shapeChar, shapeNull},
                                         new char[]{shapeChar, shapeNull, shapeNull},
                                        }
                                       };
            allDirection = new int[4, 4]{{-1, 1, 0, 1},
                                         {-1, 0, -1, 1},
                                         {-1, 1, -1, 0},
                                         {0, 1, -1, 1}
                                        };
        }

        public override bool canRotate()
        {
            int nextRotation = (currentRotation + 1) % allRotations.Length;

            if (nextRotation == 1)
            {
                return top - 1 > Bucket.topEnd
                    && !Bucket.Grid[(top - 1) - Bucket.baseRow, centerX - Bucket.baseCol];
            }
            else if (nextRotation == 2)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[centerY - Bucket.baseRow, (right + 1) - Bucket.baseCol];
            }
            else if (nextRotation == 3)
            {
                return bottom + 1 < Bucket.bottomEnd
                    && !Bucket.Grid[(bottom + 1) - Bucket.baseRow, centerX - Bucket.baseCol];
            }
            else if (nextRotation == 0)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[centerY - Bucket.baseRow, (left - 1) - Bucket.baseCol];
            }
            return false;
        }

        public override bool canMoveLeft()
        {
            if (currentRotation == 0)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, left - Bucket.baseCol];
            }
            else if (currentRotation == 1)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[top - Bucket.baseRow, left - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, left - Bucket.baseCol];
            }
            else if (currentRotation == 2)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[top - Bucket.baseRow, left - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (left - 1) - Bucket.baseCol];
            }
            else if (currentRotation == 3)
            {
                return left - 1 >= Bucket.leftEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (left - 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (left - 1) - Bucket.baseCol];
            }
            return false;
        }

        public override bool canMoveRight()
        {
            if (currentRotation == 0)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, right - Bucket.baseCol];
            }
            else if (currentRotation == 1)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[top - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (right + 1) - Bucket.baseCol];
            }
            else if (currentRotation == 2)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[top - Bucket.baseRow, right - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, (right + 1) - Bucket.baseCol];                    
            }
            else if (currentRotation == 3)
            {
                return right + 1 <= Bucket.rightEnd
                    && !Bucket.Grid[top - Bucket.baseRow, right - Bucket.baseCol]
                    && !Bucket.Grid[centerY - Bucket.baseRow, (right + 1) - Bucket.baseCol]
                    && !Bucket.Grid[bottom - Bucket.baseRow, right - Bucket.baseCol];
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
                        && !Bucket.Grid[(top + x) - Bucket.baseRow, left - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, centerX - Bucket.baseCol]
                        && !Bucket.Grid[(top + x) - Bucket.baseRow, right - Bucket.baseCol])
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
                    if (bottom + x < Bucket.bottomEnd
                        && !Bucket.Grid[(centerY + x) - Bucket.baseRow, left - Bucket.baseCol]                        
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, right - Bucket.baseCol])
                    {
                        x++;
                    }
                    else Continue = false;
                }
            }
            else if (currentRotation == 2)
            {
                while (x <= steps && Continue)
                {
                    if (bottom + x < Bucket.bottomEnd
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, left - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, centerX - Bucket.baseCol]
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, right - Bucket.baseCol])
                    {
                        x++;
                    }
                    else Continue = false;
                }
            }
            else if (currentRotation == 3)
            {
                while (x <= steps && Continue)
                {
                    if (bottom + x < Bucket.bottomEnd
                        && !Bucket.Grid[(bottom + x) - Bucket.baseRow, left - Bucket.baseCol]                        
                        && !Bucket.Grid[(centerY + x) - Bucket.baseRow, right - Bucket.baseCol])
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
