using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {


    public int Enemy_Max_Life;
    public int Enemy_Life;
    NavMeshAgent agent;
    bool isDeath=false;
    bool isStun;
    public List<Enemy_Parts> enemy_Parts;
    public List<EnemyAttack> enemyAttack;
    // Use this for initialization
    void Start () {

        Enemy_Life=Enemy_Max_Life;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        agent.updateRotation=false;
        if(Enemy_Life<=0)
        {
            if (!isDeath)
            {
                isDeath = true;
                GetComponent<Animator>().SetTrigger("Death");
            }
        }

        //Debug.Log("destasfsdfsa:" + agent.destination);
    }


    //public List<Enemy_Parts> parts{get{return enemy_Parts;}set{enemy_Parts=value;}}


    // public void OnAttackCollider(string coll)
    // {
    //     GameObject.FindGameObjectWithTag(coll).active=true
    // }

    // public void OffAttackCollider(GameObject coll)
    // {
    //     coll.active=false;
    // }
}
