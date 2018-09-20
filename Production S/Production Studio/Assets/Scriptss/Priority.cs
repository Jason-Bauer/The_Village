using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priority : MonoBehaviour {

    public Queue<int> PriorityQueue;
    public TileManager Tiles;
    public Gamemanager manager;
    public List<GameObject> possibletiles;
    public GameObject NextTile;
    public List<GameObject> Buildings;
    public List<int> possibleBuildings;
    public GameObject Townhall, Farm, House, Church,Barracks,Tower,Encampment,Cauldron,Factory,Observatory;
    public bool isgather = false;
    public bool iswar = false;
    public bool istech = false;
	// Use this for initialization
	void Start () {
        Tiles = this.GetComponent<TileManager>();
        manager = this.GetComponent<Gamemanager>();
        PriorityQueue = new Queue<int>();
        possibleBuildings = new List<int>();
        possibletiles = new List<GameObject>();
        Buildings = new List<GameObject>();
        Buildings.Add(Townhall);
        Buildings.Add(Farm);
        Buildings.Add(House);
        Buildings.Add(Church);
        Buildings.Add(Barracks);
        Buildings.Add(Tower);
        Buildings.Add(Encampment);
        Buildings.Add(Cauldron);
        Buildings.Add(Factory);
        Buildings.Add(Observatory);
        BuildPriority();

    }
	
    void checkforbuildingcomplete()
    {
        if (PriorityQueue.Count > 0)
        {
            if (Buildings[PriorityQueue.Peek()].GetComponent<Building>().cost < manager.Resources)
            {
                manager.Resources -= Buildings[PriorityQueue.Peek()].GetComponent<Building>().cost;
                Destroy(NextTile.GetComponent<Tile>().Buildingattached);
                NextTile.GetComponent<Tile>().Buildingattached = Instantiate(Buildings[PriorityQueue.Peek()]);
                NextTile.GetComponent<Tile>().Buildingattached.transform.parent = NextTile.transform;
                NextTile.GetComponent<Tile>().Buildingattached.GetComponent<Building>().type = PriorityQueue.Peek();
                NextTile.GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(0, .5f, 0);
                NextTile.GetComponent<Tile>().isbuildingon = true;
                PriorityQueue.Dequeue();
                Tiles.checkAdjacent();
                BuildPriority();
              
            }
        }
    }
	// Update is called once per frame
	void Update ()
    {
        checkforbuildingcomplete();
    }

    public void BuildPriority()
    {


        isgather = this.GetComponent<MenuControl>().isgather;
        iswar = this.GetComponent<MenuControl>().iswar;
        istech = this.GetComponent<MenuControl>().istech;
        possibleBuildings.Clear();
        PriorityQueue.Clear();
        if (isgather)
        {
            possibleBuildings.Add(1);
            possibleBuildings.Add(2);
            possibleBuildings.Add(3);
        }
        if (iswar)
        {
            possibleBuildings.Add(4);
            possibleBuildings.Add(5);
            possibleBuildings.Add(6);
        }
        if (istech)
        {
            possibleBuildings.Add(7);
            possibleBuildings.Add(8);
            possibleBuildings.Add(9);
        }

        for (int i = 0; i < possibleBuildings.Count - 1; i++)
        {
            int rand = Random.Range(0, possibleBuildings.Count);
            PriorityQueue.Enqueue(possibleBuildings[rand]);
        }
        possibletiles.Clear();
        for (int i = 0; i <= Tiles.Tiles.Count-1; i++)
        {
            for (int j = 0; j <= Tiles.Tiles.Count - 1; j++)
            {
                if (!Tiles.Tiles[i][j].GetComponent<Tile>().isbuildingon&& Tiles.Tiles[i][j].GetComponent<Tile>().adjacenttobuilding)
                {
                    possibletiles.Add(Tiles.Tiles[i][j]);
                }
            }
        }

        int rand2 = Random.Range(0, possibletiles.Count - 1);
        NextTile = possibletiles[rand2];

                }
}
