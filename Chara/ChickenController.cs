using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenController : MonoBehaviour {


    public SpawnPoint[] spawnArea;
    public GameObject chicken;

    float countTime = 0;
    private List<int> randomList = new List<int>();
    int count = 0;


    private void Update()
    {
        if (countTime <= 0)
        {
            while(true)
            {
                int i = Random.Range(0, spawnArea.Length - 1);
                if(randomList.Contains(i))
                {
                    continue;
                }
                count++;
                randomList.Add(i);
                if(!spawnArea[i].isSpown)
                {
                    spawnArea[i].Spawn(10-spawnArea[i].chickenList.Count,chicken);
                }

                if(count>=3)
                {
                    countTime += 20;
                    count = 0;
                    randomList.Clear();
                    break;
                }
            }

        }
        else
        {
            countTime -= Time.deltaTime;
        }
    }








}
