
/// <summary>
@file   InputTransion.cs
@brief  攻撃の入力
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputTransion : StateMachineBehaviour {

    [System.Serializable]
    public class attackCommond
    {
        public string buttonName;   // 入力するボタン名
        public string paraName;     // フラグを立てるAnimatorのパラメータ名
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

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
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
                    }
                }
            }           
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        for (int i = 0; i < commond.Length; i++)
        {
            animator.SetBool(commond[i].paraName, false);
        }
    }
}
