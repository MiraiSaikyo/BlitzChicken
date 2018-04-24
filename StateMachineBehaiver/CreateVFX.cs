using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateVFX : StateMachineBehaviour {

    public GameObject vfx;
    public string parent;
    Transform parentObject;
    public bool isParent = true;
    GameObject item;

    public float execute_Time;
    bool isExecute = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        parentObject = GameObject.FindGameObjectWithTag(parent).transform;
        

        isExecute = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (execute_Time < stateInfo.normalizedTime && !isExecute)
        {
            isExecute = true;
            attackInstantiate(item);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        isExecute = false;

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
    protected void attackInstantiate(GameObject hit)
    {

        hit = Instantiate(vfx, parentObject.position,parentObject.rotation) as GameObject;
        if (isParent)
        {
            hit.transform.parent = parentObject;
        }
    }
}
