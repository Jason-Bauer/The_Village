using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {

    public int cost;
    public int type;
    public GameObject gamemander;
    public GameObject tile;
	// Use this for initialization
	void Start () {
        gamemander = GameObject.Find("GameManager");
        switch (type)
        {
            case 1:
                gamemander.GetComponent<Gamemanager>().Villagers += 1;
                break;
            case 2:
                gamemander.GetComponent<Gamemanager>().Villagers += 2;
                break;
            case 3:
                gamemander.GetComponent<Gamemanager>().Villagers += 3;
                break;
            case 4:
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                gamemander.GetComponent<Gamemanager>().TechLvl -= 1;
                gamemander.GetComponent<Gamemanager>().Soldiers += 2;
                break;
            case 5:
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                gamemander.GetComponent<Gamemanager>().TechLvl -= 2;
                gamemander.GetComponent<Gamemanager>().Soldiers += 4;
                break;
            case 6:
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                gamemander.GetComponent<Gamemanager>().TechLvl -= 1;
                gamemander.GetComponent<Gamemanager>().Soldiers += 8;
                break;
            case 7:
                gamemander.GetComponent<Gamemanager>().TechLvl += 1;
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                break;
            case 8:
                gamemander.GetComponent<Gamemanager>().TechLvl += 1;
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                break;
            case 9:
                gamemander.GetComponent<Gamemanager>().TechLvl += 1;
                gamemander.GetComponent<Gamemanager>().Villagers -= 2;
                break;
        }
        if (gamemander.GetComponent<Gamemanager>().Villagers <= 0)
        {
            gamemander.GetComponent<Gamemanager>().Villagers = 1;
        }
        if (gamemander.GetComponent<Gamemanager>().TechLvl <= 0)
        {
            gamemander.GetComponent<Gamemanager>().TechLvl = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
