﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //Public Variables, mostly for setting stuff in the the editor
    public GameObject board;
    public GameObject player;
    public GameObject piece;
    public GameObject outline;

    public PieceController basketDisplay;

    public enum PieceType {Trash, Seed, Sprout, Bud, Flower };//PieceType is here because it has the most references in this script

    public int matchSize;//minimum number of pieces needed to match
    public int numInitialRows;
    public int score = 0;
    public int matchScoreValue;

    public float timeBetweenMatches;
    public float timeBetweenRows;//ammount of time before new row sent
    public float timeForPieceMovement;

    public UIManager ui;

    public SoundController sound;

    //Private Variables
    private int boardWidth = 7;
    private int boardHeight = 12;
    private int basketSize = 0;
    private int highScore;

    private bool checkingMatches = false;
    private bool piecesMoving = false;

    private PieceType basketType;

    private GameObject[] outlineArray = new GameObject[30];
    private GameObject[,] boardArray = new GameObject[7, 12];
    private List<int[]> matchingCoordinates = new List<int[]>();
    private Queue<int[]> matchCheckQueue = new Queue<int[]>(); 


    // Instantiate
    void Start(){
        GameObject currentRow;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        ui.SetHighScore(highScore);

        //Creates an array filled with tile game objects
        for (int i = 0; i < boardHeight; i++){
            currentRow = board.transform.GetChild(i).gameObject;

            for(int j = 0; j < boardWidth; j++){
                boardArray[j, i] = currentRow.transform.GetChild(j).gameObject;
            }
        }

        InitializeBoard();
        StartCoroutine(SpawnRows());
    }
    //Creates the initial rows of pieces
    private void InitializeBoard(){
        for(int i = 0; i < numInitialRows; i++){

            for(int j = 0; j < 7; j++){
                PieceType randType = RandomPieceType();

                Instantiate(piece, boardArray[j, i].transform, false).GetComponent<PieceController>().SetType(randType);
            }
        }
    }
    //Returns a random PieceType
    private PieceType RandomPieceType(){
        int num = (int)(UnityEngine.Random.value * 4);

        return (PieceType)num;
    } 

    //Moves player to playerPosition and childs it to the tile at position
    public void UpdatePlayerPosition(int playerPosition){
        GameObject targetTile = boardArray[playerPosition, 11];

        player.transform.SetParent(targetTile.transform, false);
    }

    public void DisplayOutline(int column)
    {
        //Clears old outline
        foreach (GameObject obj in outlineArray)
        {
            Destroy(obj);
        }
        Array.Clear(outlineArray, 0, outlineArray.Length);


        int[] topTile = FindHighestEmptyTile(column);
        float x = (column - 3) * 0.9f;
        float y;

        for (int i = 0; i < basketSize; i++)
        {
            y = 0.46f + (4.54f - (topTile[1] + i) * 0.9f);
            GameObject newOutline = Instantiate(outline, new Vector3(x, y, 0.0f), Quaternion.identity);
            outlineArray[i] = newOutline;
        }
    }

    public void RemoveOutlines()
    {
        
    }

    //Grabs all pieces of same type adjacent to lowest piece in column
    public void Grab(int playerPosition){
        //Access each tile from bottom up only in player's column
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
                
                if(matchingCoordinates.Contains(new int[] {playerPosition, i })){
                    matchingCoordinates.Clear();
                }
                PieceType currentPieceType = currentPiece.GetType();
                if (basketSize == 0){
                    basketType = currentPieceType;
                }else if(currentPieceType != basketType){
                    break;
                }
                basketSize++;
                currentPiece.beingGrabbed = true;

                basketDisplay.SetType(currentPieceType);
                ui.SetBasketText(basketSize);

                StartCoroutine(MovePiece(currentPiece.gameObject, new int[] { playerPosition, 11 }, 11 - i, true));
            }
        }
        print(basketSize);
    }
    //Empties basket into player column
    public void Place(int playerPosition){
        //Does nothing if basket empty
        if (basketSize > 0) {

            GameObject existingPiece;
            int[] tilePosition;
            //Finding empty tile starting at top of column
            for (int i = 0; i < 12; i++) {

                tilePosition = new int[] {playerPosition, i};
                existingPiece = GetPiece(tilePosition);
                if(existingPiece == null){
                    //Placing pieces
                    while(basketSize > 0){

                        tilePosition = new int[] { playerPosition, i };
                        GameObject spawn = Instantiate(piece, boardArray[playerPosition, 11].transform, false);
                        spawn.GetComponent<PieceController>().SetType(basketType);
                        StartCoroutine(MovePiece(spawn, tilePosition, i - 11, false));

                        i++;
                        basketSize--;

                        if (i > 11)
                            GameOver();
                    }
                    matchCheckQueue.Enqueue(tilePosition);
                    StartCoroutine(WaitThenExecuteMatch(timeBetweenMatches));

                    ui.SetBasketText(0);
                    break;//Once we find a place to put pieces we're done with the loop
                }
            }
        }
    }
    //Adds current tile to coordinates to check, then if there's a piece of the same type adjacent it calls CheckForMatch on it
    private void CheckForMatch(int[] tilePosition, PieceType matchType){
        matchingCoordinates.Add(tilePosition);

        int[] leftCoords = {tilePosition[0] - 1, tilePosition[1]};
        int[] rightCoords = {tilePosition[0] + 1, tilePosition[1]};
        int[] topCoords = {tilePosition[0], tilePosition[1] - 1};
        int[] botCoords = {tilePosition[0], tilePosition[1] + 1};

        GameObject leftPiece = GetPiece(leftCoords);
        GameObject rightPiece = GetPiece(rightCoords);
        GameObject topPiece = GetPiece(topCoords);
        GameObject botPiece = GetPiece(botCoords);
        //Checks if there's a piece, then if it's already in the list, then if it's the same type as others that are matching
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
    //Executes a match after match checking has processed all relevent pieces and there are more than the matchSize in the matchingCoordinates list
    private int ExecuteMatch(int column){
        int topRow = 12;
        int numPieces= 0;
        PieceType matchingType = GetPiece(matchingCoordinates[0]).GetComponent<PieceController>().type;

        if(matchingCoordinates.Count >= matchSize){
            numPieces = matchingCoordinates.Count;

            foreach(int[] coord in matchingCoordinates){
                if (topRow > coord[1])
                    topRow = coord[1];
                DestroyImmediate(GetPiece(coord));
            }

            int[] tilePos = FindHighestEmptyTile(column);
            GameObject tile = boardArray[tilePos[0], tilePos[1]];

            //Creates a new piece on passed column at highest empty position
            if (matchingType != PieceType.Flower){
                Instantiate(piece, tile.transform, false).GetComponent<PieceController>().SetType(matchingType + 1);
                matchCheckQueue.Enqueue(tilePos);
            }

            //If a piece has an empty tile above it, it moves up then goes into the matchCheckQueue
            //This works for multiple pieces with an empty tile above
            foreach(int[] coord in matchingCoordinates){
                int[] botCoord = { coord[0], coord[1] + 1};
                GameObject botPiece = GetPiece(botCoord);
                int i = 0;

                while(botPiece != null){

                    tilePos = FindHighestEmptyTile(botCoord[0]);
                    tile = boardArray[tilePos[0], tilePos[1]];
                    int distance = tilePos[1] - botCoord[1];
                    StartCoroutine(MovePiece(botPiece, tilePos, distance, false));
                    matchCheckQueue.Enqueue(tilePos);

                    botCoord[1]++;
                    botPiece = GetPiece(botCoord);
                    i++;
                }
            }
        }
        matchingCoordinates.Clear();
        return numPieces;
    }
    //Checks if passed coordinates are already in the matchingCoordinates list
    private bool DoCoordinatesMatch(int[] coords){
        foreach(int[] oldCoords in matchingCoordinates){
            if(oldCoords[0] == coords[0] && oldCoords[1] == coords[1]){
                return true;
            }
        }
        return false;
    }
    //Finds the highest empty tile in a passed column
    private int[] FindHighestEmptyTile(int column){
        for(int i = 0; i < 12; i++){
            if(boardArray[column, i].transform.childCount == 0){
                return new int[] { column, i };
            }
        }
        return null;
    }
    //Checks a passed coordinate for any pieces, if out of bounds or doesn't contain a piece it returns null
    private GameObject GetPiece(int[] tilePos){
        if(tilePos[0] < 0 || tilePos[0] > 6 || tilePos[1] < 0 || tilePos[1] > 11){
            return null;
        }

        Transform tileTransform = boardArray[tilePos[0], tilePos[1]].transform;
        if(tileTransform.childCount > 1){
            return tileTransform.GetChild(1).gameObject;//doesn't handle case where player is second child PLS FIX
        }else if(tileTransform.childCount > 0 && tileTransform.GetChild(0).tag != "Player"){
            return tileTransform.GetChild(0).gameObject;
        }
        else{
            return null;
        }
    }
    //GameOver method
    private void GameOver(){
        Debug.Log("game over");
        Time.timeScale = 0.0f;
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            ui.SetNewHighScore(highScore);
        }

        sound.StopMusic();
        sound.PlayGameover();

        System.Array.Clear(boardArray, 0, boardArray.Length);
        matchCheckQueue.Clear();
        matchingCoordinates.Clear();

        ui.GameOver();
    }
    //Spawns rows of random pieces every timeBetweenRows seconds, also moves all existing pieces down a row
    private IEnumerator SpawnRows(){
        List<GameObject> piecesToBeMoved = new List<GameObject>();
        List<int[]> coordsToBeMoved = new List<int[]>();

        while (true){
            yield return new WaitForSeconds(timeBetweenRows);
            timeBetweenRows -= 0.2f;
            Debug.Log(timeBetweenRows);

            //Adding all pieces and coordinates that contain pieces to lists
            for(int i = 0; i < 7; i++){

                for(int j = 0; j < 12; j++){
                    GameObject piece = GetPiece(new int[] {i, j});

                    if(piece != null){
                        PieceController pieceController = piece.GetComponent<PieceController>();
                        if (!pieceController.beingGrabbed){
                            piecesToBeMoved.Add(piece);
                            coordsToBeMoved.Add(new int[] { i, j });
                        }
                    }
                }
            }
            //Moves all pieces down
            for (int i = 0; i < coordsToBeMoved.Count; i++){
                int[] initialTile = coordsToBeMoved[i];
                StartCoroutine(MovePiece(piecesToBeMoved[i], new int[] {initialTile[0], initialTile[1] + 1}, 1, false));
            }
            //Ensures ongoing matches retain their correct coordinates
            if(matchingCoordinates.Count > 0){
                foreach (int[] coord in matchingCoordinates){
                    coord[1]++;
                }
            }
            if(matchCheckQueue.Count > 0){
                foreach (int[] coords in matchCheckQueue){
                    coords[1]++;
                }
            }

            piecesToBeMoved.Clear();
            coordsToBeMoved.Clear();
            //Creates new row of random pieces
            for(int i= 0; i < 7; i++){
                PieceType randType = RandomPieceType();

                GameObject currentPiece = Instantiate(piece, boardArray[i, 0].transform, false);
                currentPiece.GetComponent<PieceController>().SetType(randType);
                currentPiece.transform.localPosition = new Vector2(0.0f, 4.45f);
                StartCoroutine(MovePiece(currentPiece, new int[] {i, 0}, 1, false));
            }
            DisplayOutline(player.GetComponent<PlayerController>().playerPosition);
        }
    }
    //Waits "time" seconds before executing match so player can see what is happening
    private IEnumerator WaitThenExecuteMatch(float time){
        int scoreMultiplier = 0;
        int numPieces;

        //Only proceeds if WaitThenExecuteMatch has not already been called
        if (!checkingMatches){
            while (matchCheckQueue.Count > 0){
                checkingMatches = true;
                if (piecesMoving){
                    yield return new WaitForSeconds(time + timeForPieceMovement);
                }else{
                    yield return new WaitForSeconds(time);
                }

                int[] tilePosition = matchCheckQueue.Dequeue();
                GameObject existingPiece = GetPiece(tilePosition);

                if (existingPiece != null){
                    PieceType type = existingPiece.GetComponent<PieceController>().type;
                    CheckForMatch(tilePosition, type);
                    numPieces = ExecuteMatch(tilePosition[0]);

                    if (numPieces > 0){
                        scoreMultiplier++;
                        score += (scoreMultiplier * matchScoreValue * ((int)type + 1) * (numPieces - 4));
                        Debug.Log("score: " + score);
                        ui.SetCombo(scoreMultiplier);
                        ui.SetScore(score);
                    }
                }
                DisplayOutline(player.GetComponent<PlayerController>().playerPosition);
            }
            Debug.Log("Checking matches set to false");
            checkingMatches = false;
            ui.SetCombo(0);
        }
    }
    private IEnumerator MovePiece(GameObject piece, int[] targetCoords, int columnShift, bool destroyAfter){
        float initialY = 4.45f * columnShift;
        int numIncrements = 10;
        float timePerIncrement = timeForPieceMovement / numIncrements;
        float movementPerIncrement = initialY / numIncrements;

        piecesMoving = true;
        if (targetCoords[1] > 11)
            GameOver();

        piece.transform.SetParent(boardArray[targetCoords[0], targetCoords[1]].transform, false);
        piece.transform.localPosition = new Vector2(0.0f, initialY);

        for (int i = 0; i < numIncrements; i++){
            piece.transform.localPosition = new Vector2(0.0f, initialY - movementPerIncrement * i);

            yield return new WaitForSeconds(timePerIncrement);
        }
        piece.transform.localPosition = new Vector2(0.0f, 0.0f);

        piecesMoving = false;
        if (destroyAfter){
            Destroy(piece);
        }
    }
}
