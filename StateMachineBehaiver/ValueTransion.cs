using UnityEngine;
using System.Collections;
using Arbor;

public class ValueTransion : StateBehaviour {
	[SerializeField]
	GameObject valueObj;
	
	int initValue;
	int nowValue;	
	[SerializeField]

	StateLink stateLink;
	// Use this for initialization
	void Start () {
	initValue =valueObj.GetComponent<EnemyState>().Enemy_Life;
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
		nowValue=valueObj.GetComponent<EnemyState>().Enemy_Life;
		if(nowValue<=initValue*0.3)
		{
			Transition(stateLink);
		}
	}

		




	
}
