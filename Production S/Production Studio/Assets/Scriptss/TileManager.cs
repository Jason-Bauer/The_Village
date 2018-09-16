﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<List<GameObject>> Tiles;
    public int numrows;
    public int numcolumns;
    public GameObject tile;
    public GameObject parent;
    public GameObject townhall,Land1,Land2,Land3;
    public List<GameObject> lands;
    float currentX, currentZ;
    
	// Use this for initialization
	void Start () {
        lands = new List<GameObject>();
        lands.Add(Land1);
        lands.Add(Land2);
        lands.Add(Land3);
        Tiles = new List<List<GameObject>>();
         currentX = numrows*-1.1f;
        currentZ = numcolumns*-1.1f;
   

        for (int i = 0; i <= numrows*2; i++)
        {
            List<GameObject> spawnrow=new List<GameObject>();
            
            for(int j = 0; j <= numcolumns*2; j++)
            {
                GameObject spawntile = Instantiate(tile);
                spawntile.transform.position = new Vector3(currentX, 0, currentZ);
                spawntile.transform.parent = parent.transform;
                spawntile.GetComponent<Tile>().x = j;
                spawntile.GetComponent<Tile>().y = i;
                spawnrow.Add(spawntile);

                currentX += 1.1f;
                
            }
            currentZ += 1.1f;
            currentX=numrows * -1.1f;
            Tiles.Add(spawnrow);

        }


        for (int i = 0; i <= numrows * 2; i++)
        {
            for (int j = 0; j <= numcolumns * 2; j++)
            {
                if (Tiles[i][j].GetComponent<Tile>().Buildingattached == null)
                {
                    int centerx = numrows;
                    int centerZ = numcolumns;
                    if (i == centerx && j == centerZ)
                    {
                        Tiles[centerx][centerZ].GetComponent<Tile>().Buildingattached = Instantiate(townhall);
                        Tiles[centerx][centerZ].GetComponent<Tile>().Buildingattached.transform.parent = Tiles[centerx][centerZ].transform;
                        Tiles[centerx][centerZ].GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(0, .5f, 0);
                        Tiles[i][j].GetComponent<Tile>().isbuildingon = true;
                        Tiles[i][j].GetComponent<Tile>().buildingtype = 1;
                    }
                    else
                    {
                        int rand = Random.Range(0, 3);
                        Tiles[i][j].GetComponent<Tile>().Buildingattached = Instantiate(lands[rand]);
                        Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.SetParent(Tiles[i][j].transform);
                        if (rand == 0)
                        {
                            Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(0.5f, 0.5f, 0);
                        }
                        else if (rand == 1)
                        {
                            Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(0, 0, 0);
                            Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.localRotation = Quaternion.Euler(0, Random.Range(-90, 90),0);
                        }
                        else
                        {
                            Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(.35f, Random.Range(-.6f, -.75f), .35f);
                            
                            //Tiles[i][j].GetComponent<Tile>().Buildingattached.transform.localRotation = Quaternion.Euler(-90, 0, Random.Range(-90, 90));

                        }
                        Tiles[i][j].GetComponent<Tile>().isbuildingon = false;
                    }
                }
            }
        }

    }
	
	// Update is called once per frame
	void Update ()
    {
       
    }
}
