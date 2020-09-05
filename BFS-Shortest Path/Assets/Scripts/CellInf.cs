using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellInf : MonoBehaviour
{
    int randomNum;
    public GameObject parent;
    string myRow, myCol;
    Vector2 coordinates;
    SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        mySprite = gameObject.GetComponent<SpriteRenderer>();
        randomNum = Random.Range(1, 4);
        if(randomNum == 3){
            //mySprite.color = Color.white;
            //mySprite.color = Color.black;
        } else {
            mySprite.color = Color.white;
        }   
    }
}
