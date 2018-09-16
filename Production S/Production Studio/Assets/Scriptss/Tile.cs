using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public GameObject Buildingattached;
  public bool isbuildingon;
    bool isused;
    bool adjacenttobuilding;
    int numinpriority = 0;
    public int buildingtype;
    public int x;
    public int y;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
