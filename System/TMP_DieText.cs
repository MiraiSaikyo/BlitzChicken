using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TMP_DieText : MonoBehaviour {

	public Clock clock;
	TextMeshProUGUI tmp;

	// Use this for initialization
	void Start () {
		tmp=GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
		tmp.text=clock.chicken.ToString();
	}
}
