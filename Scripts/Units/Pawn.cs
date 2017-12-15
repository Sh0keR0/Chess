using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Pawn : Unit {

    private bool firstMove
    {
        get
        {
            if (this.Side == UnitSide.Player1 && this.Position.y == 1)
                return true;
            if (this.Side == UnitSide.Player2 && this.Position.y == 6)
                return true;

            return false;
        }
    }

    public Pawn(Vector2 position, UnitSide side, GameObject objectPrefab) : base(position,side,objectPrefab)
    {
       
    }

    public override List<Vector2> GetUnitMovement()
    {
        List<Vector2> legalMoves = new List<Vector2>();


        int y_dir;
        // Player1's pawn goes upwards the y axis and player2's pawn goes downwards the y axis
        if (this.Side == UnitSide.Player1)
        {
            y_dir = 1;
        }
        else { y_dir = -1; }


        // Check if there are any enemy pieces infront of the sides of the pawn
        int x_pos, y_pos;
        y_pos = (int)this.Position.y + (1 * y_dir); // Y Pos +1 for player1 or -1 for player2



        x_pos = (int)this.Position.x + 1; // check the top / bottom right tile 

        if(tileHasEnemyPiece(x_pos,y_pos))
        {
            legalMoves.Add(new Vector2(x_pos, y_pos));
        }

        x_pos = (int)this.Position.x - 1; // check the top / bottom left tile

        if (tileHasEnemyPiece(x_pos, y_pos))
        {
            legalMoves.Add(new Vector2(x_pos, y_pos));
        }
        // -----------------------------------------------------------

        Unit unit;
        int repeat = 1; // if the pawn is on his first move, check two tiles ahead instead of one
        int count = 1; 
        if (firstMove) repeat = 2;

        
        while (repeat != 0)
        {
            int x, y;
            x = (int)this.Position.x; // pawn can only move on the y axis
            y = (int)this.Position.y + (count * y_dir); // the y position of the tile that needs to be checked.
            unit = getPieceOnTile(x,y);
            if(unit != null || !isInsideTheBoard(x,y)) // if there is a unit blocking the pawn OR if the x and y values are out of the board, break;
            {
                break; // if there is a unit infront of the pawn, break the loop
            }
            
            legalMoves.Add(new Vector2(x,y));
            count++;
            repeat--;
        }




        return legalMoves;
    }



    public override Type getType()
    {
        return Type.Pawn;
    }

}
