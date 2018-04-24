using UnityEngine;
using System.Collections;
using Arbor;

public class EnemyAttackChoice : StateBehaviour {


    [SerializeField]
    private StateLink _FirstState;
    [SerializeField]
    private StateLink _SecondState;
    [SerializeField]
    private StateLink _ThirdState;


    // Use this for initialization
    void Start () {
       
	
	}

	// Use this for awake state
	public override void OnStateAwake() {
	}

	// Use this for enter state
	public override void OnStateBegin() {
	}

	// Use this for exit state
	public override void OnStateEnd() {
	}
	
	// Update is called once per frame
	void Update () {

        float rand = Random.Range(0, 3);
        
        if(rand==0)
        {
            Transition(_FirstState);
        }
        else if (rand == 1)
        {
            Transition(_SecondState);
        }
        if (rand == 2)
        {
            Transition(_ThirdState);
        }

    }
}
