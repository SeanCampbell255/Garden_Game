using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Public Instance Vars
    public Game game;

    public int moveTime;

    //Private Instance Vars
    private int[] coords = { 3, 11 };

    private bool inputBlocked = false;

    private float transformConstant = 4.5f;

    private void Update()
    {
        if (!inputBlocked)
        {
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            if (horInput != 0.0f)
            {
                if(horInput > 0.0f && coords[0] < 6)
                {
                    Move(1);
                } else if (coords[0] > 0)
                {
                    Move(-1);
                }
            } else if (Input.GetAxis("Vertical") != 0.0f)
            {


            }
        }
    }

    //Takes direction on x-axis to move player
    private void Move(int direction)
    {
        inputBlocked = true;

        coords[0] += direction;
        game.AddToMatrix(this.gameObject, coords);
    }

    private IEnumerator AnimateMove()
    {
        //IMPLEMENT: Play animation & update position
        yield return new WaitForSeconds(moveTime);
    }
}
