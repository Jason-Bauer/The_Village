using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {
   public GameObject InfluenceMenu, MenuMenu,OptionsMenu;
    public GameObject gather, war, tech;
    public bool isgather, iswar, istech;
	// Use this for initialization
	void Start () {
        InfluenceMenu.SetActive(false);
        MenuMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        isgather = false;
        iswar = false;
        istech =false;
        war.GetComponent<Image>().color = Color.red;
        gather.GetComponent<Image>().color = Color.red;
        tech.GetComponent<Image>().color = Color.red;
        isgather = true;
        gather.GetComponent<Image>().color = Color.green;
    
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void openOptions()
    {
        if (OptionsMenu.activeSelf)
        {
            OptionsMenu.SetActive(false);
        }
        else
        {
            OptionsMenu.SetActive(true);
        }
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
    public void OpenMenu()
    {
        if(MenuMenu.activeSelf)
        {
            MenuMenu.SetActive(false);
            OptionsMenu.SetActive(false);
        }
        else
        {
            MenuMenu.SetActive(true);
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
        this.GetComponent<Priority>().BuildPriority();
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
        this.GetComponent<Priority>().BuildPriority();
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
        this.GetComponent<Priority>().BuildPriority();
    }
}
