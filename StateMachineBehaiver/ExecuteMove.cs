
/// <summary>
@file   ExecuteMove.cs
@brief  アニメーションに合わせて移動させる
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteMove : StateMachineBehaviour {

    [SerializeField]
    protected float execute_time;
    private bool execute_flag;
    Rigidbody rb;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        execute_flag = false;
        rb = animator.gameObject.GetComponent<Rigidbody>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        if(execute_time<stateInfo.normalizedTime&&!execute_flag)
        {
            execute_flag = true;
            rb.AddForce(animator.gameObject.transform.forward * 200);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        execute_flag = false;
        rb.velocity = Vector3.zero;
    }
}
