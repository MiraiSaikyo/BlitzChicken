using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputTransion : StateMachineBehaviour {


    [System.Serializable]
    public class attackCommond
    {
        public string buttonName;
        public string paraName;
    }

    public attackCommond[] commond=new attackCommond[2];
    [SerializeField, Range(0, 1)]
    public float execute_Time;
    [SerializeField,Range(0,1)]
    public float discontinuation_Time;
    public bool isDiscoutinuation_Infinit;
    float discoutinuation_InfinitTime = Mathf.Infinity;
    

    public float timer;
    bool isAttack;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        isAttack = false;
        timer = 0f;
        for (int i = 0; i < commond.Length; i++)
        {
            animator.SetBool(commond[i].paraName, false);
        }

        if(isDiscoutinuation_Infinit)
        {
            discontinuation_Time = discoutinuation_InfinitTime;
        }

        timer = stateInfo.normalizedTime;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < commond.Length;i++)
        {
            if (execute_Time <= stateInfo.normalizedTime)
            {
                if (Input.GetButtonDown(commond[i].buttonName)&&
                   discontinuation_Time > stateInfo.normalizedTime)
                {
                    if(animator.GetComponent<Player_State>().pause==false)
                    {
                        animator.SetBool(commond[i].paraName, true);
                    }//isAttack = true;
                    //timer = 0f;
                }
            }           
        }

        //if (isAttack)
        //{
        //    timer += Time.deltaTime;
        //    if (timer >= discontinuation_time)
        //    {
        //        timer = 0f;
                
        //        for (int i = 0; i < commond.Length; i++)
        //        {
        //            animator.SetBool(commond[i].paraName, false);
        //        }
        //        isAttack = false;
        //    }
        //}



    }


    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        for (int i = 0; i < commond.Length; i++)
        {
            animator.SetBool(commond[i].paraName, false);
        }
        //isAttack = false;
        //timer = 0f;
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
