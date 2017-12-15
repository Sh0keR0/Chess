using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Rook : Unit
{

    public Rook(Vector2 position, UnitSide side, GameObject objectPrefab) : base(position, side, objectPrefab)
    {

    }

    public override List<Vector2> GetUnitMovement()
    {
        List<Vector2> legalMoves = new List<Vector2>();

        for(int x = (int)Position.x - 1; x >= 0; x--) // Check all the tiles to the left of the unit
        {
            if(!checkTile(x,(int)Position.y,ref legalMoves))
            {
                break;
            }
  
        }

        for (int x = (int)Position.x + 1; x < 8; x++) // Check all the tiles to the right of the unit
        {
            if (!checkTile(x, (int)Position.y, ref legalMoves))
            {
                break;
            }
        }

        for (int y = (int)Position.y - 1; y >= 0; y--) // Check all the tiles to the bottom of the unit
        {
          
            if (!checkTile((int)Position.x, y, ref legalMoves))
            {
                break;
            }
        }

        for (int y = (int)Position.y + 1; y < 8; y++) // Check all the tiles to the top of the unit
        {
                
            if (!checkTile((int)Position.x, y, ref legalMoves))
            {
                break;
            }
        }

        return legalMoves;
    }

    bool checkTile(int x, int y, ref List<Vector2> legalMoves)
    {
        Unit unit = Board.instance.GameBoard[x, y];
        if (unit != null) // IF there is a piece on this position of the board
        {

            // This will check if the piece on the tile is an enemy piece
            // If true, it will add the tile it's on as legal move, if it is an ally piece, it will NOT add it to the list, preventing the piece from trying to capture his own ally.
            if (unit.Side != this.Side)
            {
                legalMoves.Add(new Vector2(x, y));
            }
            return false; // returns false which signals to stop searching.
        }
        
        legalMoves.Add(new Vector2(x, y));
        return true;
    }


    public override Type getType()
    {
        return Type.Rook;
    }

}
