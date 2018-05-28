
/// <summary>
@file   CollDestroy.cs
@brief  指定した名前のTagに接触したらオブジェクトを消す
@author 齊藤未来
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollDestroy : MonoBehaviour {
    public string tag;

// tagにぶつかったらDestroy
void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == tag)
        {
            Destroy(gameObject);
        }
    }
}
