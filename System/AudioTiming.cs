using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTiming : MonoBehaviour {


	public AudioClip audioClip;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}


	void audioOneShot(AudioClip ac)
	{
		//AudioSource audioPlayer = GameObject.Find("VOICE").GetComponent<AudioSource>();
		AudioSource audioPlayer=GetComponent<AudioSource>();
		audioPlayer.PlayOneShot(ac);
	}
}
