using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public GameObject Buildingattached;
  public bool isbuildingon;
   public bool adjacenttobuilding;
    public int numadjacent = 0;
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
