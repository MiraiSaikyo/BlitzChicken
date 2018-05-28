/// <summary>
@file   EnemyState.cs
@brief  敵を管理する処理　このスクリプトをアタッチする
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {


    public int Enemy_Max_Life; //HPの最大値(初期値)
    public int Enemy_Life; // HPの現在値
    NavMeshAgent agent;
    bool isDeath=false;
    bool isStun;
    
    public List<Enemy_Parts> enemy_Parts;
    public List<EnemyAttack> enemyAttack;
    // Use this for initialization
    void Start () {

        Enemy_Life=Enemy_Max_Life; // 現在値に初期値を代入
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        agent.updateRotation=false;
        // HPが0以下だったら倒れたアニメーションを再生
        if(Enemy_Life<=0)
        {
            if (!isDeath)
            {
                isDeath = true;
                GetComponent<Animator>().SetTrigger("Death");
            }
        }
    }
}
