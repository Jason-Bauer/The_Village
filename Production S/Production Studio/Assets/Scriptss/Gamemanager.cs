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
    public Text RText, VText, SText, TText,EventText;
    public bool ispaused = false;
    public int eventtrigger;
    public float timer;
    private IEnumerator coroutine, coroutine2;
    public bool cantrigevent = true;
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
            //timer += Time.deltaTime;
            if (cantrigevent)
            {
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
                timer += Time.deltaTime/1.2f;
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
        EventText.text = "Event: Event Triggered";
        coroutine = WaitAndChange(3.0f);
        bool townahlltrigger = true;
        GameObject deletetile;
        do{
            int rand = Random.Range(0, GetComponent<TileManager>().numrows * 2);
            int rand1 = Random.Range(0, GetComponent<TileManager>().numrows * 2);
            deletetile = GetComponent<TileManager>().Tiles[rand][rand1];
            Debug.Log(rand + "," + rand1);
            if(rand== GetComponent<TileManager>().numrows&&rand1== GetComponent<TileManager>().numrows)
            {
                townahlltrigger = false;
            }
            else
            {
                townahlltrigger = true;
            }
        }while(!deletetile.GetComponent<Tile>().isbuildingon&&townahlltrigger);
        coroutine2 = BuildingDestruction(3.0f, deletetile);


        StartCoroutine(coroutine2);
        StartCoroutine(coroutine);
        Debug.Log("Event Triggered");
    }

    private IEnumerator WaitAndChange(float waitTime)
    {
            cantrigevent = false;
            yield return new WaitForSeconds(waitTime);
            cantrigevent = true;
            print("WaitAndPrint " + Time.time);
            EventText.text = "Event: ";
        
    }
    private IEnumerator BuildingDestruction(float waitTime, GameObject deletetile)
    {
     

        yield return new WaitForSeconds(waitTime);
        if (deletetile.GetComponent<Tile>().x == 10 && deletetile.GetComponent<Tile>().y == 10)
        {
            Debug.Log("Tried to delete the town hall");
            triggerEvent();
        }
        else
        {
            Destroy(deletetile.GetComponent<Tile>().Buildingattached);
            deletetile.GetComponent<Tile>().Buildingattached = null;
            deletetile.GetComponent<Tile>().isbuildingon = false;
        }
        Villagers -= 4;

           // Debug.Log("Tried to delete the town hall");

    }

}
