using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCameraShaker : StateMachineBehaviour {

    Camera2 camera;

    public float setShakeTime;
    public float setShakeRange;

    bool isShake;

    public float executeTime;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        camera = GameObject.FindGameObjectWithTag("CameraParent").GetComponent<Camera2>();
        isShake = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (executeTime <= stateInfo.normalizedTime&&(!isShake))
        {
            isShake = true;
            camera.setShake(setShakeTime, setShakeRange);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isShake = false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
