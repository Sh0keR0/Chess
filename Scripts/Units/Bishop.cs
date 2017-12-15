using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Bishop : Unit
{

    public Bishop(Vector2 position, UnitSide side, GameObject objectPrefab) : base(position, side, objectPrefab)
    {

    }

    public override List<Vector2> GetUnitMovement()
    {
        List<Vector2> legalMoves = new List<Vector2>();

        checkDirection(1, 1, (int)Position.x, (int)Position.y, ref legalMoves); // Check top right
        checkDirection(1, -1, (int)Position.x, (int)Position.y, ref legalMoves); // Check bottom right
        checkDirection(-1, 1, (int)Position.x, (int)Position.y, ref legalMoves); // Check top left
        checkDirection(-1, -1, (int)Position.x, (int)Position.y, ref legalMoves); // Check bottom left




        return legalMoves;
    }

    void checkDirection(int dir_x, int dir_y, int startX, int startY, ref List<Vector2> legalMoves)
    {
    

        for (int x = startX + dir_x, y = startY + dir_y; (x < 8 && x >= 0) && (y < 8 && y >= 0); x += dir_x, y += dir_y)
        {
            if (Board.instance.GameBoard[x, y] != null)
            {
                if (Board.instance.GameBoard[x, y].Side != this.Side)
                {
                    legalMoves.Add(new Vector2(x, y));
                }
                break;

            }
            legalMoves.Add(new Vector2(x, y));

        }

    }

    public override Type getType()
    {
        return Type.Bishop;
    }

}
