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
        //Input control
        if (!inputBlocked)
        {
            float horInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");

            if (horInput != 0.0f)
            {
                if(horInput > 0.0f)
                {
                    if (coords[0] < 6)
                    {
                        Move(1);
                    }
                }
                else if (horInput < 0.0f)
                {
                    if(coords[0] == 0)
                    {
                        DumpTrash();
                    }
                    else
                    {
                        Move(-1);
                    }
                    
                }
            }
            else if (vertInput != 0.0f)
            {


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
        AnimateMove(direction);
    }
    
    //Handles movement animation along with position and matrix updates
    private IEnumerator AnimateMove(int direction)
    {
        //IMPLEMENT: Play animation & update position
        //Somehow allow anim to shift
        yield return new WaitForSeconds(moveTime);
    }
}
