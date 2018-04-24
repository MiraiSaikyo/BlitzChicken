using UnityEngine;
using System.Collections;
using Arbor;

public class AngleTransition : StateBehaviour {
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float angleOffset;
    [SerializeField]
    private StateLink rightScene,leftScene;
    [SerializeField]
    private StateLink FalseScene;


    // Use this for initialization
    void Start () {
        
    }

    // Use this for awake state
    public override void OnStateAwake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Use this for exit state
    public override void OnStateEnd() {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 vec = new Vector3(target.position.x,0,target.position.z) -new Vector3( transform.position.x,0,transform.position.z);
        if (Vector3.Angle(vec.normalized, transform.forward) <= angleOffset)
        {
            Transition(FalseScene);
        }
        else
        {
            if (vec.normalized.x > 0f)
            {
                Transition(rightScene);
            }
            else
            {
                Transition(leftScene);
            }
        }

    }
}
