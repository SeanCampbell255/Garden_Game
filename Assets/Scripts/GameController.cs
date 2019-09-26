using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables
    public GameObject board;
    public GameObject player;
    public GameObject piece;

    public enum PieceType {None, Trash, Seed, Sprout, Bud, Flower };

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private int basketSize = 0;

    private PieceType basketType;
    

    private GameObject[,] boardArray = new GameObject[7, 12];
    private List<GameObject> matchingPieces = new List<GameObject>();


    // Instantiate & Preprocess
    void Start(){

        GameObject currentRow;

        //Creates an array filled with tile game objects
        for(int i = 0; i < boardHeight; i++){
            currentRow = board.transform.GetChild(i).gameObject;

            for(int j = 0; j < boardWidth; j++){
                boardArray[j, i] = currentRow.transform.GetChild(j).gameObject;
            }
        }
    }

    //Moves player to playerPosition and childs it to the tile at position
    public void UpdatePlayerPosition(int playerPosition){
        GameObject targetTile = boardArray[playerPosition, 11];

        player.transform.SetParent(targetTile.transform, false);
    }

    //Grabs all pieces of same type adjacent to lowest piece in column
    public void Grab(int playerPosition){
        //Access each tile from bottom up
        for(int i = 11; i >=0; i--){
            GameObject currentTile = boardArray[playerPosition, i];
            int childCount = currentTile.transform.childCount;
            PieceController currentPiece = null;

            //Gets the child piece, the first condition is for when it's on the same tile as player
            if(childCount > 1){
                currentPiece = currentTile.transform.GetChild(1).gameObject.GetComponent<PieceController>();
            }else if(childCount > 0){
                GameObject currentChild = currentTile.transform.GetChild(0).gameObject;

                if(currentChild.transform.tag != "Player"){
                    currentPiece = currentChild.GetComponent<PieceController>();
                }
            }
            //Update basketSize and basketType based on pickups and state of basketType
            //This discriminates between pieces of the current basketType and other types
            if (currentPiece != null){
                PieceType currentPieceType = currentPiece.GetType();
                if (basketSize == 0){
                    basketType = currentPieceType;
                }else if(currentPieceType != basketType){
                    break;
                }
                basketSize++;
                Destroy(currentPiece.gameObject);
            }
        }
        print(basketSize);
    }

    public void Place(int playerPosition){
        if (basketSize > 0) {
            for (int i = 11; i >= 0; i--) {
                print(basketSize);
                GameObject currentTile = boardArray[playerPosition, i];
                int childCount = currentTile.transform.childCount;

                if (childCount > 1) {
                    GameOver();
                } else if (childCount > 0 && currentTile.transform.GetChild(0).tag != "Player"){

                    while(basketSize > 0){
                        if(++i > 11){
                            GameOver();
                            break;
                        }
                        else{
                            PieceController currentPiece = Instantiate(piece, boardArray[playerPosition, i].transform, false).GetComponent<PieceController>();
                            currentPiece.SetType(basketType);
                            basketSize--;
                            print(basketSize);
                        }
                    }
                    break;
                } else if (i == 0){
                    i--;
                    while (basketSize > 0){
                        if (++i > 11)
                        {
                            GameOver();
                            break;
                        }
                        else
                        {
                            PieceController currentPiece = Instantiate(piece, boardArray[playerPosition, i].transform, false).GetComponent<PieceController>();
                            currentPiece.SetType(basketType);
                            basketSize--;
                            print(basketSize);
                        }
                    }
                    break;
                }
            }
        }
        basketType = PieceType.None;
        
    }

    private void initialCheckMatch(GameObject currentTile, int[] currentPosition){
        checkMatch(currentTile, currentPosition);

        foreach(GameObject piece in matchingPieces){
            if(matchingPieces.Count >= 5){
                Destroy(piece);
            }
        }
    }

    private void checkMatch(GameObject currentTile, int[] currentPosition){
        //Checks if there's a piece on currentTile and sets it to currentPiece if there is
        GameObject currentPiece = getPiece(currentTile);
        if (currentPiece == null)
            return;

        if(basketType == PieceType.None){
            return;
        }else if(basketType != currentPiece.GetComponent<PieceController>().type){
            return ;
        }
        else if(!matchingPieces.Contains(currentPiece)){
            matchingPieces.Add(currentPiece);
            checkAdjacencies(currentTile, currentPosition);
            return;
        }
        else{
            return;
        }
    }

    private void checkAdjacencies(GameObject currentTile, int[] currentPosition){
        GameObject leftPiece = null;
        GameObject rightPiece = null;
        GameObject topPiece = null;
        GameObject botPiece = null;
        GameObject[] adjacencyArray = { leftPiece, rightPiece, topPiece, botPiece };

        int[] leftPos = { currentPosition[0] - 1, currentPosition[1] };
        int[] rightPos = { currentPosition[0] + 1, currentPosition[1] };
        int[] topPos = { currentPosition[0], currentPosition[1] - 1 };
        int[] botPos = { currentPosition[0], currentPosition[1] + 1 };

        if (leftPos[0] >= 0)
            leftPiece = getPiece(boardArray[leftPos[0], leftPos[1]]);
        if (rightPos[0] <= 6)
            rightPiece = getPiece(boardArray[rightPos[0], rightPos[1]]);
        if (topPos[1] >= 0)
            topPiece = getPiece(boardArray[topPos[0], topPos[1]]);
        if (botPos[1] <= 11)
            botPiece = getPiece(boardArray[botPos[0], botPos[1]]);

        foreach (GameObject piece in adjacencyArray){
            if(piece != null && !matchingPieces.Contains(piece)){
                if(piece == leftPiece){
                    checkMatch(piece, leftPos);
                }else if(piece == rightPiece){
                    checkMatch(piece, rightPos);
                }else if(piece == topPiece){
                    checkMatch(piece, topPos);
                }
                else{
                    checkMatch(piece, botPos);
                }
            }
        }
    }

    private GameObject getPiece(GameObject currentTile){
        //With the rules we currently have and anticipate, when there's more than 1 child it means the player and piece are on the same tile
        if(currentTile.transform.childCount > 1){
            return currentTile.transform.GetChild(1).gameObject;
        }else if(currentTile.transform.childCount > 0){
            if(currentTile.transform.GetChild(0).tag == "Player"){
                return null;
            }else{
                return currentTile.transform.GetChild(0).gameObject;
            }
        }else{
            return null;
        }
    }

    private void GameOver(){
        print("Big RIP");
    }
}
