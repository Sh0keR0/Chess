using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class BoardTile : MonoBehaviour {


    void OnMouseDown()
    {
        Board.instance.OnTilePressed(transform.position);
    }

}
