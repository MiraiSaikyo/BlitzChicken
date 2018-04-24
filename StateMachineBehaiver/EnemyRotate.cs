using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotate : StateMachineBehaviour {




    GameObject player;
    Vector3 vec;

    public float RotateTime;


    [SerializeField, Range(0, 1)]
    float execute_time;
    [SerializeField, Range(0, 1)]
    float discontinuation_time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  
        player=GameObject.FindGameObjectWithTag("Player");
        //vec=animator.transform.position-player.transform.position;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (execute_time <= stateInfo.normalizedTime)
        {
            if (discontinuation_time > stateInfo.normalizedTime)
            {
                vec = new Vector3(player.transform.position.x, 0, player.transform.position.z) - new Vector3(animator.transform.position.x, 0, animator.transform.position.z);
                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, Quaternion.LookRotation(new Vector3(vec.x,0f,vec.z)), RotateTime * Time.deltaTime);
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
