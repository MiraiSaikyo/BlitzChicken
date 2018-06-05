/// <summary>
@file   LockOnDetector.cs
@brief  敵をロックオンする処理
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnDetector : MonoBehaviour {
    private GameObject target;
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "LockOn")
        {

            if (target == null)
            {
                target = c.gameObject;
            }
        }
    }
    void OnTriggerExit(Collider c)
    {
        {
            // 範囲外だったらロックオンを解除する
            if(c.gameObject)
            {
                target = null;
            }
        }
    }
    public GameObject getTarget()
    {
        return this.target;
    }
}
