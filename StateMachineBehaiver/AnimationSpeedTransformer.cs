using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedTransformer : StateMachineBehaviour {

    public AnimationCurve ac;

    public  string buttonName;
    public  float  waitSpeed=1f;

    public  float  execute_time;
    public  float  discontinuation_time;

    private bool   isCharge;

    Player_State ps;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        ps.speed = 1f;

        isCharge = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if(Input.GetButtonUp(buttonName))
        {
            isCharge = true;
        }

        if ((execute_time <= stateInfo.normalizedTime) && !isCharge)
        {
            if (discontinuation_time > stateInfo.normalizedTime && (Input.GetButton(buttonName)))
            {
                ps.speed = waitSpeed;
            }
            else
            {
                ps.speed = ac.Evaluate(stateInfo.normalizedTime);
            }
        }
        else
        {
                ps.speed = ac.Evaluate(stateInfo.normalizedTime);   
        }
    }

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        ps.speed = 1f;
        isCharge = false;
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
