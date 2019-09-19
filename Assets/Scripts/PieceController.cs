using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public enum PieceType {Trash, Seed, Sprout, Bud, Flower};

    private PieceType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetType(PieceType type){
        this.type = type;
    }
}
