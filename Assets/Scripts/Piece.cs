using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //Public Instance Vars
    public enum Type {trash, seed, sprout, bud, flower};

    public Game game;

    public float moveTime = 0.33f;

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
        int vector = coords[1] - targetCoords[1];

        game.RemoveFromMatrix(coords);

        game.AddToMatrix(this.gameObject, targetCoords);

        //Play anim & change position
    }

    private IEnumerator MoveAnim(int vector)
    {
        float originX = gameObject.transform.position.x;
        float originY = gameObject.transform.position.y;
        float move = (0.9f * vector) / 8;
        float interval = moveTime / 8;

        for (int i = 1; i <= 8; i++)
        {
            gameObject.transform.position = new Vector2(originX, originY + (move * i));

            yield return new WaitForSeconds(interval);
        }
    }
}
