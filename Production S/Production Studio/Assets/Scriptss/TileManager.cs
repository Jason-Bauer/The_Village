using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public List<List<GameObject>> Tiles;
    public int numrows;
    public int numcolumns;
    public GameObject tile;
    public GameObject parent;
    public GameObject townhall;
    float currentX, currentZ;
	// Use this for initialization
	void Start () {
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
        Tiles[4][4].GetComponent<Tile>().Buildingattached = Instantiate(townhall);
        Tiles[4][4].GetComponent<Tile>().Buildingattached.transform.parent = Tiles[4][4].transform;
        Tiles[4][4].GetComponent<Tile>().Buildingattached.transform.position = new Vector3(0, .5f, 0);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
