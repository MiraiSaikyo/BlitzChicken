
/// <summary>
@file   EnemyAgentStep.cs
@brief  アニメーションに合わせて敵が移動する
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgentStep : StateMachineBehaviour {

    NavMeshAgent agent;
    Transform target;
	[SerializeField]
    Vector3 targetPosition;
    public float limitTime;
    float time;
    public float speed;
	[SerializeField, Range(0, 1)]
    float execute_time;
    [SerializeField, Range(0, 1)]
    float discontinuation_time;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
        time = 0;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
		agent.updateRotation=false;
        target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) 
    {
	    if (execute_time <= stateInfo.normalizedTime)
        {
            if (discontinuation_time > stateInfo.normalizedTime)
            {
                // 正面方向に移動
		        Vector3 param = animator.gameObject.transform.right * speed * Time.deltaTime;
                agent.destination += param;
			}
			else
			{
                // 移動量を0にする
				agent.destination = animator.gameObject.transform.position;
                agent.velocity = Vector3.zero;
			}
		}
	}

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
		agent.updateRotation=true;
        time = 0;
        agent.destination = animator.gameObject.transform.position;
        agent.velocity = Vector3.zero;
    }
}
