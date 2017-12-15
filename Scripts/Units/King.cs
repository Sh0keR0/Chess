using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class King : Unit
{

    public King(Vector2 position, UnitSide side, GameObject objectPrefab) : base(position, side, objectPrefab)
    {

    }

    public override List<Vector2> GetUnitMovement()
    {
        List<Vector2> legalMoves = new List<Vector2>();

        for(int x = (int)Position.x - 1; x <= (int)Position.x + 1; x++)
        {
            for(int y = (int)Position.y - 1; y <= (int)Position.y + 1; y++)
            {
                if(isInsideTheBoard(x,y) && (new Vector2(x,y) != Position))
                {
                       Unit unit = getPieceOnTile(x, y);
                       if(unit != null && unit.Side == this.Side)
                       {
                          // Do nothing
                       }else
                       {
                           legalMoves.Add(new Vector2(x, y));
                       }

                }
            }
        }


        return legalMoves;
    }

    public override Type getType()
    {
        return Type.King;
    }

}
