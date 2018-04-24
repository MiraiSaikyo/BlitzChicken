using UnityEngine;
using System.Collections;
using Arbor;

public class TargetRotate : StateBehaviour {

    [SerializeField]
    private Transform targetObject;

    private Vector3 target;

    [SerializeField]
    private float RotateTime;
    [SerializeField]
    private float angleOffset;
    [SerializeField]
    private StateLink NextScene;

    public string rightName;
    public string leftName;
    public bool isTackle;

    Animator anim;

    public override void OnStateBegin()
    {
        anim = GetComponent<Animator>();
        targetObject = GameObject.FindGameObjectWithTag("Player").transform;
        target = targetObject.position;
    }


    // Use this for initialization
    void Start () {
        
	}

	// Use this for awake state
	public override void OnStateAwake() {
	}

	// Use this for enter state
	

	// Use this for exit state
	public override void OnStateEnd() {
	}

    // Update is called once per frame
    void Update() {
        //target = targetObject.position;


        Vector3 vec = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z);


        if (Vector3.Angle(vec.normalized, transform.forward) <= angleOffset)
        {
            Transition(NextScene);
            anim.SetBool(leftName, false);
            anim.SetBool(rightName, false);
        }
        else
        {
            if (vec.x>=angleOffset)
            {
                anim.SetBool(leftName, false);
                anim.SetBool(rightName, true);


                //Debug.Log("Right");
            }
            else if((vec.x<=angleOffset))
            {
                anim.SetBool(rightName, false);
                anim.SetBool(leftName, true);

                //Debug.Log("Left");
            }
            if(!isTackle)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x,0f,vec.z)), RotateTime * Time.deltaTime);
            }

            //Debug.Log(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x,0f,vec.z)), RotateTime * Time.deltaTime));
        }




    }


    private bool _checkClockwise(float current, float istarget)
    {
        return istarget > current ? !(istarget - current > 180f)
                              : current - istarget > 180f;
    }


}
