
/// <summary>
@file   SpawnPoint.cs
@brief  鶏が繁殖するポイント
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {
    public GameObject[] point;
    GameObject player;
   
    public bool isSpown;
    public float countTime;
    public List<GameObject> chickenList=new List<GameObject>();
    public float diffusionPer;
   
   // プレイヤーとの距離を測る　
    public float DistanceMeasure(Transform targetPos)
    {
        Vector3 Apos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 Bpos = new Vector3(targetPos.position.x, 0, targetPos.position.z);
        return Vector3.Distance(Apos, Bpos);
    }
    public void count()
    {
        countTime -= Time.deltaTime;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (chickenList.Count != 0)
        {
            RemoveList(chickenList);
        }
        if ((countTime <= 0))
        {
            if (chickenList.Count <= 5)
            {
                isSpown = false;
            }
        }
        else
        {
            isSpown = true;
            count();
        }
    }

    // 鶏を繁殖
    public void Spawn(int value, GameObject character)
    {
        int pointNumber=Random.Range(0, point.Length-1);
        countTime = 10;

        for (int i = 0; i < value; i++)
        {
            float _x = Random.Range(-diffusionPer,diffusionPer);
            float _z = Random.Range(-diffusionPer, diffusionPer);

            Vector3 diffusion = new Vector3(_x, 0, _z);
            var item =Instantiate(character, this.transform.position+diffusion, Quaternion.identity)as GameObject;
            chickenList.Add(item);
        }
    }
    // 繁殖した鶏を保存する
    void RemoveList(List<GameObject> list)
    {
        for(int i=0;i<list.Count;i++)
        {
            if(list[i]==null)
            {
                list.RemoveAt(i);
            }
            else if (list[i].GetComponent<TargetMove>().number != -1)
            {
                list.RemoveAt(i);
            }

        }
    }



}
