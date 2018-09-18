using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Priority : MonoBehaviour {

    public Queue<int> PriorityQueue;
    public List<GameObject> Buildings;
    public List<int> possibleBuildings;
    public GameObject Townhall, Farm, House, Church,Barracks,Tower,Encampment,Cauldron,Factory,Observatory;
    public bool isgather = false;
    public bool iswar = false;
    public bool istech = false;
	// Use this for initialization
	void Start () {
        PriorityQueue = new Queue<int>();
        possibleBuildings = new List<int>();
        Buildings = new List<GameObject>();
        Buildings.Add(Townhall);
        Buildings.Add(Farm);
        Buildings.Add(House);
        Buildings.Add(Church);

	}
	
	// Update is called once per frame
	void Update ()
    {
        
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

    }
}
