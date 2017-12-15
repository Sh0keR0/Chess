using UnityEngine;
using System.Collections.Generic;

public abstract class Unit
{

    // Will store all the units that are currently on the board, used to perform some checks
    public static List<Unit> AllUnits = new List<Unit>(); 

    // Enums
    public enum UnitSide { Player1, Player2};
    public enum Type { Pawn, Knight, Bishop, Rook, Queen, King};

    // Public Variables
    public Vector2 Position { get { return _position; } set { OnPositionUpdate(_position, value);  _position = value;  } }
    public UnitSide Side;
    public GameObject GetGameObject { get { return _gameObject; } }

    // Private Variables
    private GameObject _gameObject;
    private Vector2 _position = new Vector2(5,5); // the inital position of each piece is 5,5 , this allow the OnPositionUpdate to work correctly



    public Unit(Vector2 position, UnitSide side, GameObject objectPrefab)
    {
        _gameObject = GameObject.Instantiate(objectPrefab, Position, Quaternion.identity) as GameObject;
        Position = position;
        Side = side;
        AllUnits.Add(this);
        
    }



    // Public Methods

    public void Capture() // this will capture the unit and remove it from the board
    {
        GameObject.Destroy(_gameObject);
        Board.instance.GameBoard[(int)Position.x, (int)Position.y] = null;
        AllUnits.Remove(this);

        if(getType() == Type.King) // If the king was captured, announce the winning player
        {
            if(this.Side == UnitSide.Player1)
            {
                Board.instance.Win(UnitSide.Player2);
            }
            else
            {
                Board.instance.Win(UnitSide.Player1);
            }
        }
    }

    void OnPositionUpdate(Vector2 oldPosition, Vector2 newPosition)
    {
        // When the position is updated, set the previous position to null and apply the new one to the array.
            Board.instance.GameBoard[(int)oldPosition.x, (int)oldPosition.y] = null;
            Board.instance.GameBoard[(int)newPosition.x, (int)newPosition.y] = this;

        _gameObject.transform.position = newPosition; // Update the game object position
    }

    
    public bool Move(Vector2 pos) // Move the piece to position "pos"
    {
        if(CanMove(pos))
        {
            Unit unit = Board.instance.GameBoard[(int)pos.x, (int)pos.y];
            if (unit != null) // IF there is a piece on the position we are trying to move, if true, capture that unit
            {
                if(unit.Side == this.Side) // IF the unit is trying to capture an ally throw an error
                {
                    Debug.LogError("A piece on the board is trying to caputre an allied piece!");
                    return false;
                }

                unit.Capture(); // Capture the unit
            }


            // Move the chess piece to the tile if everything is correct
            this.Position = pos;
                    

            return true;
        }

        return false;
    }

    bool CanMove(Vector2 position)
    {
        foreach(Vector2 pos in GetUnitMovement())
        {
            if(pos == position) // IF one of the vectors in the list is equal to the position we want to move
            {
                return true;
            }
        }

        return false;
    }

    // Abstract methods

    public abstract List<Vector2> GetUnitMovement(); // Return a list of vectors with all the possible positions the unit can move to
    public abstract Type getType(); // Return the type of the unit: Pawn, Knight, Bishop, Rook, Queen or King.


    // Useful methods 

    public bool isInsideTheBoard(int x, int y)
    {
        if (x > 7 || x < 0 || y > 7 || y < 0) return false;

        return true;
    }


    public bool tileHasEnemyPiece(int x_pos, int y_pos)
    {
        if (isInsideTheBoard(x_pos, y_pos) && Board.instance.GameBoard[x_pos, y_pos] != null && Board.instance.GameBoard[x_pos, y_pos].Side != this.Side)
        {
            return true;
        }
        return false;
    }

    public Unit getPieceOnTile(int x, int y)
    {
        if (!isInsideTheBoard(x, y)) return null;
        return Board.instance.GameBoard[x, y];
    }



}
