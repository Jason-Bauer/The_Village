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
    public int eventtrigger;
    public float timer;
	// Use this for initialization
	void Start () {
        Resources = 0;
        Villagers = 10;
        Soldiers = 0;
        TechLvl = 1;
        eventtrigger = 100;
        timer = 10;
	}

    // Update is called once per frame
    void Update()
    {
        if (!ispaused)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                timer = 0;
                if (Villagers > eventtrigger)
                {
                    int rand = Random.Range(0, Villagers);
                    if (rand > eventtrigger)
                    {
                        triggerEvent();
                    }
                }
            }
            eventtrigger = (int)(((TechLvl + .5) * 10)*.8);
            if (Villagers > (TechLvl+.5) * 10)
            {
                Villagers = (int)((double)(TechLvl+.5) * 10);
            }
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
    public void triggerEvent()
    {
        Debug.Log("Event Triggered");
    }
}
