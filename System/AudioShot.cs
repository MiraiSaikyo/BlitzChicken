using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioShot : MonoBehaviour {

    AudioSource audioSource;
    public AudioClip[] clip;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
	
    public void OneShot(AudioClip ac)
    {
        audioSource.PlayOneShot(ac);
    }
}
