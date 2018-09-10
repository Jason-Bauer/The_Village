using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour {
   public GameObject InfluenceMenu;
	// Use this for initialization
	void Start () {
        InfluenceMenu.SetActive(false);
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
}
