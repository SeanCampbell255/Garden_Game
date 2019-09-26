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

    private bool checkMatch(GameObject currentTile, int[] currentPosition){
        //Checks if there's a piece on currentTile and sets it to currentPiece if there is
        GameObject currentPiece = getPiece(currentTile);
        if (currentPiece == null)
            return false;

        if(basketType == PieceType.None){
            return false;
        }else if(basketType != currentPiece.GetComponent<PieceController>().type){
            return false;
        }
        else if(!matchingPieces.Contains(currentPiece)){
            matchingPieces.Add(currentPiece);
            checkAdjacencies(currentTile, currentPosition);
            return true;
        }
        else{
            return false;
        }
    }

    private void checkAdjacencies(GameObject currentTile, int[] currentPosition){
        
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
