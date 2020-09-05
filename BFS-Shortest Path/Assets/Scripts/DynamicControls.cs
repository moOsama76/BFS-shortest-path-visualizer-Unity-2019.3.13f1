using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DynamicControls : MonoBehaviour
{

    public GameObject input;
    public GameObject visual;
    public GameObject leftBorder;
    public GameObject rightBorder;
    int rows, coloumns;
    Vector2 mousePos;
    string colorOnClick = "None";
    SpriteRenderer nearstColor;

    // Start is called before the first frame update
    void Start()
    {
        rows = input.GetComponent<MatSizIn>().rows;
        coloumns = input.GetComponent<MatSizIn>().coloumns;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject nearst = getNearst();
        nearstColor = nearst.GetComponent<SpriteRenderer>();
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        
        if(!visual.GetComponent<VisualPerform>().isRunning && mousePos.x > leftBorder.transform.position.x && mousePos.x < rightBorder.transform.position.x){
            if (Input.GetMouseButtonDown(0)){
                if(nearstColor.color == Color.yellow){
                    colorOnClick = "yellow";

                } else if(nearstColor.color == Color.blue){
                    colorOnClick = "blue";

                } else{
                    colorOnClick = "None";
                }

            }

            if(colorOnClick != "yellow" && colorOnClick != "blue"){
                if(Input.GetMouseButton(0)){
                    if(nearstColor.color == Color.white){
                        nearstColor.color = Color.black;
                    }

                } else if(Input.GetMouseButton(1)){
                    if(nearstColor.color == Color.black){
                        nearstColor.color = Color.white;
                    }
                }
            } else {
                if(Input.GetMouseButton(0)){
                    changeStartEndPos();
                }
            }
        }
    }

    GameObject getNearst(){

        GameObject nearst = GameObject.Find("0 0");
        float minDis = 999999f;

        for(int i = 0; i < rows; i++){
            for(int j = 0; j < coloumns; j++){
                GameObject currentCell =  getCurrent(i, j);
                float currentDis = distanceTwoPoints(currentCell.transform.position, mousePos);
                if(currentDis < minDis){
                    minDis = currentDis;
                    nearst = currentCell;
                }
            }
        }
        
        return nearst;
    }

    GameObject getCurrent(int r, int c){
    /*
        This if statment is not logically at all,
        but i was forced to handle it due to a bug in GameObject.Find() function.
        please contact me if you have information about that.

    */
    string s;
    if(r % 11 != 0)
        s = string.Concat(r.ToString(), " ", c.ToString());
    else
        s = string.Concat(r.ToString(), r.ToString(), " ", c.ToString());
        
        GameObject currentCell = GameObject.Find(s);
        return currentCell;
    }

    float distanceTwoPoints(Vector2 point1, Vector2 point2){
        return Mathf.Sqrt( (point1.x-point2.x)*(point1.x-point2.x) + (point1.y-point2.y)*(point1.y-point2.y) );
    }

    void changeStartEndPos(){

        // erase prevoius
        for(int i = 0; i < rows; i++){
            for(int j = 0; j < coloumns; j++){
                GameObject currentCell =  getCurrent(i, j);
                SpriteRenderer currentColor = currentCell.GetComponent<SpriteRenderer>();
                if(currentColor.color == Color.yellow && colorOnClick == "yellow" || currentColor.color == Color.blue && colorOnClick == "blue"){
                    currentColor.color = Color.white;
                }
            }
        }

        // create new
        if(colorOnClick == "yellow" && nearstColor.color != Color.blue){
            nearstColor.color = Color.yellow;

        } else if(colorOnClick == "blue" && nearstColor.color != Color.yellow){
            nearstColor.color = Color.blue;
        }

        // change real start/end values
        for(int i = 0; i < rows; i++){
            for(int j = 0; j < coloumns; j++){
                GameObject currentCell =  getCurrent(i, j);
                SpriteRenderer currentColor = currentCell.GetComponent<SpriteRenderer>();
                if(currentColor.color == Color.yellow){
                    visual.GetComponent<VisualPerform>().randomR_startCell = i;
                    visual.GetComponent<VisualPerform>().randomC_startCell = j;

                } else if(currentColor.color == Color.blue){
                    visual.GetComponent<VisualPerform>().randomR_endCell = i;
                    visual.GetComponent<VisualPerform>().randomC_endCell = j;
                }
            }
        }
    }

    public void reload(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
