using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrisObject : MonoBehaviour
{
     
    float lastFall = 0f;
    private stopgame stopGameComponent;




    void Start()
    {
        Button leftButton = GameObject.FindWithTag("left").GetComponent<Button>();
        Button rightButton = GameObject.FindWithTag("right").GetComponent<Button>();
        Button turnButton = GameObject.FindWithTag("turn").GetComponent<Button>();
        leftButton.onClick.AddListener(() =>
        {

            transform.position += new Vector3(-1, 0, 0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }

        });
        rightButton.onClick.AddListener(() =>
        {
            transform.position += new Vector3(1, 0, 0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        });


        turnButton.onClick.AddListener(() =>
        {
            transform.Rotate(new Vector3(0, 0, -90));
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, 90));
            }
        });
    }
    void Update()
    {
 
        
        if (Time.time - lastFall >= 1)
        {
            transform.position += new Vector3(0, -1, 0);
            if (IsValidGridPosition())
            {
                UpdateMatrixGrid();
            }
            else
            {
                transform.position += new Vector3(0, 1, 0);
                MatrixGrid.DeleteWholeRows();
                FindObjectOfType<Spawner>().SpawnRandom();
                stopGameComponent = GetComponent<stopgame>();
                stopGameComponent.newobject = false;
                Destroy(this);
            }
            lastFall = Time.time;
        }
        
    }



    bool IsValidGridPosition()
    {
        foreach (Transform chlid in transform)
        {
            Vector2 v= MatrixGrid.RoundVector(chlid.position);

            if (!MatrixGrid.IsInsideBorder(v))
                return false;
            if (MatrixGrid.grid[(int)v.x, (int)v.y] != null && MatrixGrid.grid[(int)v.x,(int)v.y].parent != transform)
                return false;
          
            
               
        }
        return true;
    }

    void UpdateMatrixGrid()
    {
        for (int y = 0; y<MatrixGrid.column; ++y)
        {
            for(int x = 0; x<MatrixGrid.row; ++x)
            {
                if (MatrixGrid.grid[x,y] != null)
                {
                    if (MatrixGrid.grid[x,y].parent == transform)
                    {
                        MatrixGrid.grid[x, y] = null;
                    }
                }
            }
        }
        foreach (Transform chlid in transform)
        {
            Vector2 v = MatrixGrid.RoundVector(chlid.position);
            MatrixGrid.grid[(int)v.x,(int)v.y] = chlid;
        }
    }
}
