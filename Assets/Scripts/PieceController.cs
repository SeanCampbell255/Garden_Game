using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    //Public Variables
    public GameController gameController;

    //Private Variables
    private GameObject[] adjacencyArray;
    private GameObject abovePiece;
    private GameObject rightPiece;
    private GameObject belowPiece;
    private GameObject leftPiece;

    private int xPosition;
    private int yPosition;

    // Start is called before the first frame update
    void Awake(){
        adjacencyArray = gameController.GetAdjacencies(xPosition, yPosition);
    }

    // Update is called once per frame
    void Update(){
        
    }

    //Gets the position this piece is in
    void SetPosition(int x, int y){
        xPosition = x;
        yPosition = y;
    }
}
