
/// <summary>
@file   RandomChoice.cs
@brief  敵の攻撃をランダムで発動させる
@author 齊藤未来
@details　Arbor上でしか使えないので注意
/// </summary>

using UnityEngine;
using System.Collections;
using Arbor;
using System.Linq;

public class ChoiceAttack : StateBehaviour {

   
    [SerializeField]
    Transform target; // 自分との距離を測るためにプレイヤーの座標を取得
   

    // 重みと遷移先
    int[] weight;
    [System.Serializable]
    public class WeightV
    {
        public int[] weightValue;
        public float distance;
       
    }
    [SerializeField]
    public WeightV[] weightV;

    int distance;
    public StateLink[] NextScene;
    // Use this for enter state
    public override void OnStateBegin() {
        //weight=new int[0];

        // プレイヤーのTransformを取得
        target = GameObject.FindGameObjectWithTag("Player").transform;
        
        distance = 0;
        Vector3 Apos = transform.position;
        Vector3 Bpos = target.position;
        // 
        float dis = Vector3.Distance(Apos, Bpos);


        // プレイヤーとの距離を測って距離毎の重みを代入
        for(int i=0;i<weightV.Length;i++)
        {
            // 最後の要素を判別
             if(i==weightV.Length-1)
             {
                weight=new int[weightV[i].weightValue.Length];
                for(int j=0;j<weightV[i].weightValue.Length;j++)
                {
                    weight[j]=weightV[i].weightValue[j];
                }
                 break;
             }
            else if(dis<=weightV[i].distance)
            {
                weight=new int[weightV[i].weightValue.Length];
                for(int j=0;j<weightV[i].weightValue.Length;j++)
                {
                    weight[j]=weightV[i].weightValue[j];
                }
               // Debug.Log(i+"の重み付け");
                break;
            }
        }
    }

	// Update is called once per frame
	void Update () {
        var index = GetRandomIndex(weight);
        Transition(NextScene[index]);
	}
    //重み付け乱数
    public static int GetRandomIndex(params int[] weightTable)
    {
        var totalWeight = weightTable.Sum();
        var value = Random.Range(1, totalWeight + 1);
        var retIndex = -1;
        for (var i = 0; i < weightTable.Length; ++i)
        {
            if (weightTable[i] >= value)
            {
                retIndex = i;
                break;
            }
            value -= weightTable[i];
        }
        return retIndex;
    }

}
