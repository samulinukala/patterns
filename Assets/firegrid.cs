using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firegrid : MonoBehaviour
{
    struct Node
    { 
        public bool HasWood; 
        public bool onFire; 
    }
    //2d grid
    Node[,,] grid=new Node[2,8,8];
    int current = 0;
    int next = 1;
    private void Awake()
    {
        for(int x = 0; x < 8; x++)
        {
            for(int y = 0; y < 8; y++)
            {
                grid[current,x,y].HasWood= false;
                grid[current, x, y].onFire = false;
            }
        }
        grid[current, 3,3].HasWood = true;
        grid[current, 3, 4].HasWood = true;
        grid[current, 4, 3].HasWood = true;
        grid[current, 4, 4].HasWood = true;
        grid[current, 3, 5].HasWood = true;
        grid[current, 5, 5].HasWood = true;
        grid[current, 2, 6].HasWood = true;
        grid[current, 4, 6].HasWood = true;
        grid[current, 6, 6].HasWood = true;
        grid[current, 3, 7].HasWood = true;

        grid[current, 3,3].onFire= true;

    }
    private void swapBuffers()
    {
        int temp = current;
        current = next;
        next = temp;
    }
    void spreadFireSim()
    {//grid through
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (grid[current, x, y].HasWood)
                {
                    grid[next,x,y].HasWood=true;
                    for(int i = x - 1; i <= x + 1; i++)
                    {
                        for(int j = y - 1; j <= y + 1; j++)
                        {
                            if(i>=0&&i<8&&j>=0&&j<8) 
                            {
                                if (grid[current, i, j].onFire == true)
                                {
                                    grid[next, x,y].onFire= true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        swapBuffers();
    }
    void printGrid()
    {
        
        for (int y = 0; y < 8; y++)
        {
            string str="";
            for (int x = 0; x < 8; x++)
            {
                if (grid[current, x, y].onFire)
                {
                    str +="[X]" +" ";
                }
                if(grid[current, x, y].HasWood)
                {
                    str += "[I]" + " ";
                }
                else
                {
                    str += "[_]" +" ";
                }
            }
            
            Debug.Log(str);
        
        }
        Debug.Log("");

    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            printGrid();



        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            spreadFireSim();
        }
    }
}
