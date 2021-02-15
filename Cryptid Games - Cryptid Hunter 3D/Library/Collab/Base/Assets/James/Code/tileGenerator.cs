using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileGenerator : MonoBehaviour
{
    
    int seed = -1; 

    public GameObject GrassTile, WaterTile, PlateauTile, OilTile;



    //public Tilemap groundMap, abovemap;

    void Start()
    {
        Debug.Log("Into server start");
        if (seed == -1) {
            seed = Random.Range(1, 10000000);
        }
        Debug.Log("Seed generated");
        GenerateWorld();
        Debug.Log("Finished call to GenerateWorld()");
    }

    void GenerateWorld() 
    {
        //@James
        //Might need to change seed range to INT_MAX, however that makes things break horribly right now
        int ColumnLength = 200, RowLength = 200, mult = 15;  //Should mult ever change?
        int x_Start = -ColumnLength/2, y_Start = -RowLength/2;

        //Seed logic needs to be changed

        //@Brandon Terrain Generation
        Debug.Log("Generated Seed is "+seed);
        //first water tile hit by the code is set to the oil pond as a failsafe, using modulus on the seed, a position is chosen and the first water tile
        //hit after that position during generation is assigned as the oil pocket, if no water tile is hit after that, it will be the first water tile.
        int water =0,oil =0,oilx=0,oily=0, oilafter = seed%(ColumnLength*RowLength);
        Debug.Log(oilafter);
        Vector3Int pos;
        for(int i = 0; i < ColumnLength; i+=2)
        {
            for(int j = 0; j < RowLength; j+=2)
            {
                float ycord = (Mathf.PerlinNoise((float)(seed+i)/mult,(float)(seed+j)/mult))*20;
                ycord = Mathf.Round(ycord/2);
                if(ycord < 3)
                {
                    //tiles below 3 are water/oil  
                    if(oil == 0 && (i*ColumnLength+j)>oilafter)
                    {
                        oilx = x_Start +i;
                        oily = y_Start +j;
                        oil = 1;
                    }
                    //marks first water in case there isnt water after seed designated oil coord
                    if(water==0)
                    {
                        water=1;
                        oilx=x_Start+i;
                        oily=y_Start+j;
                    }
                    pos = new Vector3Int(x_Start + i, -1, y_Start + j);
                    //groundMap.SetTile(pos,WaterTile);
                    //WaterTile.transform.position += pos;
                    Instantiate (WaterTile, pos, transform.rotation);
                                    
                }
                else if (ycord >= 3 && ycord <6)
                {
                    //tiles between 3 and 6 are grass
                    pos = new Vector3Int(x_Start + i, 0, y_Start + j);
                    //groundMap.SetTile(pos,GrassTile);
                    //GrassTile.transform.position += pos;
                    Instantiate (GrassTile, pos, transform.rotation);
                    
                }
                else
                {
                    //tiles above 6 are plateaus
                    pos = new Vector3Int(x_Start + i, 3, y_Start + j);
                    //abovemap.SetTile(pos,PlateauTile);
                    //PlateauTile.transform.position += pos;
                    Instantiate (PlateauTile, pos, transform.rotation);
                    pos = new Vector3Int(x_Start + i, 0, y_Start + j);
                    //groundMap.SetTile(pos,GrassTile);
                    //GrassTile.transform.position += pos;
                    //@Brandon this fills in under plateaus with grass so it isnt empty
                    Instantiate (GrassTile, pos, transform.rotation);
                }
            }
        }
        //PropogateOil(oilx,oily);
    }

    //when given a location, will replace entire contiguous chunk of water tiles with oil tiles recursively 
    void PropogateOil(int x, int y)
    {
        
        Vector3Int pos = new Vector3Int(x,y,-1);

        GameObject[] wetTiles = GameObject.FindObjectsOfType(typeof(WaterTile));


        /*
        if(groundMap.GetTile(pos) == WaterTile || groundMap.GetTile(pos) == OilTile)
        {
            Vector3Int pos1 = new Vector3Int(x +1, y,0);
            Vector3Int pos2 = new Vector3Int(x, y +1,0);
            Vector3Int pos3 = new Vector3Int(x -1,y,0);
            Vector3Int pos4 = new Vector3Int(x, y -1,0);
            //sets current tile to oil then recursively calls on any neighboring tiles that are water
            groundMap.SetTile(pos,OilTile);
            if(groundMap.GetTile(pos1)==WaterTile)
            {
                PropogateOil(x+1,y);
            }
            if(groundMap.GetTile(pos2)==WaterTile)
            {
                PropogateOil(x,y+1);
            }
            if(groundMap.GetTile(pos3)==WaterTile)
            {
                PropogateOil(x-1,y);
            }
            if(groundMap.GetTile(pos4)==WaterTile)
            {
                PropogateOil(x,y-1);
            }
            
        }
        */
            
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
