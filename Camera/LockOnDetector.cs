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
    private int value = 0;
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "LockOn")
        {

            if (target == null)
            {
                // LockOnListからリストを取得してロックオンする対象を決める
                target = c.gameObject.GetComponent<LockOnList>().lockParts[value];
                c.gameObject.GetComponent<LockOnList>().value = value;
                //Debug.Log("targetCheck");
            }

            //　ロックオンする対象が変わる
            if (Input.GetButton("R_Button"))
            {
                ++value;
            }
            // valueの数値が配列数より大きくなったら
            if (value >= c.gameObject.GetComponent<LockOnList>().lockParts.Length)
            {
                value = 0;
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
