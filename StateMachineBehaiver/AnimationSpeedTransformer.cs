
/// <summary>
@file   AnimationSpeedTransformer.cs
@brief  Animationの再生スピードを変化させる
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedTransformer : StateMachineBehaviour {

    public AnimationCurve ac;　// AnimationCurveを用いる事で再生スピードを図で表せる
    public  string buttonName;
    public  float  waitSpeed=1f;
    public  float  execute_time;
    public  float  discontinuation_time;

    Player_State ps;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        // プレイヤーから数値を取得
        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        ps.speed = 1f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {

        if ((execute_time <= stateInfo.normalizedTime))
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

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        ps.speed = 1f;
    }
}
