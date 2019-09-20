using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public GameController gameController;

    public GameController.PieceType type;

    public Sprite[] trashSprites;
    public Sprite seedSprite;
    public Sprite sproutSprite;
    public Sprite budSprite;
    public Sprite flowerSprite;

    private SpriteRenderer spriteRenderer;

    void Awake(){
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void SetType(GameController.PieceType type){
        this.type = type;

        if(type == GameController.PieceType.Trash){
            int spriteNum = (int)Random.Range(0.0f, 3.9f);
            spriteRenderer.sprite = trashSprites[spriteNum];

        }else if (type == GameController.PieceType.Seed){
            spriteRenderer.sprite = seedSprite;

        }else if (type == GameController.PieceType.Sprout){
            spriteRenderer.sprite = sproutSprite;

        }else if (type == GameController.PieceType.Bud){
            spriteRenderer.sprite = budSprite;

        }else if (type == GameController.PieceType.Flower){
            spriteRenderer.sprite = flowerSprite;

        }
    }

    public GameController.PieceType GetType(){
        return type;
    }
    
}
