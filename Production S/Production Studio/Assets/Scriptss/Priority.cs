using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priority : MonoBehaviour {

    public Queue<int> PriorityQueue;
    public TileManager Tiles;
    public Gamemanager manager;
    public List<GameObject> possibletiles,possibletiles2;
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
        possibletiles2 = new List<GameObject>();
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
                NextTile.GetComponent<Tile>().Buildingattached.GetComponent<Building>().tile = NextTile;
                NextTile.GetComponent<Tile>().Buildingattached.transform.localPosition = new Vector3(0, .5f, 0);
                this.GetComponent<BehaviourManager>().makeVillager(NextTile.GetComponent<Tile>().Buildingattached);
                if (NextTile.GetComponent<Tile>().Buildingattached.GetComponent<Building>().type == 4)
                {
                    NextTile.GetComponent<Tile>().Buildingattached.transform.localRotation = Quaternion.Euler(0, Random.Range(-90, 90), 0);
                }
               // NextTile.GetComponent<Tile>().Buildingattached.transform.localRotation = Quaternion.Euler(0, Random.Range(-90, 90), 0);
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
            if(this.GetComponent<Gamemanager>().TechLvl >= 2)
            {
                possibleBuildings.Add(2);
            }
            if (this.GetComponent<Gamemanager>().TechLvl >= 3)
            {
                possibleBuildings.Add(3);
            }
           
        }
        if (iswar)
        {
            possibleBuildings.Add(4);
            if (this.GetComponent<Gamemanager>().Soldiers > 15)
            {
                possibleBuildings.Add(5);
            }
            if (this.GetComponent<Gamemanager>().Soldiers > 35)
            {
                possibleBuildings.Add(6);
            }
        }
        if (istech)
        {
            possibleBuildings.Add(7);
            if (this.GetComponent<Gamemanager>().TechLvl >= 2)
            {
                possibleBuildings.Add(8);
            }
            if (this.GetComponent<Gamemanager>().TechLvl >= 3)
            {
                possibleBuildings.Add(9);
            }
        }

        for (int i = 0; i < possibleBuildings.Count; i++)
        {
            int rand = Random.Range(0, possibleBuildings.Count);
            PriorityQueue.Enqueue(possibleBuildings[rand]);
           // Debug.Log(rand);
        }
        possibletiles.Clear();
        int topadj = 0;
        for (int i = 0; i <= Tiles.Tiles.Count-1; i++)
        {
            for (int j = 0; j <= Tiles.Tiles.Count - 1; j++)
            {
                if (!Tiles.Tiles[i][j].GetComponent<Tile>().isbuildingon&& Tiles.Tiles[i][j].GetComponent<Tile>().adjacenttobuilding&& Tiles.Tiles[i][j].GetComponent<Tile>().numadjacent>=0)
                {
                    for (int po = 0; po < Tiles.Tiles[i][j].GetComponent<Tile>().numadjacent; po++)
                    {
                        possibletiles.Add(Tiles.Tiles[i][j]);
                    }

                    Tiles.Tiles[i][j].GetComponent<Tile>().numadjacent = 0;
                }
                else
                {
                    Tiles.Tiles[i][j].GetComponent<Tile>().numadjacent=0;
                }
            }
        }
      //  Debug.Log(topadj);
        possibletiles2.Clear();
        for (int j = 0; j<possibletiles.Count; j++)
        {
            if (possibletiles[j].GetComponent<Tile>().numadjacent >= topadj)
            {
                //Debug.Log(topadj);
                possibletiles2.Add(possibletiles[j]);
            }
        }
       
        int rand2 = Random.Range(0, possibletiles2.Count - 1);
        NextTile = possibletiles2[rand2];
       // Debug.Log("Built Priority");
                }
}
