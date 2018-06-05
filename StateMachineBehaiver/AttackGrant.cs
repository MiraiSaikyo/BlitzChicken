
/// <summary>
@file   AttackGrant.cs
@brief  プレイヤーのアニメーターに合わせてオブジェクトを生成
@author 齊藤未来
@detalize StateMachineBehaviourを継承しているので注意
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGrant : StateMachineBehaviour {


    [System.Serializable]
    public struct AttackPara
    {
        public bool isAttackColl;       // 当たり判定があるか
        public string objectPath;       // Objectの名前　名前で取得するので被ってると詰む
        public GameObject hitObject;    // 生成するオブジェクト
        [System.NonSerialized]
        public Transform hitOffset;     //Startで初期化
        public bool isParent;           // 親子付けするか
        public bool isIdentity;　       // 生成する向き
        [SerializeField, Range(0, 1)]
        public float execute_Time;      // 生成されるタイミング
        [System.NonSerialized]
        public bool isExecute;          //Startで初期化
        [System.NonSerialized]
        public GameObject item;         // 生成されたオブジェクトを一時保存
    }

    public AttackPara []attackPara;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

        // 初期化
        for (int i=0;i<attackPara.Length;i++)
        {
            attackPara[i].hitOffset = GameObject.FindWithTag(attackPara[i].objectPath).transform;
            attackPara[i].isExecute = false;
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // 設定した時間になると一度だけ生成
        for (int i = 0; i < attackPara.Length; i++)
        {
            if (attackPara[i].execute_Time < stateInfo.normalizedTime && !attackPara[i].isExecute)
            {
                attackPara[i].isExecute = true;
                attackInstantiate(attackPara[i].hitObject, attackPara[i].hitOffset, attackPara[i].isParent,attackPara[i].isIdentity, attackPara[i].item);
            }
        }

    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        // 生成したオブジェクトを削除
        for (int i = 0; i < attackPara.Length; i++)
        {
            attackPara[i].isExecute = false;
            Destroy(attackPara[i].item);

        }
    }

    // 生成
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
