
/// <summary>
@file   ParticleStopDestroy.cs
@brief  パーティクルを止めてからオブジェクトを消す
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleStopDestroy : MonoBehaviour {
   
    ParticleSystem Psys;
    public float waitTime = 0;
    float timer=0;
    
	void Start () 
    {
        Psys = GetComponent<ParticleSystem>();
	}
	
	void Update ()
    {
        timer += Time.deltaTime;
        if(timer>=waitTime)
        {
            Psys.Stop();
            if(timer>waitTime+1)
            { 
                Destroy(gameObject);
            }
        }
	}
}
