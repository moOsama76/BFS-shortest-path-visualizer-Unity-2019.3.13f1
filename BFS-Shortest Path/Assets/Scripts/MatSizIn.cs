using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatSizIn : MonoBehaviour
{
    public int rows, coloumns;
    public Transform cell, row;
    public Transform parent;
    public GameObject visiualPerform;

    // As a 2D project thoose values handle the perspective
    const float deltaX_Cells = 2.5f, deltaY_Cells = -2.5f;
    const float deltaX_Rows = -3.5f, deltaY_Rows = -1.8f;
    
    void Start(){

        float xPos = 0f, yPos = 0f;

        // instantiate cells(Coloumns)
        for(int i = 1; i < coloumns; i++){
            xPos += deltaX_Cells;
            yPos += deltaY_Cells;
            Transform newCell = Instantiate(cell, new Vector2(xPos, yPos), Quaternion.identity);
            newCell.name = "x " +  i.ToString();
            newCell.parent = parent;
        }

        xPos = 0f;
        yPos = 0f;

        // instantiate Rows
        for(int i = 1; i < rows; i++){
            xPos += deltaX_Rows;
            yPos += deltaY_Rows;
            Transform newRow = Instantiate(row, new Vector2(xPos, yPos), Quaternion.identity);
            newRow.name = i.ToString();
        }

        // fix cells' names
        for(int i = 0; i < rows; i++){
            for(int j = 0; j < coloumns; j++){
                string s = string.Concat("x ", j.ToString());
                GameObject getByName = GameObject.Find(s);
                s = s.Remove(0,1);
                string parentName = getByName.GetComponent<CellInf>().parent.name; 
                s = string.Concat(parentName, s);
                getByName.name = s;
            }
        }

        // start visiual perform
        visiualPerform.SetActive(true);
    }
}
