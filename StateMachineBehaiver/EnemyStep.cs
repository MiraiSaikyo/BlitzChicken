using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStep : StateMachineBehaviour {

    NavMeshAgent agent;
    Transform target;
    Vector3 targetPosition;
    public float limitTime;
    float time;
    public float speed;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        time = 0;
        agent = animator.gameObject.GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = new Vector3(target.position.x, 0, target.position.z);
        //agent.enabled = false;
        //agent.updateRotation = false;
        
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //if(Vector3.Distance(animator.gameObject.transform.position,agent.destination)<0.1f)
        if (time >= limitTime)
        {
            animator.SetBool("Run", false);
            agent.destination = animator.gameObject.transform.position;
            agent.velocity = Vector3.zero;
        }
        else
        {
            time += Time.deltaTime;
            //animator.gameObject.transform.position += animator.gameObject.transform.forward * speed*Time.deltaTime;

            Vector3 param = animator.gameObject.transform.forward * speed * Time.deltaTime;
            agent.destination += param;
            //Debug.Log("移動値:" + param);
        }


	}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        time = 0;
        agent.destination = animator.gameObject.transform.position;
        agent.velocity = Vector3.zero;

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
