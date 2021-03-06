﻿
/// <summary>
@file   TimeDestroy.cs
@brief  生成されてから指定した時間でオブジェクトを消す
@author 齊藤未来
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {

    public float TimeDes;

	void Start () 
    {
        Invoke("Des", TimeDes);
	}
	
    void Des()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyAttack" || other.tag == "Shield")
        {
            Des();
        }
    }
}
