using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

   public Camera MainCam;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        MainCam.transform.LookAt(new Vector3(0, 0, 0));
    
        MainCam.transform.Translate(Vector3.right * Time.deltaTime);
    }
}
