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
    public float timer,suntimer;
    public int numevents = 2;
    private IEnumerator coroutine, coroutine2;
    public bool cantrigevent = true;
    public bool Tornadomoving = false;
    public GameObject tornadoobj;
    public GameObject tornadoprefab;
    public bool sunrising, sunsetting,towardsmid;
    public GameObject Light;
    public Light mainlight;
    public float R, G, B;
    // Use this for initialization
    void Start () {
        sunrising = true;
        sunsetting = false;
        towardsmid = false;
        mainlight = Light.GetComponent<Light>();
        Resources = 0;
        Villagers = 10;
        Soldiers = 0;
        TechLvl = 1;
        eventtrigger = 100;
        timer = 0;
        suntimer = 0;
        R = 122;
        G = 110;
        B = 60;
       // mainlight.color = new Color(R, G, B);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            triggerEvent();
        }

        if (!ispaused)
        {
            //timer += Time.deltaTime;
            if (suntimer > .25f)
            {
                suntimer = 0;
                if (sunrising)
                {
                    //mainlight.color.
                    mainlight.color=new Color(R, G, B);
                    if (R > 250&&G>170)
                    {
                        sunrising = false;
                        sunsetting = true;
                    }
                    else
                    {
                        R+=1;
                        G += .6f;
                        if (towardsmid)
                        {
                            B += .27f;
                            if (B > 70)
                            {
                                towardsmid = false;
                            }
                        }
                        else
                        {
                            B -= .27f;
                            if (B < 5)
                            {
                                towardsmid = true;
                            }
                        }
                    }
                }
                else if (sunsetting)
                {
                    mainlight.color = new Color(R, G, B);
                    if (R < 15 && G < 15)
                    {
                        sunrising = true;
                        sunsetting = false;
                    }
                    else
                    {
                        R -= 1;
                        G -= .6f;
                        if (towardsmid)
                        {
                            B += .27f;
                            if (B > 70)
                            {
                                towardsmid = false;
                            }
                        }
                        else
                        {
                            B -= .27f;
                            if (B < 5)
                            {
                                towardsmid = true;
                            }
                        }
                    }
                }
                else
                {
                    Debug.Log("How did this happen");
                }
               
            }
            suntimer += Time.deltaTime;

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
                timer += Time.deltaTime / 1.2f;
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
        int eventype = Random.Range(0, numevents) + 1;

        switch (eventype)
        {
            case 1:
             bool townahlltrigger = true;
            GameObject deletetile;
             do {
                    EventText.text = "EARTHQUAKE!";
                    int rand = Random.Range(0, GetComponent<TileManager>().numrows * 2);
            int rand1 = Random.Range(0, GetComponent<TileManager>().numrows * 2);
            deletetile = GetComponent<TileManager>().Tiles[rand][rand1];
            // Debug.Log(rand + "," + rand1);
            if (rand == GetComponent<TileManager>().numrows && rand1 == GetComponent<TileManager>().numrows)
            {
                townahlltrigger = false;
            }
            else
            {
                townahlltrigger = true;
            }
        } while (!deletetile.GetComponent<Tile>().isbuildingon && townahlltrigger);
                 coroutine2 = BuildingDestruction(3.0f, deletetile);
                StartCoroutine(coroutine2);
       
                break;
            case 2:
                EventText.text = "TORNADO!";
                int startx = Random.Range(0, GetComponent<TileManager>().numrows * 2);
                int starty = Random.Range(0, GetComponent<TileManager>().numrows * 2);
                int endx = Random.Range(0, GetComponent<TileManager>().numrows * 2);
                int endy = Random.Range(0, GetComponent<TileManager>().numrows * 2);
                GameObject StartTile = GetComponent<TileManager>().Tiles[startx][starty];
                GameObject EndTile = GetComponent<TileManager>().Tiles[endx][endy];
                Vector3 directionvec = Vector3.Normalize(new Vector3(endx-startx,0,endy-starty));
                tornadoobj = Instantiate(tornadoprefab);
                tornadoobj.transform.position = new Vector3(StartTile.transform.position.x, 1.5f, StartTile.transform.position.z);
                tornadoobj.GetComponent<TornadoThings>().target = EndTile;
                break;
    }
        StartCoroutine(coroutine);
        Debug.Log("Event Triggered");
    }

    private IEnumerator WaitAndChange(float waitTime)
    {
            cantrigevent = false;
            yield return new WaitForSeconds(waitTime);
            cantrigevent = true;
          //  print("WaitAndPrint " + Time.time);
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
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            yield return new WaitForSeconds(.25f);
            deletetile.GetComponent<Tile>().Buildingattached.transform.Translate(new Vector3(0, -.05f, 0));
            Destroy(deletetile.GetComponent<Tile>().Buildingattached);
            deletetile.GetComponent<Tile>().Buildingattached = null;
            deletetile.GetComponent<Tile>().isbuildingon = false;
        }
        Villagers -= 4;

           // Debug.Log("Tried to delete the town hall");

    }

}
