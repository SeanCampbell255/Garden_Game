using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables
    public GameObject board;
    public GameObject player;
    public GameObject piece;

    public enum PieceType {Trash, Seed, Sprout, Bud, Flower };

    public int matchSize;

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private int basketSize = 0;

    private PieceType basketType;
    

    private GameObject[,] boardArray = new GameObject[7, 12];
    private List<int[]> matchingCoordinates = new List<int[]>();


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

            GameObject existingPiece;
            int[] tilePosition;
            for (int i = 0; i < 12; i++) {

                tilePosition = new int[] {playerPosition, i};
                existingPiece = GetPiece(tilePosition);
                if(existingPiece == null){

                    while(basketSize > 0){

                        tilePosition = new int[] { playerPosition, i };
                        Instantiate(piece, boardArray[tilePosition[0], tilePosition[1]].transform,
                                    false).GetComponent<PieceController>().SetType(basketType);
                        i++;
                        basketSize--;
                    }
                    CheckForMatch(tilePosition, basketType);
                    ExecuteMatch(playerPosition);
                    break;
                }
            }
        }
    }

    private void CheckForMatch(int[] tilePosition, PieceType matchType){
        Debug.Log("Checking at pos: " + tilePosition[0] + " " + tilePosition[1]);
        matchingCoordinates.Add(tilePosition);

        int[] leftCoords = {tilePosition[0] - 1, tilePosition[1]};
        int[] rightCoords = {tilePosition[0] + 1, tilePosition[1]};
        int[] topCoords = {tilePosition[0], tilePosition[1] - 1};
        int[] botCoords = {tilePosition[0], tilePosition[1] + 1};

        GameObject leftPiece = GetPiece(leftCoords);
        GameObject rightPiece = GetPiece(rightCoords);
        GameObject topPiece = GetPiece(topCoords);
        GameObject botPiece = GetPiece(botCoords);

        if(leftPiece != null && !DoCoordinatesMatch(leftCoords)){
            if(leftPiece.GetComponent<PieceController>().type == matchType){
                CheckForMatch(leftCoords, matchType);
            }
        }
        if(rightPiece != null && !DoCoordinatesMatch(rightCoords)){
            if(rightPiece.GetComponent<PieceController>().type == matchType){
                CheckForMatch(rightCoords, matchType);
            }
        }
        if(topPiece != null && !DoCoordinatesMatch(topCoords)){
            if(topPiece.GetComponent<PieceController>().type == matchType){
                CheckForMatch(topCoords, matchType);
            }
        }
        if(botPiece != null && !DoCoordinatesMatch(botCoords)){
            if(botPiece.GetComponent<PieceController>().type == matchType){
                CheckForMatch(botCoords, matchType);
            }
        }
    }

    private void ExecuteMatch(int column){
        int topRow = 12;
        if(matchingCoordinates.Count >= matchSize){
            foreach(int[] coord in matchingCoordinates){
                if (topRow > coord[1])
                    topRow = coord[1];
                DestroyImmediate(GetPiece(coord));
            }
            Instantiate(piece, boardArray[column, topRow].transform, false).GetComponent<PieceController>().SetType(basketType + 1);

            foreach(int[] coord in matchingCoordinates){
                int[] botCoord = { coord[0], coord[1] + 1};
                GameObject botPiece = GetPiece(botCoord);

                while(botPiece != null){
                    Debug.Log(botPiece.GetComponent<PieceController>().type + " at " + botCoord[0] + " " + botCoord[1]);
                    botPiece.transform.SetParent(FindHighestEmptyTile(botCoord[0]).transform, false);

                    botCoord[1]++;
                    botPiece = GetPiece(botCoord);
                }
            }
        }
        matchingCoordinates.Clear();
    }

    private bool DoCoordinatesMatch(int[] coords){
        foreach(int[] oldCoords in matchingCoordinates){
            if(oldCoords[0] == coords[0] && oldCoords[1] == coords[1]){
                return true;
            }
        }
        return false;
    }

    private GameObject FindHighestEmptyTile(int column){
        for(int i = 0; i < 12; i++){
            if(boardArray[column, i].transform.childCount == 0){
                Debug.Log("Highest empty tile at: " + column + " " + i);
                return boardArray[column, i];
            }
        }
        return null;
    }

    private GameObject GetPiece(int[] tilePos){
        if(tilePos[0] < 0 || tilePos[0] > 6 || tilePos[1] < 0 || tilePos[1] > 11){
            return null;
        }

        Transform tileTransform = boardArray[tilePos[0], tilePos[1]].transform;
        if(tileTransform.childCount > 1){
            return tileTransform.GetChild(1).gameObject;
        }else if(tileTransform.childCount > 0 && tileTransform.GetChild(0).tag != "Player"){
            return tileTransform.GetChild(0).gameObject;
        }
        else{
            return null;
        }
    }

    private void GameOver(){
        print("Big RIP");
    }
}
