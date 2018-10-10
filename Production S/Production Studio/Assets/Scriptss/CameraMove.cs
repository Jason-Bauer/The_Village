using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{

    public Camera MainCam;
   
    public float panSpeed = .00000002f;
    private Vector3 oldPos;
    private Vector3 panOrigin;
    public bool bDragging = true;
    public bool thing = true;

    public float outerLeft = -10f;
    public float outerRight = 10f;
    public GameObject gamemander;
    // Use this for initialization
    void Start()
    {
        gamemander = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!gamemander.GetComponent<Gamemanager>().ispaused)
        {

            MainCam.transform.LookAt(new Vector3(0, 0, 0));
            if (Input.GetMouseButton(0))
            {
                //Debug.Log("Camera Pam");
                //MousePan();
            }
            else
            {
                MainCam.transform.Translate(Vector3.right * Time.deltaTime);
            }
            MainCam.transform.position = new Vector3(Mathf.Clamp((MainCam.transform.position.x), -9.2f, 9.2f), Mathf.Clamp((MainCam.transform.position.y), -9.2f, 9.2f), Mathf.Clamp((MainCam.transform.position.z), -9.2f, 9.2f));


        }


    }

    public void MoveLeft()
    {
        if (!gamemander.GetComponent<Gamemanager>().ispaused)
        {
            MainCam.transform.Translate(-Vector3.right * Time.deltaTime * 3.5f);
        }
    }
    public void MoveRight()
    {
        if (!gamemander.GetComponent<Gamemanager>().ispaused)
        {

            MainCam.transform.Translate(Vector3.right * Time.deltaTime * 2);
        }
    }
    void MousePan()
    {
        if (Input.GetMouseButtonDown(0)&&thing)
        {
            bDragging = true;
            thing = false;
            oldPos = MainCam.transform.position;
            panOrigin = MainCam.WorldToScreenPoint(Input.mousePosition);                   
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 pos = panOrigin-MainCam.WorldToScreenPoint(Input.mousePosition) ;
            pos.y = 0;
            //pos.z = 0;
            if (pos.x > 0)
            {
                MoveRight();
            }
            else if (pos.x < 0)
            {
                MoveLeft();
            }
           // MainCam.transform.position = new Vector3(Mathf.Clamp((oldPos.x + -pos.x * panSpeed * Time.deltaTime), -9.2f, 9.2f), Mathf.Clamp((oldPos.y + -pos.y * panSpeed * Time.deltaTime), -9.2f, 9.2f), Mathf.Clamp((oldPos.z + -pos.z * panSpeed * Time.deltaTime), -9.2f, 9.2f));
           // MainCam.transform.position = oldPos + -pos * panSpeed*Time.deltaTime;
            MainCam.transform.LookAt(new Vector3(0, 0, 0));
           
        }

        if (Input.GetMouseButtonUp(0))
        {
            bDragging = false;
            thing = true;
            MainCam.transform.LookAt(new Vector3(0, 0, 0));
        }
    }
}
