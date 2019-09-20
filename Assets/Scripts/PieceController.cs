﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameController gameController;

    public GameController.PieceType type;

    void Awake(){
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void SetType(GameController.PieceType type){
        this.type = type;
    }

    public GameController.PieceType GetType(){
        return type;
    }
    
}
