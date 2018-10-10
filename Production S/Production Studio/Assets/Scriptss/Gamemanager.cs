using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamemanager : MonoBehaviour {
    public float Resources;
    public float ResourceGain;
    public int Villagers;
    public int Soldiers;
    public int TechLvl;
    public Text RText, VText, SText, TText;
    public bool ispaused = false;
	// Use this for initialization
	void Start () {
        Resources = 0;
        Villagers = 10;
        Soldiers = 0;
        TechLvl = 1;
	}

    // Update is called once per frame
    void Update()
    {
        if (!ispaused)
        {
            ResourceGain = ((Villagers / 2) + TechLvl) * Time.deltaTime / 2;
            Resources += ResourceGain;
            RText.text = "Resources: " + Resources.ToString("F0");
            VText.text = "Villagers: " + Villagers;
            SText.text = "Soldier: " + Soldiers;
            TText.text = "Tech Lvl: " + TechLvl;
        }
    }
    public void pausegame()
    {
        if (ispaused)
        {
            ispaused = false;
        }
        else
        {
            ispaused = true;
        }
    }
}
