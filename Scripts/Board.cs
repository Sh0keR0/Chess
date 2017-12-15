using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


    // TO DO NEXT: 

public class Board : MonoBehaviour
{




    // Prefabs Variables
    [Header("BoardTiles")]
    public GameObject DarkTile;
    public GameObject LightTile;
    [Space(10)]

    [Header("BlackPieces")]
    public GameObject BlackPawn;
    public GameObject BlackRook;
    public GameObject BlackKnight;
    public GameObject BlackBishop;
    public GameObject BlackKing;
    public GameObject BlackQueen;
    [Space(10)]


    [Header("WhitePieces")]
    public GameObject WhitePawn;
    public GameObject WhiteRook;
    public GameObject WhiteKnight;
    public GameObject WhiteBishop;
    public GameObject WhiteKing;
    public GameObject WhiteQueen;
    [Space(10)]


    [Header("HighlightObjects")] // Objects that will be used to highlight the selected unit / legal moves
    public GameObject FrameHighlightGameObject;
    public GameObject HighlightPathGameObject; 
    [Space(5)]


    [Header("Game Over Menu")] // Game objects used to display the game over menu
    public Text playerTurnText;
    public GameObject GameOverMenu;
    public Text GameOverText;


    //singleton instance
    public static Board instance;
    
    // public Variables
    public Unit[,] GameBoard;


    // private variables
    private Unit selectedUnit;
    private GameObject[] pathHighlight;
    private Unit.UnitSide playerTurn;
    private bool gameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        pathHighlight = new GameObject[8 * 8];

