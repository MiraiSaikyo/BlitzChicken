using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TMPText : MonoBehaviour {


    public FormationPosition fp;

    int value;
    TextMeshProUGUI tmp;

    // Use this for initialization
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
	// Update is called once per frame
	void Update () {
        value = fp.chickenLength;
        tmp.text = value.ToString(); 
    }
}
