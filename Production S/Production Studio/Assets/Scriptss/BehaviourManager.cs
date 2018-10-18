using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BehaviourManager : MonoBehaviour
{
	public GameObject vehiclePrototype; //vehicle to instantiate
	public GameObject targetPrototype; //target to instantiate

	public GameObject vehicle;
	public GameObject target;

	public BoundingSphere vechbox;
	public BoundingSphere tarbox;

	//public Terrain terrain; //terrain we are walking in
	//private TerrainGenerator terrainGenerator; //terrain information
	public Vector3 worldSize;
    public List<GameObject> zombies = new List<GameObject>();
    public List<GameObject> players = new List<GameObject>();
    public List<BoundingSphere> zombiessp = new List<BoundingSphere>();
    public List<BoundingSphere> playerssp = new List<BoundingSphere>();
    public int zombiecount;
    public int playercount;
    //world size

    public Material white;
    public Material red;
    public Material blue;
    public Material black;
    public Material green;
    public Material purple;

    // Use this for initialization
    void Start ()
	{
		//is vehicle prototype assigned in editor?
		if(null == vehiclePrototype)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is not assigned.");
			Debug.Break();
		}
		//is target prototype assigned in editor?
		if(null == targetPrototype)
		{
			Debug.Log("Error in " + gameObject.name + 
			          ": VehiclePrototype is not assigned.");
			Debug.Break();
		}
		//is terrain assigned in editor?
		//if(null == terrain)
		//{
		//	Debug.Log("Error in " + gameObject.name + 
		//	          ": Terrain is not assigned.");
		//	Debug.Break();
		//}
		//is the terrain assigned a terraingenerator component?
		//terrainGenerator = terrain.GetComponent<TerrainGenerator>();
		//if(null == terrainGenerator)
		//{
		//	Debug.Log("Error in " + gameObject.name + 
		//	          ": Terrain is required to have a TerrainGenerator script");
		//	Debug.Break();
		//}

		//initialize this world size with the terrain generator world size
		//worldSize = terrainGenerator.worldSize;
        for(int i = 0; i < playercount; i++)
        {
            vehicle = Instantiate(vehiclePrototype);
            vehicle.GetComponent<MovementForces>().townhall = this.GetComponent<TileManager>().Tiles[this.GetComponent<TileManager>().numrows][this.GetComponent<TileManager>().numcolumns].GetComponent<Tile>().Buildingattached;

            players.Add(vehicle);
            vechbox = vehicle.GetComponent<BoundingSphere>();
            vehicle.GetComponent<MovementForces>().townhall = this.GetComponent<TileManager>().Tiles[this.GetComponent<TileManager>().numrows][this.GetComponent<TileManager>().numcolumns].GetComponent<Tile>().Buildingattached;
            playerssp.Add(vechbox);
            RandomizePosition(vehicle);
        }
        for (int i = 0; i < zombiecount; i++)
        {
            target = Instantiate(targetPrototype);
            target.GetComponent<MovementForces>().townhall = this.GetComponent<TileManager>().Tiles[this.GetComponent<TileManager>().numrows][this.GetComponent<TileManager>().numcolumns].GetComponent<Tile>().Buildingattached;

            zombies.Add(target);
            target.GetComponent<MovementForces>().townhall = this.GetComponent<TileManager>().Tiles[this.GetComponent<TileManager>().numrows][this.GetComponent<TileManager>().numcolumns].GetComponent<Tile>().Buildingattached;

            tarbox = target.GetComponent<BoundingSphere>();
            zombiessp.Add(tarbox);
            RandomizePosition(target);
        }
		settargets();
     

	}

    void settargets()
    {
        foreach(GameObject a in players)
        {
            MovementForces mf = a.GetComponent<MovementForces>();
            mf.targets = zombies;
        }
        foreach (GameObject a in zombies)
        {
            MovementForces mf = a.GetComponent<MovementForces>();
            mf.targets = players;
        }
    }
	//calculates the position of the object at random
	void RandomizePosition(GameObject theObject)
	{
		//Set position of target based on the size of the world
		Vector3 position = new Vector3 (Random.Range(-10.0f,10.0f), 0.0f, Random.Range(-10.0f,10.0f));
        //set the height of the object based on the position of the terrain
        position.y = .5f;
		//set the position of target back
		theObject.transform.position = position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (BoundingSphere a in playerssp) {
			foreach (BoundingSphere b in zombiessp) {
				if(b.IsColliding(a)){
					RandomizePosition(zombies[zombiessp.IndexOf(b)]);

				}
                if (a.IsColliding(b))
                {
                    RandomizePosition(players[playerssp.IndexOf(a)]);

                }
            }

		}
		settargets ();
	}


     
}
