
/// <summary>
@file   EnemyAnimationSpeed.cs
@brief  敵のアニメーション速度を操作する
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSpeed : StateMachineBehaviour {
    
    public AnimationCurve ac;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = ac.Evaluate(stateInfo.normalizedTime);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;
    }
}
