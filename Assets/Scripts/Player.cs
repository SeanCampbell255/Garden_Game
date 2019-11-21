using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public Instance Vars
    public Game game;

    public Animator anim;

    public float moveTime = 0.33f;

    //Private Instance Vars
    private int[] coords = { 3, 11 };

    private bool inputBlocked = false;

    private float transformConstant = 4.5f;

    private void Update()
    {
        //Input control
        if (!inputBlocked)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Move(-1);
            } else if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                Move(1);
            }
        }
    }

    //If trash in basket, dump anim & update score & basket
    private void DumpTrash()
    {
        //IMPLEMENT: Dumping trash and scoring
    }

    //Takes int direction on x-axis to move player
    private void Move(int direction)
    {
        inputBlocked = true;
        game.RemoveFromMatrix(coords);

        coords[0] += direction;
        game.AddToMatrix(this.gameObject, coords);
        StartCoroutine(AnimateMove(direction));
    }
    
    //Handles movement animation along with position and matrix updates
    private IEnumerator AnimateMove(int direction)
    {
        float originX = gameObject.transform.position.x;
        float originY = gameObject.transform.position.y;
        float move = (0.9f * direction) / 8;
        float interval = moveTime / 8;

        anim.SetTrigger("Walk");

        for(int i = 1; i <= 8; i++)
        {
            gameObject.transform.position = new Vector2(originX + (move * i), originY);
            
            yield return new WaitForSeconds(interval);
        }

        inputBlocked = false;

    }
}
