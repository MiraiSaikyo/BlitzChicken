using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIController : MonoBehaviour {

    public GameObject game;
    public GameObject pause;
    bool isPause;

    bool sendScene;


    CursorLockMode mode;
	// Use this for initialization
	void Start () {
        isPause = false;
        pause.active = false;
        sendScene=false;
    }

    // Update is called once per frame
    void Update () {

        Cursor.lockState = mode;
        if(!sendScene)
        {
        if(Input.GetButtonDown("Cancel"))
        {

            if (isPause)
            {
                isPause = false;
                pause.active = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>().pause=false;
            }
            else
            {
                isPause = true;
                pause.active = true ;
                GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>().pause=true;
            }
        }
        }		
	}


    public void SetPauseActive(bool flag)
    {
          isPause=flag;
          pause.active=flag;
    }
    private void OnGUI()
    {
        GUILayout.BeginVertical();
        if(isPause)
        {
            Cursor.visible=true;
            mode = CursorLockMode.None;
        }
        else
        {
            Cursor.visible=false;
            mode = CursorLockMode.Locked;
        }
        GUILayout.EndVertical();
        
    }
    public void Scene()
    {
        sendScene=true;
    }
    
}
