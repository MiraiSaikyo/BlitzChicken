using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GUIManager : MonoBehaviour {

    public TextMeshProUGUI text;

    // Use this for initialization
    void Start () {
        //text=GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        if(gameObject==EventSystem.current.currentSelectedGameObject)
        {
            text.color = Color.black;
            {
                
            }
        }
        else
        {
            text.color = Color.white;
        }

    }
}
