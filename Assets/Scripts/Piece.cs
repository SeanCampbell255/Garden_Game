using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //Public Instance Vars
    public enum Type {trash, seed, sprout, bud, flower};

    public Game game;

    //Private Instance Vars
    private Type type;

    private int[] coords;

    public void SetType(Type type)
    {
        this.type = type;

        
    }

    public Type GetType()
    {
        return this.type;
    }

    public void Move(int[] targetCoords)
    {
        game.RemoveFromMatrix(coords);

        game.AddToMatrix(this.gameObject, targetCoords);

        //Play anim & change position
    }
}
