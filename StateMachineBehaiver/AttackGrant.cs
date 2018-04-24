using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGrant : StateMachineBehaviour {


    [System.Serializable]
    public struct AttackPara
    {
        public bool isAttackColl;
        public string objectPath;
        public GameObject hitObject;
        [System.NonSerialized]
        public Transform hitOffset;//Startで初期化
        public bool isParent;
        public bool isIdentity;
        [SerializeField, Range(0, 1)]
        public float execute_Time;
        [System.NonSerialized]
        public bool isExecute;    //Startで初期化
        [System.NonSerialized]
        public GameObject item;
    }

    public AttackPara []attackPara;


    


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        for (int i=0;i<attackPara.Length;i++)
        {
            attackPara[i].hitOffset = GameObject.FindWithTag(attackPara[i].objectPath).transform;
            attackPara[i].isExecute = false;

        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        for (int i = 0; i < attackPara.Length; i++)
        {
            if (attackPara[i].execute_Time < stateInfo.normalizedTime && !attackPara[i].isExecute)
            {
                attackPara[i].isExecute = true;
                attackInstantiate(attackPara[i].hitObject, attackPara[i].hitOffset, attackPara[i].isParent,attackPara[i].isIdentity, attackPara[i].item);
            }
        }

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        for (int i = 0; i < attackPara.Length; i++)
        {
            attackPara[i].isExecute = false;
            Destroy(attackPara[i].item);

        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    protected void attackInstantiate(GameObject hitObject,Transform hitOffset,bool isParent,bool isIdentity, GameObject item)
    {

        if (isParent)
        {
            item = Instantiate(hitObject, hitOffset.position, hitOffset.rotation) as GameObject;
            item.transform.parent = hitOffset;
        }
        else if(isIdentity)
        {
            Instantiate(hitObject, hitOffset.position,Quaternion.identity);
        }
        else
        {
            Instantiate(hitObject, hitOffset.position, hitOffset.rotation);
        }
    }
}
