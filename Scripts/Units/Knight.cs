using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Knight : Unit
{

    public Knight(Vector2 position, UnitSide side, GameObject objectPrefab) : base(position, side, objectPrefab)
    {

    }

    public override List<Vector2> GetUnitMovement()
    {
        List<Vector2> legalMoves = new List<Vector2>();

        int current_x = (int)Position.x;
        int current_y = (int)Position.y;

        int xToCheck, yToCheck;


        // Top Right
        xToCheck = current_x + 1;
        yToCheck = current_y + 2;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));


        // Top Left
        xToCheck = current_x - 1;
        yToCheck = current_y + 2;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));


        // Right Top
        xToCheck = current_x + 2;
        yToCheck = current_y + 1;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));


        // Right Bottom
        xToCheck = current_x + 2;
        yToCheck = current_y - 1;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));

        // Left Top
        xToCheck = current_x - 2;
        yToCheck = current_y + 1;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));

        //Left Bottom
        xToCheck = current_x - 2;
        yToCheck = current_y - 1;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));

        // Bottom Right
        xToCheck = current_x + 1;
        yToCheck = current_y - 2;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));

        /// Bottom Left
        xToCheck = current_x -1;
        yToCheck = current_y -2;
        if (checkTile(xToCheck, yToCheck)) legalMoves.Add(new Vector2(xToCheck, yToCheck));


        return legalMoves;
    }

    bool checkTile(int x, int y)
    {
        if (x >= 8 || x < 0 || y >= 8 || y < 0) return false;

        if(Board.instance.GameBoard[x,y] != null)
        {
            if(Board.instance.GameBoard[x,y].Side != this.Side)
            {
                return true;
            }
            return false;
        }

        return true;
    }

    public override Type getType()
    {
        return Type.Knight;
    }

}
