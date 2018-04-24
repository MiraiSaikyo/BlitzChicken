using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitter : StateMachineBehaviour {
    public string objectPath;
    public string childPath;
    public GameObject hitObject;
    GameObject hitOffset;
    MonoBehaviour mb;
    public bool isParent = true;
    public float execute_Time;
    public float discontinuation_Time;
    bool isExecute=false;
    [SerializeField]
    bool isInstantiate=true;

    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        hitOffset = GameObject.FindWithTag(objectPath);
       isExecute = false;
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	if(execute_Time<stateInfo.normalizedTime&&!isExecute)
        {
            isExecute = true;
            
            
            
            if(isInstantiate)
            {
                attackInstantiate(hitObject);
            }
            else
            {
                OnAttackActive(hitOffset);
            }
            
        }
    if((discontinuation_Time<stateInfo.normalizedTime)&&isExecute&&!isInstantiate)
    {
        
        OffAttackActive(hitOffset);
    }

	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       isExecute = false;
       //Destroy(item);
       if(!isInstantiate)
       {
        OffAttackActive(hitOffset);
       }

        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}


    protected void  attackInstantiate(GameObject hit)
    {

        hit = Instantiate(hitObject, hitOffset.transform.position, hitOffset.transform.rotation) as GameObject;
        if (isParent)
        {
            hit.transform.parent = hitOffset.transform;
        }
    }

    void OnAttackActive(GameObject hit)
    {
        hit.transform.Find(childPath).gameObject.SetActive(true);
    }
    void OffAttackActive(GameObject hit)
    {
    hit.transform.Find(childPath).gameObject.SetActive(false);
    }

}
