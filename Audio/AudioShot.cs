
/// <summary>
@file   AudioShot.cs
@brief  音を再生する処理
@author 齊藤未来
/// </summary>
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
	
    // 外部から再生する
    public void OneShot(AudioClip ac)
    {
        audioSource.PlayOneShot(ac);
    }
}
