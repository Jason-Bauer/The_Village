using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public Camera MainCam;
    public bool isright;
	// Use this for initialization
	void Start () {
		
	}
    void OnMouseOver()
    {
        if (!isright)
        {
            MainCam.GetComponent<CameraMove>().MoveLeft();
        }
        if (isright)
        {
            MainCam.GetComponent<CameraMove>().MoveRight();
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
