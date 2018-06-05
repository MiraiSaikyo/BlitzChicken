
/// <summary>
@file   EnemyRotate.cs
@brief  アニメーションに合わせて敵を回転させる
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

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

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {  
        player=GameObject.FindGameObjectWithTag("Player");
    }

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
}
