using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    //Public Variables
    public GameController gameController;
    public SoundController sound;
    public SpriteRenderer spriteRenderer;
    public Animator animator;

    public bool canGrab;
    public bool canPlace;

    //Private Variables
    

    private int playerPosition = 3;
    private bool moveRight;
    private bool moveLeft;
    private bool grab;
    private bool place;
    private bool isWalking;

    //Instantiation
    private void Start(){
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        moveLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        moveRight = Input.GetKeyDown(KeyCode.RightArrow);
        grab = Input.GetKeyDown(KeyCode.DownArrow);
        place = Input.GetKeyDown(KeyCode.UpArrow);

        //Detects a grab or place, makes it so you can't do both same frame
        if (grab && canGrab){
            gameController.Grab(playerPosition);
            StartCoroutine(PlayAnimation("Grabbing", 0.306f));
        }
        else if (place && canPlace){
            gameController.Place(playerPosition);
            StartCoroutine(PlayAnimation("Pushing", 0.556f));
        }

        //Detects and applies player movement
        if (moveLeft && playerPosition > 0){
            playerPosition--;
            spriteRenderer.flipX = false;
            if (!isWalking){ 
                StartCoroutine(PlayAnimation("Walking", 0.167f));
                sound.PlayWalk();
            }
            gameController.UpdatePlayerPosition(playerPosition);
        }
        else if (moveRight && playerPosition < 6){
            playerPosition++;
            spriteRenderer.flipX = true;
            if (!isWalking){ 
                StartCoroutine(PlayAnimation("Walking", 0.167f));
                sound.PlayWalk();
            }
            gameController.UpdatePlayerPosition(playerPosition);
        }
        
    }

    IEnumerator PlayAnimation(string condition, float animTime){
        
        if (condition == "Walking")
        {
            isWalking = true;
        }
        animator.SetBool(condition, true);

        yield return new WaitForSeconds(animTime);

        animator.SetBool(condition, false);
        if (condition == "Walking")
        {
            isWalking = false;
        }
    }
}
