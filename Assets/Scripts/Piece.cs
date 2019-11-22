using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //Public Instance Vars
    public enum Type {trash, seed, sprout, bud, flower};
    public enum Trash {not, cigBox, bottle, paper, can };

    public Game game;
    public SpriteRenderer sprite;
    public Sprite[] trashSprites;
    public Sprite seedSprite;
    public Sprite sproutSprite;
    public Sprite budSprite;
    public Sprite flowerSprite;

    public int[] coords = new int[2];
    public float moveTime = 0.33f;

    //Private Instance Vars
    private Type type;

    

    void Awake()
    {
        game = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();
        sprite = this.GetComponent<SpriteRenderer>();
    }

    public void SetType(Type type, Trash trashType)
    {
        this.type = type;

        switch (type)
        {
            case Type.trash:
                switch (trashType)
                {
                    case Trash.not:
                        int num = (int)Random.Range(0.0f, 3.9f);
                        sprite.sprite = trashSprites[num];
                        break;
                    case Trash.cigBox:
                        sprite.sprite = trashSprites[0];
                        break;
                    case Trash.bottle:
                        sprite.sprite = trashSprites[1];
                        break;
                    case Trash.paper:
                        sprite.sprite = trashSprites[2];
                        break;
                    case Trash.can:
                        sprite.sprite = trashSprites[3];
                        break;
                }
                break;
            case Type.seed:
                sprite.sprite = seedSprite;
                break;
            case Type.sprout:
                sprite.sprite = sproutSprite;
                break;
            case Type.bud:
                sprite.sprite = budSprite;
                break;
            case Type.flower:
                sprite.sprite = flowerSprite;
                break;
        }
    }

    public Type GetType()
    {
        return this.type;
    }

    public void Move(int yChange)
    {
        int vector = coords[1] - yChange;

        game.RemoveFromMatrix(coords);
        coords = new int[] {coords[0], coords[1] + yChange};

        game.AddToMatrix(this.gameObject, coords);

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
