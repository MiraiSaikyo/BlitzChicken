using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour {
	
	bool flag;

	public string scene;
	public SceneController sceneCon;

	// Use this for initialization
	void Start () {
		flag=false;
	}
	
	// Update is called once per frame
	void Update () {



		if(Input.GetButtonDown("Fire1")&&(!flag))
		{
			flag=true;
			sceneCon.ChangeScene(scene);
		}
	}
}
