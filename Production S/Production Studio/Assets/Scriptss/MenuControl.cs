using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {
   public GameObject InfluenceMenu;
    public GameObject gather, war, tech;
    public bool isgather, iswar, istech;
	// Use this for initialization
	void Start () {
        InfluenceMenu.SetActive(false);
        isgather = false;
        iswar = false;
        istech =false;
        war.GetComponent<Image>().color = Color.red;
        gather.GetComponent<Image>().color = Color.red;
        tech.GetComponent<Image>().color = Color.red;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenDropdown()
    {
        if (InfluenceMenu.activeSelf)
        {
            InfluenceMenu.SetActive(false);
        }
        else
        {
            InfluenceMenu.SetActive(true);
        }
    }
    public void flipgather()
    {
        if (isgather)
        {
            isgather = false;
            gather.GetComponent<Image>().color = Color.red;
        }
        else if (!isgather)
        {
            isgather = true;
            gather.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("how?");
        }

    }
    public void flipwar()
    {
        if (iswar)
        {
            iswar = false;
            war.GetComponent<Image>().color = Color.red;
        }
        else if (!iswar)
        {
            iswar=true;
            war.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("hoWWWWW?");
        }

    }
    public void fliptech()
    {
        if (istech)
        {
            istech = false;
            tech.GetComponent<Image>().color = Color.red;
        }
        else if (!istech)
        {
            istech = true;
            tech.GetComponent<Image>().color = Color.green;
        }
        else
        {
            Debug.Log("No really. how?");
        }

    }
}
