using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class mapGenerator : MonoBehaviour
{
    
    //Our inital seed -1 will generate a random seed
    int seed = -1; 

    //The Object Prefabs for generation purposes
    public GameObject GrassBlock, WaterBlock, underPlateauBlock, PlateauBlock, OilBlock, ClearBlock, Campfire, Tent, Spawnpoint, AISpawnPoint, TreeRes,RockRes,GrassRes;
    public GameObject TreeCollectable,RockCollectable,GrassCollectable;

    public GameObject[,,] WorldArray = new GameObject[102,102,2];
    void Start()
    {
        //@James
        //When our range is to INT_MAX things break horribly. We should figure out our exact limit.
        if (seed == -1) 
        {
            seed = Random.Range(1, 10000000);
        }
        
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
        int campFound = 0, campx=0, campz=0, campAt = (seed/10)%(ColumnLength*RowLength);
        //Debug.Log(oilafter);

        Vector3 pos;
        Vector3 extraPos;
        for(int i = 0; i <= ColumnLength; i+=2)
        {
            for(int j = 0; j <= RowLength; j+=2)
            {
                float ycord = (Mathf.PerlinNoise((float)(seed+i)/mult,(float)(seed+j)/mult))*20;
                ycord = Mathf.Round(ycord/2);
                
                if(campFound==0 && (i*ColumnLength+j)>campAt)
                {
                    campx = x_Start + i;
                    campz = y_Start + j;
                    campFound = 1;
                }

                if(((i == ColumnLength) | (i == 0)) | ((j == RowLength) | (j == 0)))
                {
                    //Creates a wall around the entire playspace
                    pos = new Vector3(x_Start + i, 3, y_Start + j);
                    WorldArray[i/2,j/2,1] = Instantiate (PlateauBlock, pos, transform.rotation);

                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    WorldArray[i/2,j/2,0] = Instantiate (underPlateauBlock, pos, transform.rotation);
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
                    WorldArray[i/2,j/2,0] = Instantiate (WaterBlock, pos, transform.rotation);

                    //Instantiate the Clear Border Block
                    extraPos = new Vector3(x_Start + i, 1, y_Start + j);
                    WorldArray[i/2,j/2,1] = Instantiate (ClearBlock, extraPos, transform.rotation);
                                    
                }
                else if (ycord >= 3 && ycord < 6)
                {
                    //tiles between 3 and 6 are grass
                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    WorldArray[i/2,j/2,0] = Instantiate (GrassBlock, pos, transform.rotation);
                    
                }
                else
                {
                    //tiles above 6 are plateaus
                    pos = new Vector3(x_Start + i, 3, y_Start + j);
                    WorldArray[i/2,j/2,1] = Instantiate (PlateauBlock, pos, transform.rotation);

                    //Fills in under plateaus with grass so it isnt empty
                    pos = new Vector3(x_Start + i, 0, y_Start + j);
                    WorldArray[i/2,j/2,0] = Instantiate (underPlateauBlock, pos, transform.rotation);
                }
            }
        }

        PropogateOil(oilx,oily);
        

        //GameObject[] unconnected = GameObject.FindGameObjectsWithTag("GRASS");  //returns GameObject[]
        //List<GameObject> flagged = new List<GameObject>();
        //List<GameObject> searched = new List<GameObject>();

        PlaceCamp(campx,campz);
        //FixAlcoves(campx, campz, unconnected, flagged,searched);

        //Note if these are called in the wrong order it will not work correctly
        PlaceResources(ColumnLength/2,RowLength/2,5,TreeRes,TreeCollectable);
        //PlaceResources(ColumnLength/2,RowLength/2,4,RockRes,RockCollectable);
        //PlaceResources(ColumnLength/2,RowLength/2,1,GrassRes,GrassCollectable);  

        
    }

    //when given a location, will replace entire contiguous chunk of water tiles with oil tiles recursively 
    void PropogateOil(int x, int y)
    {
        //Make Sure this is equal to the name of the duplicated Water Blocks
        if(WorldArray[(x+101)/2,(y+101)/2,0].name == "WaterTemp(Clone)")
        {
            Vector3 pos = new Vector3(x,-0.5f,y);
            //sets current tile to oil then recursively calls on any neighboring tiles that are water
            Destroy(WorldArray[(x+101)/2,(y+101)/2,0]);
            WorldArray[(x+101)/2,(y+101)/2,0] = Instantiate (OilBlock, pos, transform.rotation);
            
            
            PropogateOil(x+2,y);
            PropogateOil(x,y+2);
            PropogateOil(x-2,y);
            PropogateOil(x,y-2);
        }
        return;
    }

    
    int FixAlcoves(float x, float z,GameObject[] unconnected,List<GameObject> flagged,List<GameObject> searched)
    {
        int p1,p2,p3,p4;
        Vector3 pos = new Vector3(x,0,z);

        //flag initial block
        /*  for possible conversion to using worldarray
        if(WorldArray[(x+101)/2,(y+101)/2,0] != null)
        {
            if(WorldArray[(x+101)/2,(y+101)/2,0].name = "GrassTemp(Clone)")
            {

            }
        }
        */

        for(int i = 0; i < unconnected.Length; i++) 
        {
            if(unconnected[i].name == "GrassTemp(Clone)")
            {
                if (unconnected[i].transform.position == pos)
                {
                    flagged.Add(unconnected[i]);
                    unconnected[i] = null;
                    break;
                }
            }
        }

        //flagged will always be above 0 unless there is no more connected grass

        int rtn = 0;

        while(rtn == 0)
        {
            while(flagged.Count>0)
            {
                //mark current flagged as searched
                searched.Add(flagged[0]);

                //record its position
                float curx = flagged[0].transform.localPosition.x;
                float cury = flagged[0].transform.localPosition.z;
                //unflag current
                flagged.RemoveAt(0);
                //create pos for each adjacent side
                Vector3 pos1 = new Vector3(curx+2,0,cury);
                Vector3 pos2 = new Vector3(curx-2,0,cury);
                Vector3 pos3 = new Vector3(curx,0,cury+2);
                Vector3 pos4 = new Vector3(curx,0,cury-2);


                //move grass to flagged if it exists
                p1 = 0;
                p2 = 0;
                p3 = 0;
                p4 = 0;
                for(int j = 0; j <unconnected.Length;j++)
                {
                    if(unconnected[j] != null)
                    {
                        if(unconnected[j].name == "GrassTemp(Clone)")
                        {
                            if (unconnected[j].transform.position == pos1)
                            {
                                flagged.Add(unconnected[j]);
                                unconnected[j] = null;
                                p1 = 1;
                                if(p1+p2+p3+p4 == 4)
                                {
                                    j = unconnected.Length;
                                }
                            }
                            else if (unconnected[j].transform.position == pos2)
                            {
                                flagged.Add(unconnected[j]);
                                unconnected[j] = null;
                                p2 = 1;
                                if(p1+p2+p3+p4 == 4)
                                {
                                    j = unconnected.Length;
                                }
                            }
                            else if (unconnected[j].transform.position == pos3)
                            {
                                flagged.Add(unconnected[j]);
                                unconnected[j] = null;
                                p3 = 1;
                                if(p1+p2+p3+p4 == 4)
                                {
                                    j = unconnected.Length;
                                }
                            }
                            else if (unconnected[j].transform.position == pos4)
                            {
                                flagged.Add(unconnected[j]);
                                unconnected[j] = null;
                                p4 = 1;
                                if(p1+p2+p3+p4 == 4)
                                {
                                    j = unconnected.Length;
                                }
                            }
                        }
                    }
                }
            }
        
            //check that the starting alcove has water and oil
            rtn = checkStartAlcove(searched);
            if(rtn == 2)
            {
                return 2;
            }
            else
            {
                boreHole(searched[0].transform.localPosition.x,searched[0].transform.localPosition.z);
                flagged = new List<GameObject>(searched);
                searched = new List<GameObject>();
                unconnected = GameObject.FindGameObjectsWithTag("GRASS");
            }
        }
        return 0;
    }
    
    int checkStartAlcove(List<GameObject> connected)
    {
        bool oilFound = false;
        bool waterFound = false;

        GameObject[] ListofAllWaterBlocks= GameObject.FindGameObjectsWithTag("WATER");
        GameObject[] ListofAllOilBlocks = GameObject.FindGameObjectsWithTag("OIL");

        for(int i = 0; i < connected.Count; i++) 
        {
            if(waterFound == false)
            {
                for (int windex = 0; windex < ListofAllWaterBlocks.Length; windex++)
                {
                    if (ListofAllWaterBlocks[windex].transform.position == connected[i].transform.position + new Vector3(2,(float)-0.5,0))
                    {
                    //we are next to a water block

                        waterFound = true;
                        if(oilFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllWaterBlocks[windex].transform.position == connected[i].transform.position + new Vector3(0,(float)-0.5,2))
                    {
                    //we are next to a water block

                        waterFound = true;
                        if(oilFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllWaterBlocks[windex].transform.position == connected[i].transform.position + new Vector3(-2,(float)-0.5,0))
                    {
                    //we are next to a water block

                        waterFound = true;
                        if(oilFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllWaterBlocks[windex].transform.position == connected[i].transform.position + new Vector3(0,(float)-0.5,-2))
                    {
                    //we are next to a water block

                        waterFound = true;
                        if(oilFound == true)
                        {
                            return 2;
                        }

                    }
                }
            }
            
            if(oilFound == false)
            {
                for (int oIndex = 0; oIndex < ListofAllOilBlocks.Length; oIndex++)
                {
                    if (ListofAllOilBlocks[oIndex].transform.position == connected[i].transform.position + new Vector3(2,(float)-0.5,0))
                    {
                    //we are next to a oil block

                        oilFound = true;
                        if(waterFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllOilBlocks[oIndex].transform.position == connected[i].transform.position + new Vector3(0,(float)-0.5,2))
                    {
                    //we are next to a oil block

                        oilFound = true;
                        if(waterFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllOilBlocks[oIndex].transform.position == connected[i].transform.position + new Vector3(-2,(float)-0.5,0))
                    {
                    //we are next to a oil block

                        oilFound = true;
                        if(waterFound == true)
                        {
                            return 2;
                        }

                    }
                    else if (ListofAllOilBlocks[oIndex].transform.position == connected[i].transform.position + new Vector3(0,(float)-0.5,-2))
                    {
                    //we are next to a oil block

                        oilFound = true;
                        if(waterFound == true)
                        {
                            return 2;
                        }

                    }
                }
            }
        }
        return 0;
    }

    void boreHole(float x, float z)
    {
        GameObject empty = new GameObject();
        bool digging = false;
        Object myObject = empty;
        Object[] objs = Object.FindObjectsOfType(typeof(GameObject));


        if(x < 0)
        {
            //left
            if(z < 0)
            {
                //bottom left
                while(true)
                {
                    z += 2;
                    Vector3 pos = new Vector3(x,0,z);
                    Vector3 pos3 = new Vector3(x,-0.5f,z);
                
                    foreach (GameObject go in objs) 
                    {
                        if (go.transform.position == pos) 
                        {
                            myObject = go;
                            break;   
                        }
                        if (go.transform.position == pos3) 
                        {
                            myObject = go;
                            break;   
                        }
                    }


                    if(myObject.name == "WaterTemp(Clone)" || myObject.name ==  "OilTemp(Clone)")
                    {
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);
                        Vector3 pos4 = new Vector3(x,1,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos4) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(myObject.name == "underPlateauTemp(Clone)")
                    {
                        digging = true;
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);

                        Vector3 pos2 = new Vector3(x,3,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos2) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(digging == true & myObject.name == "GrassTemp(Clone)")
                    {
                        return;
                    }
                }            
            }
            else
            {
                //top left
                while(true)
                {
                    x += 2;
                    Vector3 pos = new Vector3(x,0,z);
                    Vector3 pos3 = new Vector3(x,-0.5f,z);

                    foreach (GameObject go in objs) 
                    {
                        if (go.transform.position == pos) 
                        {
                            myObject = go;
                            break;   
                        }
                        if (go.transform.position == pos3) 
                        {
                            myObject = go;
                            break;   
                        }
                    }


                    if(myObject.name == "WaterTemp(Clone)" || myObject.name ==  "OilTemp(Clone)")
                    {
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);
                        Vector3 pos4 = new Vector3(x,1,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos4) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(myObject.name == "underPlateauTemp(Clone)")
                    {
                        digging = true;
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);

                        Vector3 pos2 = new Vector3(x,3,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos2) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(digging == true & myObject.name == "GrassTemp(Clone)")
                    {
                        return;
                    }
                } 
            }
        }
        else
        {
            //right   
            if(z < 0)
            {
                //bottom right
                while(true)
                {
                    x -= 2;
                    Vector3 pos = new Vector3(x,0,z);
                    Vector3 pos3 = new Vector3(x,-0.5f,z);

                    foreach (GameObject go in objs) 
                    {
                        if (go.transform.position == pos) 
                        {
                            myObject = go;
                            break;   
                        }
                        if (go.transform.position == pos3) 
                        {
                            myObject = go;
                            break;   
                        }
                    }


                    if(myObject.name == "WaterTemp(Clone)" || myObject.name ==  "OilTemp(Clone)")
                    {
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);
                        Vector3 pos4 = new Vector3(x,1,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos4) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if (myObject.name == "underPlateauTemp(Clone)")
                    {
                        digging = true;
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);

                        Vector3 pos2 = new Vector3(x,3,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos2) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(digging == true & myObject.name == "GrassTemp(Clone)")
                    {
                        return;
                    }
                } 
            }
            else
            {
                //top right
                while(true)
                {
                    z -= 2;
                    Vector3 pos = new Vector3(x,0,z);
                    Vector3 pos3 = new Vector3(x,-0.5f,z);

                    foreach (GameObject go in objs) 
                    {
                        if (go.transform.position == pos) 
                        {
                            myObject = go;
                            break;   
                        }
                        if (go.transform.position == pos3) 
                        {
                            myObject = go;
                            break;   
                        }
                    }


                    if(myObject.name == "WaterTemp(Clone)" || myObject.name ==  "OilTemp(Clone)")
                    {
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);
                        Vector3 pos4 = new Vector3(x,1,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos4) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(myObject.name == "underPlateauTemp(Clone)")
                    {
                        digging = true;
                        Instantiate (GrassBlock, pos, transform.rotation);
                        Destroy(myObject);

                        Vector3 pos2 = new Vector3(x,3,z);
                        foreach (GameObject go in objs) 
                        {
                            if (go.transform.position == pos2) 
                            {
                                Destroy(go);
                                break;   
                            }
                        }
                    }
                    else if(digging == true & myObject.name == "GrassTemp(Clone)")
                    {
                        return;
                    }
                } 
            }
        }
    }

    int PlaceCamp(int campx, int campz)
    {
        //nudge camp possition if it overlaps with the world border
        if(campx>91)
        {
            campx = 91;
        }
        else if (campx<-91)
        {
            campx = -91;
        }

        if(campz>91)
        {
            campz = 91;
        }
        else if (campz<-91)
        {
            campz = -91;
        }

        Debug.Log("camp at: "+campx+" "+campz);

        //replace blocks at pos with camp 
        //range of z-4 to z+8, x-8 x+8

        Vector3 pos,pos2, aiPos;
        Object[] objs = Object.FindObjectsOfType(typeof(GameObject));
        GameObject empty = new GameObject();
        Object myObject;
        for(int i = campx-8;i<=campx+8;i+=2)
        {
            for(int j = campz-4;j<=campz+8;j+=2)
            {
                if(((i==campx-8 && j == campz+8) || (j==campz-4 && i==campx+8)) || ((i==campx-8 && j==campz-4) || (i==campx+8 && j==campz+8)))
                {
                    //do nothing
                }
                else
                {
                    pos = new Vector3(i,0,j);
                    if(WorldArray[(i+101)/2,(j+101)/2,0].name == "GrassTemp(Clone)")
                    {
                        //do nothing
                    }
                    else if(WorldArray[(i+101)/2,(j+101)/2,0].name == "underPlateauTemp(Clone)")
                    {
                        //replace underplateau with grass
                        Destroy(WorldArray[(i+101)/2,(j+101)/2,0]);
                        WorldArray[(i+101)/2,(j+101)/2,0] = Instantiate (GrassBlock, pos, transform.rotation);                        

                        //remove above plateau
                        Destroy(WorldArray[(i+101)/2,(j+101)/2,1]);
                    }
                    //if none found then it must be a liquid tile tile
                    else
                    {
                        pos = new Vector3(i,-0.5f,j);
                        pos2 = new Vector3(i,1,j);
                        Destroy(WorldArray[(i+101)/2,(j+101)/2,0]);
                        Destroy(WorldArray[(i+101)/2,(j+101)/2,1]);

                        pos = new Vector3(i,0,j);
                        WorldArray[(i+101)/2,(j+101)/2,0] = Instantiate (GrassBlock, pos, transform.rotation);


                    }

                    //check grass pos if underplat, also remove plat
                    //if grass, do nothing
                    //if none check water pos if found remove invis
                }
            }
        }
        int AIx,AIz;
        if(campx>0)
        {
            AIx = -89;
        }
        else
        {
            AIx = 89;
        } 
        if(campz>0)
        {
            AIz = -89;
        }
        else
        {
            AIz = 89;
        } 

        bool loop = true;
        while(loop)
        {
            pos = new Vector3(AIx,3,AIz);
            pos2 = new Vector3(AIx,1,AIz);
            //myObject = empty;

            if(WorldArray[(AIx+101)/2,(AIz+101)/2,1] != null)
            {
                if(AIz>0)
                {
                    AIz = AIz - 2;
                }
                else
                {
                    AIz = AIz + 2;
                }
            }
            else if(AIz == -1)
            {
                loop = false;
            }
            else
            {
                loop = false;
            }
        }

        Debug.Log("AI Spawn at "+AIx+" "+AIz);

        GameObject temp;
        
        //place spawnpoints
        pos = new Vector3(campx - 2, 0.79f, campz+4);
        WorldArray[(campx-2+101)/2,(campz+4+101)/2,1] = Instantiate (Spawnpoint, pos, transform.rotation);
        WorldArray[(campx-2+101)/2,(campz+4+101)/2,1].transform.Rotate(0, 180.0f, 0.0f, Space.Self);

        pos= new Vector3(campx - 6, 0.79f, campz+2);
        WorldArray[(campx-6+101)/2,(campz+2+101)/2,1] = Instantiate (Spawnpoint, pos, transform.rotation);
        WorldArray[(campx-6+101)/2,(campz+2+101)/2,1].transform.Rotate(0, 180.0f, 0.0f, Space.Self);

        pos= new Vector3(campx + 2, 0.79f, campz+4);
        WorldArray[(campx+2+101)/2,(campz+4+101)/2,1] =Instantiate (Spawnpoint, pos, transform.rotation);
        WorldArray[(campx+2+101)/2,(campz+4+101)/2,1].transform.Rotate(0, 180.0f, 0.0f, Space.Self);

        pos= new Vector3(campx + 6, 0.79f, campz+2);
        WorldArray[(campx+6+101)/2,(campz+2+101)/2,1] =Instantiate (Spawnpoint, pos, transform.rotation);
        WorldArray[(campx+6+101)/2,(campz+2+101)/2,1].transform.Rotate(0, 180.0f, 0.0f, Space.Self);

        //AISpawn Point
        pos= new Vector3(AIx, 0.79f, AIz);
        WorldArray[(AIx+101)/2,(AIz+101)/2,1] = Instantiate(AISpawnPoint, pos, transform.rotation);
        WorldArray[(AIx+101)/2,(AIz+101)/2,1].transform.Rotate(0, 180.0f, 0.0f, Space.Self);
        //Tents
        pos = new Vector3(campx-2,1,campz+6);
        WorldArray[(campx-2+101)/2,(campz+6+101)/2,1] = Instantiate (Tent, pos, transform.rotation);

        pos = new Vector3(campx-6,1,campz+4);
        WorldArray[(campx-6+101)/2,(campz+4+101)/2,1] = Instantiate (Tent, pos, transform.rotation);

        pos = new Vector3(campx+2,1,campz+6);
        WorldArray[(campx+2+101)/2,(campz+6+101)/2,1] = Instantiate (Tent, pos, transform.rotation);

        pos = new Vector3(campx+6,1,campz+4);
        WorldArray[(campx+6+101)/2,(campz+4+101)/2,1] = Instantiate (Tent, pos, transform.rotation);
        //campfire
        pos = new Vector3(campx,0.5f,campz);
        WorldArray[(campx+101)/2,(campz+101)/2,1] = Instantiate (Campfire, pos, transform.rotation);


        return 0;
    }

    void PlaceResources(int height, int width, int R, GameObject Resource, int Collectable)
    {
        //create bluenoise

        float[,]bluenoise = new float[height,width];

        for (int y = 0; y < height; y++) 
        {
            for (int x = 0; x < width; x++) 
            {
                //float nx = x/width - 0.5f, ny = y/height - 0.5f;
                // blue noise is high frequency; try varying this
                bluenoise[y,x] = (Mathf.PerlinNoise((float)(seed+y)/2,(float)(seed+x/2))*20);
            }
        }

        //Object[] objs = Object.FindObjectsOfType(typeof(GameObject));
        //GameObject empty = new GameObject();
        //Object myObject;
        Vector3 pos;
        Vector3 pos1;

        for (int yc = 0; yc < height; yc++) 
        {
            for (int xc = 0; xc < width; xc++)
            {
                //find max value in the noicefield
                double max = 0;
                for (int yn = yc - R; yn <= yc + R; yn++) 
                {
                    for (int xn = xc - R; xn <= xc + R; xn++) 
                    {
                        if (0 <= yn && yn < height && 0 <= xn && xn < width) 
                        {
                            double e = bluenoise[yn,xn];
                            if (e > max) 
                            {
                                max = e;
                            }
                        }
                    }
                }
                //place a resource only at the peaks
                if (bluenoise[yc,xc] == max) 
                {
                    pos = new Vector3((yc*2)-101,3,(xc*2)-101);
                    pos1 = new Vector3((yc*2)-101,1,(xc*2)-101);
                    if(WorldArray[(yc*2)/2,(xc*2)/2,1] == null)
                    {
                        WorldArray[(yc*2)/2,(xc*2)/2,1] = Instantiate (Resource, pos, transform.rotation); 

                        if(Collectable != null)
                        {
                            pos = new Vector3((yc2)-101, 1.69f, (xc2)-101); // ask Brandon about how to disperce the other items
                            ItemWorld.SpawnItemWorld(pos, new Item { itemType = Item.ItemType.HealthPotion, amount = 2});
                            Instantiate (Collectable, pos, transform.rotation); 
                        }
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       Object[] objs = Object.FindObjectsOfType(typeof(GameObject));
    }
}