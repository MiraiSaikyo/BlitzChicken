using UnityEngine;
using System.Collections;
using Arbor;
using System.Linq;

public class RandomChoice : StateBehaviour {
    int[] weight;


    [System.Serializable]
    public class WeightV
    {
        public int weightValue;
        public StateLink NextScene;
    }
    [SerializeField]
    public WeightV[] weightV;
    // Use this for initialization
    void Start () {
	
	}

	// Use this for awake state
	public override void OnStateAwake() {
	}

	// Use this for enter state
	public override void OnStateBegin() {
        //for (int i = 0; i < weightV.Length; i++)
        //{
        //    weight[i] = weightV[i].weightValue;
        //}
        //weight = new int[] { 50, 50 };

             weight=new int[weightV.Length];
            for(int i=0;i<weightV.Length;i++)
            {
                weight[i]=weightV[i].weightValue;
            }





    }

    // Use this for exit state
    public override void OnStateEnd() {
	}
	
	// Update is called once per frame
	void Update () {
        var index = GetRandomIndex(weight);
        Transition(weightV[index].NextScene);
    }

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
