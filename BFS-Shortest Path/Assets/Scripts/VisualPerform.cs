using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualPerform : MonoBehaviour
{
    public GameObject manager;

    int rows, coloumns;
    bool [][] visited = new bool [500][];
    int [][] dis = new int [500][];
    public float yellowTime = 1f;
    public float iterationTime = 0.7f;
    float timeToWait = 0.3f;
    public bool isRunning = false;

    public int randomR_startCell;
    public int randomC_startCell;
    public int randomR_endCell;
    public int randomC_endCell;
    
    // Start is called before the first frame update
    void Start()
    {
        rows = manager.GetComponent<MatSizIn>().rows;
        coloumns = manager.GetComponent<MatSizIn>().coloumns;

        for(int i = 0; i < rows; i++){
            visited[i] = new bool[coloumns];
            dis[i] = new int[coloumns];
        }

        // random start cell
        randomR_startCell = Random.Range(0, rows / 2);
        randomC_startCell = Random.Range(0, coloumns);
        SpriteRenderer currentColor = getSR(randomR_startCell, randomC_startCell);
        currentColor.color = Color.yellow;

        // random end cell
        randomR_endCell = Random.Range(rows / 2 + 1, rows);
        randomC_endCell = Random.Range(0, coloumns);
        currentColor = getSR(randomR_endCell, randomC_endCell);
        currentColor.color = Color.blue;

        //Go();
    
    }

    public void Go(){
        isRunning = true;
        Queue<int> rowsBFS = new Queue<int>();
        Queue<int> colsBFS = new Queue<int>();
        rowsBFS.Enqueue(randomR_startCell);
        colsBFS.Enqueue(randomC_startCell);
        int pathVal = shortestPath(rowsBFS, colsBFS);
        dis[randomR_startCell][randomC_startCell] = 0;
        getThePath(randomR_endCell, randomC_endCell, pathVal);
    }

    // ________________________________________________________________________
    int shortestPath(Queue<int> rowsBFS, Queue <int> colsBFS){
        
        // BFS
        while(rowsBFS.Count > 0){


            int rPeek = rowsBFS.Peek();
            int cPeek = colsBFS.Peek();

            

            if(rPeek == randomR_endCell && cPeek == randomC_endCell)
                break;

            SpriteRenderer SP0 = getSR(0, 0);
            SpriteRenderer SP1 = getSR(0, 0);
            SpriteRenderer SP2 = getSR(0, 0);
            SpriteRenderer SP3 = getSR(0, 0);
            SpriteRenderer SP4 = getSR(0, 0);

            SP0 = getSR(rPeek, cPeek);

            if(cPeek+1 < coloumns)
                SP1 = getSR(rPeek, cPeek+1);

            if(cPeek-1 >= 0)
                SP2 = getSR(rPeek, cPeek-1);

            if(rPeek+1 < rows)
                SP3 = getSR(rPeek+1, cPeek);

            if(rPeek-1 >= 0)
                SP4 = getSR(rPeek-1, cPeek);

            float initialTimetoWait = 0.3f;
             
            // Right Cell
            if(cPeek+1 < coloumns && !visited[rPeek][cPeek+1] && SP1.color != Color.black){
                rowsBFS.Enqueue(rPeek);
                colsBFS.Enqueue(cPeek+1);
                visited[rPeek][cPeek+1] = true;
                dis[rPeek][cPeek+1] = dis[rPeek][cPeek]+1;
                StartCoroutine(colorRed(SP1, timeToWait));
                StartCoroutine(colorGray(SP0, timeToWait));
                timeToWait += initialTimetoWait;
            }

            // Left Cell
            if(cPeek-1 >= 0 && !visited[rPeek][cPeek-1] && SP2.color != Color.black){
                rowsBFS.Enqueue(rPeek);
                colsBFS.Enqueue(cPeek-1);
                visited[rPeek][cPeek-1] = true;
                dis[rPeek][cPeek-1] = dis[rPeek][cPeek]+1;
                StartCoroutine(colorRed(SP2, timeToWait));
                StartCoroutine(colorGray(SP0, timeToWait));
                timeToWait += initialTimetoWait;
            }

            // Down Cell
            if(rPeek+1 < rows && !visited[rPeek+1][cPeek] && SP3.color != Color.black){
                rowsBFS.Enqueue(rPeek+1);
                colsBFS.Enqueue(cPeek);
                visited[rPeek+1][cPeek] = true;
                dis[rPeek+1][cPeek] = dis[rPeek][cPeek]+1;
                StartCoroutine(colorRed(SP3, timeToWait));
                StartCoroutine(colorGray(SP0, timeToWait));
                timeToWait += initialTimetoWait;
            }

            // Up Cell
            if(rPeek-1 >= 0 && !visited[rPeek-1][cPeek] && SP4.color != Color.black){
                rowsBFS.Enqueue(rPeek-1);
                colsBFS.Enqueue(cPeek);
                visited[rPeek-1][cPeek] = true;
                dis[rPeek-1][cPeek] = dis[rPeek][cPeek]+1;
                StartCoroutine(colorRed(SP4, timeToWait));
                StartCoroutine(colorGray(SP0, timeToWait));
                timeToWait += initialTimetoWait;
            }

            rowsBFS.Dequeue();
            colsBFS.Dequeue();
        }

        return dis[randomR_endCell][randomC_endCell];
    }

    bool getThePath(int r, int c, int pathVal){
        if(pathVal == 0)
            return true;

        float initialTimetoWait = 0.3f;


        if(c+1 < coloumns && dis[r][c+1] == pathVal-1 && getThePath(r, c+1, pathVal-1)){
            SpriteRenderer currentSR = getSR(r, c);
            StartCoroutine(colorGreen(currentSR, timeToWait));
            timeToWait += initialTimetoWait;
            return true;

        } else if(c-1 >= 0 && dis[r][c-1] == pathVal-1 && getThePath(r, c-1, pathVal-1)){
            SpriteRenderer currentSR = getSR(r, c);
            StartCoroutine(colorGreen(currentSR, timeToWait));
            timeToWait += initialTimetoWait;
            return true;

        } else if(r+1 < rows && dis[r+1][c] == pathVal-1 && getThePath(r+1, c, pathVal-1)){
            SpriteRenderer currentSR = getSR(r, c);
            StartCoroutine(colorGreen(currentSR, timeToWait));
            timeToWait += initialTimetoWait;
            return true;
            
        } else if(r-1 >= 0 && dis[r-1][c] == pathVal-1 && getThePath(r-1, c, pathVal-1)){
            SpriteRenderer currentSR = getSR(r, c);
            StartCoroutine(colorGreen(currentSR, timeToWait));
            timeToWait += initialTimetoWait;
            return true;
        }

        return false;
    }
    

    SpriteRenderer getSR(int r, int c){
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
        SpriteRenderer currentColor = currentCell.GetComponent<SpriteRenderer>();
        return currentColor;
    }

    
    
    IEnumerator colorGreen(SpriteRenderer currentSprite, float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        if(currentSprite.color != Color.blue)
            currentSprite.color = Color.green;

    }

    IEnumerator colorGray(SpriteRenderer currentSprite, float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        if(currentSprite.color == Color.red)
            currentSprite.color = Color.gray;

    }

    IEnumerator colorRed(SpriteRenderer currentSprite, float timeToWait){
        yield return new WaitForSeconds(timeToWait);
        if(currentSprite.color == Color.white)
            currentSprite.color = Color.red;

    }
}
