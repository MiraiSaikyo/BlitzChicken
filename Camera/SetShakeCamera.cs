using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetShakeCamera : MonoBehaviour {

	Camera2 camera;
	public float time;
	public float range;
	// Use this for initialization
	void Start () {
		camera=GameObject.FindGameObjectWithTag("CameraParent").GetComponent<TargetCamera>();
		camera.setShake(time,range);
	}
	
	
}
