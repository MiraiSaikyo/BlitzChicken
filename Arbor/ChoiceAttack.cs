using UnityEngine;
using System.Collections;
using Arbor;
using System.Linq;

public class ChoiceAttack : StateBehaviour {

   
    [SerializeField]
    Transform target;
   
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


    // Use this for initialization
    void Start () {
    }

    // Use this for awake state
    // public override void OnStateAwake() {
        
	// }

    // Use this for enter state
    public override void OnStateBegin() {
        //weight=new int[0];

        target = GameObject.FindGameObjectWithTag("Player").transform;
        distance = 0;
        Vector3 Apos = transform.position;
        Vector3 Bpos = target.position;
        float dis = Vector3.Distance(Apos, Bpos);

        for(int i=0;i<weightV.Length;i++)
        {
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


        // if (dis <= 4)
        // {
        //     distance = 0;
           
        // //  for(int i=0;i<weightV.Length;i++)
        // //    {
        // //        weight[i]=weightV[distance].weightValue[i];
        // //    }
        //    //return;
        //     weight = new int[] {52,32,10};
        // }
        // else if (dis > 4 && dis <= 6)
        // {
        //     distance = 1;
        //     // for(int i=0;i<weightV.Length;i++)
        //     // {
        //     //    weight[i]=weightV[distance].weightValue[i];
        //     // }
        //    // return;
        //     weight = new int[] { 0, 30,50,10};

        // }
        // else
        // {
        //     distance = 2;

        // //    for(int i=0;i<weightV.Length;i++)
        // //    {
        // //        weight[i]=weightV[distance].weightValue[i];
        // //    }
        //   // return;
        //     weight = new int[] { 0, 5,0,20};
        // }
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