        for(int i = 0; i < pathHighlight.Length; i++)
        {
            pathHighlight[i] = Instantiate(HighlightPathGameObject,this.transform) as GameObject;
            pathHighlight[i].SetActive(false);
        }


    }

    void Start()
    {
        DrawBoard(); // Draw the tiles for the chess board

        RestartGame(); // will start the game

    }

    void RestartGame()
    {

        // Restart variables
        selectedUnit = null;
        playerTurn = Unit.UnitSide.Player1;
        gameOver = false;


        GameBoard = new Unit[8, 8]; // Create new game board
        foreach(Unit unit in Unit.AllUnits) // Destroy all pieces that are left on the game board
        {
            Destroy(unit.GetGameObject);
        }
        Unit.AllUnits = new List<Unit>(); // Empty the list;
        CreateBoardPieces(); // Create all the pieces again for each player

        playerTurnText.text = "Player 1 Turn";
        playerTurnText.color = new Color(10, 223, 108, 255);

        GameOverMenu.SetActive(false); // Display the game over popup


    }

    void CreateBoardPieces()
    {
        Unit unit;
        // Player 1 
        unit = new Rook(new Vector2(0, 0), Unit.UnitSide.Player1, WhiteRook);
        unit = new Knight(new Vector2(1, 0), Unit.UnitSide.Player1, WhiteKnight);
        unit = new Bishop(new Vector2(2, 0), Unit.UnitSide.Player1, WhiteBishop);
        unit = new King(new Vector2(3, 0), Unit.UnitSide.Player1, WhiteKing);
        unit = new Queen(new Vector2(4, 0), Unit.UnitSide.Player1, WhiteQueen);
        unit = new Bishop(new Vector2(5, 0), Unit.UnitSide.Player1, WhiteBishop);
        unit = new Knight(new Vector2(6, 0), Unit.UnitSide.Player1, WhiteKnight);
        unit = new Rook(new Vector2(7, 0), Unit.UnitSide.Player1, WhiteRook);

        for(int i = 0; i < 8; i ++) // pawns
        {
            unit = new Pawn(new Vector2(i, 1), Unit.UnitSide.Player1, WhitePawn);
        }


        // Player 2
        unit = new Rook(new Vector2(0, 7), Unit.UnitSide.Player2, BlackRook);
        unit = new Knight(new Vector2(1, 7), Unit.UnitSide.Player2, BlackKnight);
        unit = new Bishop(new Vector2(2, 7), Unit.UnitSide.Player2, BlackBishop);
        unit = new King(new Vector2(3, 7), Unit.UnitSide.Player2, BlackKing);
        unit = new Queen(new Vector2(4, 7), Unit.UnitSide.Player2, BlackQueen);
        unit = new Bishop(new Vector2(5, 7), Unit.UnitSide.Player2, BlackBishop);
        unit = new Knight(new Vector2(6, 7), Unit.UnitSide.Player2, BlackKnight);
        unit = new Rook(new Vector2(7, 7), Unit.UnitSide.Player2, BlackRook);

        for (int i = 0; i < 8; i++) // pawns
        {
            unit = new Pawn(new Vector2(i, 6), Unit.UnitSide.Player2, BlackPawn);
        }


    } // Create the chess pieces for each player

    public void DrawBoard() // Draws a 8 by 8 chess board
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {

                GameObject objectToCreate;

                if (y % 2 == 0)
                {
                    if (x % 2 == 0)
                    {
                        objectToCreate = DarkTile;
                    }
                    else
                    {
                        objectToCreate = LightTile;
                    }
                }
                else
                {
                    if (x % 2 == 0)
                    {
                        objectToCreate = LightTile;
                    }
                    else
                    {
                        objectToCreate = DarkTile;
                    }
                }

                Instantiate(objectToCreate, new Vector2(x, y), Quaternion.identity, this.transform);

            }

        }


    }


    public bool OnTilePressed(Vector2 tilePosition) // Will be called when the player presses on one of the tiles on the board
    {

        if (gameOver) return false; // Don't do anything if the game is over

        int x = (int)tilePosition.x;
        int y = (int)tilePosition.y;


        if(selectedUnit == null) 
        {
            if (GameBoard[x, y] != null && playerTurn == GameBoard[x,y].Side)
            {
                selectedUnit = GameBoard[x, y];
                Highlight(GameBoard[x, y]);
                HighlightPath(GameBoard[x, y]);
            }
        }
        else
        {
            if(selectedUnit.Move(tilePosition))
            {
                SwitchTurn();
            }
            HighlightDisable();
            selectedUnit = null;
        }


        return true;
    }


    void SwitchTurn()
    {
        if (playerTurn == Unit.UnitSide.Player1)
        {
            playerTurnText.text = "Player 2 Turn";
            playerTurnText.color = new Color(0, 255, 255,255);
            playerTurn = Unit.UnitSide.Player2;
        }
        else
        {
            playerTurnText.text = "Player 1 Turn";
            playerTurnText.color = new Color(0, 255, 107);
            playerTurn = Unit.UnitSide.Player1;
        }

    }

    public void Win(Unit.UnitSide player) // player will win the game
    {
        gameOver = true;
        HighlightDisable();
        GameOverMenu.SetActive(true);

        GameOverText.text = "Player " + ((int)player + 1) + " win!";
    }

    public void OnRetryButtonPressed()
    {
        RestartGame();
    }


    #region Hightlight
    // Highlight the unit with a frame
    void Highlight(Unit unit)
    {
        FrameHighlightGameObject.transform.position = unit.Position;
        FrameHighlightGameObject.SetActive(true);
    }

    //Highlight all the legal moves the piece can do
    void HighlightPath(Unit unit)
    {
        List<Vector2> positions = unit.GetUnitMovement();
        for(int i = 0; i < positions.Count; i++)
        {
            pathHighlight[i].transform.position = positions[i];
            pathHighlight[i].SetActive(true);
        }
    }

    // Disables all highlights
    void HighlightDisable()
    {
        FrameHighlightGameObject.SetActive(false);

        foreach(GameObject go in pathHighlight)
        {
            if(go.activeInHierarchy)
              go.SetActive(false);
        }
    }
    #endregion

}
