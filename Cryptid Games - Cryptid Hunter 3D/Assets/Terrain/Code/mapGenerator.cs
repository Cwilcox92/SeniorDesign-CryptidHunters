using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapGenerator : MonoBehaviour
{
    
    //Our inital seed
    int seed = -1; 

    //The Object Prefabs for generation purposes
    public GameObject GrassBlock, WaterBlock, underPlateauBlock, PlateauBlock, OilBlock, ClearBlock;

    void Start()
    {
        //@James
        //When our range is to INT_MAX things break horribly. We should figure out our exact limit.
        if (seed == -1) 
        {
            seed = Random.Range(1, 10000000);
        }
        Debug.Log("Seed generated");

        GenerateWorld();
        Debug.Log("Finished call to GenerateWorld()");
    }

    void GenerateWorld() 
    {
        int ColumnLength = 202, RowLength = 202, mult = 15;  //Should mult ever change?
        int x_Start = -ColumnLength/2, y_Start = -RowLength/2;

        
        Debug.Log("Generated Seed is " + seed);
        //@Brandon
        //first water tile hit by the code is set to the oil pond as a failsafe, using modulus on the seed, a position is chosen and the first water tile
        //hit after that position during generation is assigned as the oil pocket, if no water tile is hit after that, it will be the first water tile.
        int water = 0, oil = 0, oilx = 0, oily = 0, oilafter = seed%(ColumnLength*RowLength);
        Debug.Log(oilafter);

        Vector3 pos;
        Vector3 extraPos;
        for(int i = 0; i <= ColumnLength; i+=2)
        {
            for(int j = 0; j <= RowLength; j+=2)
            {
                float ycord = (Mathf.PerlinNoise((float)(seed+i)/mult,(float)(seed+j)/mult))*20;
                ycord = Mathf.Round(ycord/2);
                
                if(((i == ColumnLength) | (i == 0)) | ((j == RowLength) | (j == 0)))
                {
                    //Creates a wall around the entire playspace
                    pos = new Vector3(x_Start + i, 3, y_Start + j);
                    Instantiate (PlateauBlock, pos, transform.rotation);

                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    Instantiate (underPlateauBlock, pos, transform.rotation);
                }

                else if(ycord < 3)
                {
                    //tiles below 3 are water/oil  
                    if(oil == 0 && (i*ColumnLength+j)>oilafter)
                    {
                        oilx = x_Start +i;
                        oily = y_Start +j;
                        oil  = 1;
                    }
                    //marks first water in case there isnt water after seed designated oil coord
                    if(water==0)
                    {
                        water = 1;
                        oilx  = x_Start+i;
                        oily  = y_Start+j;
                    }

                    pos = new Vector3(x_Start + i, -0.5f, y_Start + j);
                    Instantiate (WaterBlock, pos, transform.rotation);

                    //Instantiate the Clear Border Block
                    extraPos = new Vector3(x_Start + i, 1, y_Start + j);
                    Instantiate (ClearBlock, extraPos, transform.rotation);
                                    
                }
                else if (ycord >= 3 && ycord < 6)
                {
                    //tiles between 3 and 6 are grass
                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    Instantiate (GrassBlock, pos, transform.rotation);
                    
                }
                else
                {
                    //tiles above 6 are plateaus
                    pos = new Vector3(x_Start + i, 3, y_Start + j);
                    Instantiate (PlateauBlock, pos, transform.rotation);

                    //Fills in under plateaus with grass so it isnt empty
                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    Instantiate (underPlateauBlock, pos, transform.rotation);
                }
            }
        }
        PropogateOil(oilx,oily);
    }

    //when given a location, will replace entire contiguous chunk of water tiles with oil tiles recursively 
    void PropogateOil(int x, int y)
    {
        GameObject empty = new GameObject();
        Vector3 pos = new Vector3(x,-0.5f,y);
        Object myObject = empty;
        Object[] objs = Object.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject go in objs) 
        {
            if (go.transform.position == pos) 
            {
                myObject = go;
                break;   
            }
        }

        //Make Sure this is equal to the name of the duplicated Water Blocks
        if(myObject.name == "WaterTemp(Clone)")
        {
            
            //sets current tile to oil then recursively calls on any neighboring tiles that are water
            Instantiate (OilBlock, pos, transform.rotation);
            Destroy(myObject);
            
            PropogateOil(x+2,y);
            PropogateOil(x,y+2);
            PropogateOil(x-2,y);
            PropogateOil(x,y-2);
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
