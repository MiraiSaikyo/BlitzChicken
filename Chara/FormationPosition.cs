﻿
/// <summary>
@file   FormationPosition.cs
@brief  鶏がプレイヤーに付いてくる処理
@author 齊藤未来
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FormationPosition : MonoBehaviour {

    public GameObject targetPos;　
    public float distance;

    public Transform FormationPos;
    public Transform adsPoint;
    GameObject[] chicken;
    public int chickenLength;
    public List<GameObject> list = new List<GameObject>();
    int halfSize;
    public float offset;
    public bool isCluck = false;
    public bool isAngry=false;

    private void Update()
    {
        chickenLength = list.Count;

        //
        //自分の配列番号の座標に移動する
        // プレイヤーがADSモードになると鶏たちの間隔が狭くなる
        if(targetPos.GetComponent<Player_State>().isAds)
        {
            FormationPos.position = Vector3.Lerp(FormationPos.position, 
            adsPoint.position+new Vector3(0,0.3f), 100 * Time.deltaTime);
            FormationPos.LookAt(adsPoint);
            offset = 0.1f;
        }
        else
        {
            FormationPos.position = Vector3.Lerp(FormationPos.position, 
            adsPoint.position+new Vector3(0,0.3f), 100 * Time.deltaTime);
            FormationPos.LookAt(adsPoint);
            offset =0.3f;
        }

        // プレイヤーの移動に少し遅れて追従する
        Vector3 Apos = FormationPos.position;
        Vector3 Bpos = targetPos.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);
        if (dis > distance)
        {
            FormationPos.position = Vector3.Lerp(FormationPos.position, targetPos.transform.position, 10*Time.deltaTime);
        }
        list.Distinct().ToArray();
        
        // 鶏が配列から抜けた時の処理
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i]==null)
            {
                list.RemoveAt(i);
            }

            else if (list[i].GetComponent<TargetMove>().number <= -1)
            {
                list[i].GetComponent<NavMeshAgent>().enabled = false;
                list.RemoveAt(i);
            }
        }

        // 鶏たちに配列番号を指定して配列順に鶏を整列させる
        if (list.Count != 0)
        {
            halfSize=Mathf.RoundToInt(Mathf.Sqrt(list.Count));

            for (int j = 0; 0 <halfSize; j++)
            {
                for (int i = list.Count/halfSize*j; i < list.Count / halfSize * (j+1); i++)
                {
                    if(i==list.Count)
                    { return; }

                    if (list[i].GetComponent<TargetMove>().number >= 0)
                    {
                        if (list[i].GetComponent<TargetMove>().number != i)
                        {
                            list[i].GetComponent<TargetMove>().number = i;
                        }
                        else
                        {
                            list[i].GetComponent<TargetMove>().targetPos = (FormationPos.position
                                + (FormationPos.forward * (i - (list.Count / halfSize * j) - halfSize / 2) * offset)
                                + (FormationPos.right * ((j - halfSize / 2))) * offset);
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider coll)
    {
        // プレイヤーが鶏を呼び集める
        if (isCluck)
        {
            if (coll.gameObject.tag == "Chicken")
            {
                if (coll.gameObject.GetComponent<TargetMove>().number == -1)
                {
                    list.Add(coll.gameObject);
                    coll.gameObject.GetComponent<TargetMove>().number = list.Count-1;
                } 
            }
        }
    }
}

