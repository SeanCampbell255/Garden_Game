using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;

    public bool canGrab;
    public bool canPlace;

    //Private Variables
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private int playerPosition = 3;
    private bool moveRight;
    private bool moveLeft;
    private bool grab;
    private bool place;
    private bool isWalking;

    //Instantiation
    private void Start(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        animator.speed = 0;
    }

    void Update(){
        moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.RightArrow);
        grab = Input.GetKeyDown(KeyCode.DownArrow);
        place = Input.GetKeyDown(KeyCode.UpArrow);

        //Detects a grab or place, makes it so you can't do both same frame
        if (grab && canGrab){
            gameController.Grab(playerPosition);
        }else if (place && canPlace){
            gameController.Place(playerPosition);
        }

        //Detects and applies player movement
        if (moveLeft && playerPosition > 0){
            playerPosition--;
            spriteRenderer.flipX = false;
            if (!isWalking){ 
                StartCoroutine(PlayAnimation("walk", 0.3333f, 1.0f, true));
            }
        }
        else if (moveRight && playerPosition < 6){
            playerPosition++;
            spriteRenderer.flipX = true;
            if (!isWalking){ 
                StartCoroutine(PlayAnimation("walk", 0.3333f, 1.0f, true));
            }
        }
        gameController.UpdatePlayerPosition(playerPosition);
    }

    IEnumerator PlayAnimation(string state, float animTime, float speed, bool isWalk){
        isWalking = true;

        animator.Play(state);
        animator.speed = speed;

        yield return new WaitForSeconds(animTime);
        animator.speed = 0;
        isWalking = false;
    }
}
