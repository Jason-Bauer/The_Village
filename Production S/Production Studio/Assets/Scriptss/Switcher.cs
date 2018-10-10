using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Switcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
    public void Switchscene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Switchsceneback()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // Update is called once per frame
    void Update () {
		
    
	}
}
