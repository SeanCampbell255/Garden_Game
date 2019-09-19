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
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        adjacencyArray = gameController.GetAdjacencies(xPosition, yPosition);

        SetAdjacencies();
    }

    // Update is called once per frame
    void Update(){
        
    }

    //Gets the position this piece is in
    void SetPosition(int x, int y){
        xPosition = x;
        yPosition = y;
    }

    void SetAdjacencies(){
        if(adjacencyArray[0] != null){
            abovePiece = adjacencyArray[0];
        }
        if(adjacencyArray[1] != null){
            rightPiece = adjacencyArray[1];
        }
        if(adjacencyArray[2] != null){
            belowPiece = adjacencyArray[2];
        }
        if(adjacencyArray[3] != null){
            leftPiece = adjacencyArray[3];
        }
    }
}
