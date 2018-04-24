using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMove : MonoBehaviour {
    public int chicken_Life;
    [System.NonSerialized]
    public Vector3 targetPos;
    public int attackPower=30;

    // 整列用
    [System.NonSerialized]
    public bool isCall = false;
    [System.NonSerialized]
    public int number = -1;
   
    //飛んでいく角度と倍率
    //[System.NonSerialized]
    //public float angle = 50;
    //[System.NonSerialized]
    //public float scalar = 20;

    // 飛ぶ計算用
    float aX = 0f;
    float aY =-9.81f;
    float viX, vfX, viY, vfY, t;
    Transform _transform;

    // random用
    Vector3 diffusion=Vector3.zero;
    public float randx,randz;



    //particle
    public GameObject[] damageEffect;
    public GameObject kickEffect;
    public GameObject stickEffect;
    bool isStick=false;

    //public GameObject model;

    bool isAttack = false;
    bool isInvincible = false;
    bool isDeath = false;


    FormationPosition formationPosition;

    Rigidbody rb;
    Animator anim;
    NavMeshAgent agent;
    AudioShot aShot;

    void Start () {
        formationPosition=GameObject.Find("FormationController").GetComponent<FormationPosition>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        agent= GetComponent<NavMeshAgent>();
        agent.enabled = true;
        aShot = GetComponent<AudioShot>();
       // agent.SetDestination(this.transform.position);
    }
    void Update()
    {
        
        {
            if (!isDeath && !isInvincible && !isStick)
            {
              CallMode();
            }
            else if (isDeath||isStick)
            {

                number = -2;
                isCall = false;
                isAttack = false;
                isStick = false;
                agent.enabled = true;
                GetComponent<TargetMove>().enabled=false;
                GetComponent<CapsuleCollider>().enabled=false;
                agent.enabled=false;
            }
            
            else if (isInvincible)
            {

                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("DamageReaction"))
                {
                    isInvincible = false;
                }

            }
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        // 地面との接触判定
        if (c.gameObject.tag == "floor")
        {
            anim.SetBool("Attack", false);
            anim.SetBool("AngryAttack",false);

            agent.enabled = true;
            //agent.updatePosition = true ;
        }


        if (!agent.enabled)
        {
            if (c.gameObject.tag == "wall")
            {
                anim.SetBool("AngryAttack",false);
                anim.SetBool("Attack", true);
                GameObject.Find("Timer").GetComponent<Clock>().chicken++;
                isStick = true;
                               // GetComponent<TargetMove>().enabled=false;

                number = -2;
                if (stickEffect != null)
                {
                    Instantiate(stickEffect, c.ClosestPoint(transform.position), transform.rotation);
                }
            }
            else if ((number!=-2)&&c.gameObject.tag == "Enemy"&&isAttack)
            {
                Enemy_ChildParts enemy=c.gameObject.GetComponent<Enemy_ChildParts>();
                enemy.OnDamage(attackPower);
                float defence=enemy.enemy_Parts.defensePower;
                for(int i=0;i<damageEffect.Length;i++)
                {
                    if(i==damageEffect.Length-1)
                    {
                    var item=Instantiate(damageEffect[i], transform.position, Quaternion.identity) as GameObject;
                    }

                    else if(defence<=((i+1)*25))
                    {
                        if (!(damageEffect[i] == null))
                        {
                            var item=Instantiate(damageEffect[i], transform.position, Quaternion.identity) as GameObject;
                        }
                        break;
                    }
                }
                isAttack = false;
                //Destroy(gameObject);
            }
        }



    }

    // 放物線で飛ばす
    void parabora()
    {
        t = Time.deltaTime;

        vfX = viX + aX * t;
        float deltaX = 0.5f * (viX + vfX) * t;
        viX = vfX;

        vfY = viY + aY * t;
        float deltaY = 0.5f * (viY + vfY) * t;
        viY = vfY;

        _transform.Translate(new Vector3(0, deltaY, deltaX),Space.Self);
    }

    // ダメージ、死亡、蹴られていない時に通る
    void CallMode()
    {

        // 呼ばれてるとき
        if (isCall&&(agent.enabled))
        {
            //rb.isKinematic = true;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("getHit"))
            {
                agent.SetDestination(targetPos + diffusion);
            }
            anim.SetFloat("Move", agent.velocity.magnitude);
        }
         // 呼ばれていなかった場合呼ばれる 整列用
        if ((number != -1) && (!isCall))
        {
            float _x = Random.Range(-randx, randx);
            float _z = Random.Range(-randz, randz);
            diffusion = new Vector3(_x, 0, _z);

            isCall = true;
            anim.SetTrigger("Call");
        }
        // 一度も呼ばれていない場合
        else if (number == -1)
        {
            isCall = false;
        }

        // agentがfalse
        if (!agent.enabled)
        {
            //rb.isKinematic = false;
            parabora();
            //FallOut();
            anim.SetFloat("Move", 0);

        }
        // agentがtrue
        else
        {
            if(!agent.isOnNavMesh)
            {
                NavMeshHit hit;
                agent.FindClosestEdge(out hit);
                agent.Warp(hit.position);
            }
        }

       

    }
    public void Damage(int power)// ダメージ処理
    {
        if ((!isDeath)&&(!isInvincible)&&(!isAttack))
        {
             chicken_Life-= power;
            if (chicken_Life <= 0)
            {
                GameObject.Find("Timer").GetComponent<Clock>().chicken++;
                isDeath = true;
                anim.SetTrigger("Death");
            }
            else
            {
                isInvincible = true;
                anim.SetTrigger("Wound");
            }
        }
    }

    // これだけ唯一攻撃判定にならない
    public void AttackSwitching(float angle,float scalar)
    {
        agent.enabled = false;

        aShot.OneShot(aShot.clip[0]);
        aShot.OneShot(aShot.clip[1]);
        anim.SetBool("Attack", true);
        // 蹴った時のParticle
        if (kickEffect != null)
        {
            Instantiate(kickEffect, transform.position, transform.rotation);
        }

        // 飛ばす為の初期化
        _transform = transform;
        viY = Mathf.Sin(angle * Mathf.Deg2Rad) * scalar;
        viX = Mathf.Cos(angle * Mathf.Deg2Rad) * scalar;
    }
    public void AttackSwitching(Vector3 cPosition,Vector3 forward,float angle,float scalar)
    {
        agent.enabled = false;
        number = -1;
        formationPosition.list.Remove(gameObject);
        formationPosition.list.Add(gameObject);
        number = formationPosition.list.Count-1;
        isAttack = true;

        transform.position=cPosition;

        aShot.OneShot(aShot.clip[0]);
        aShot.OneShot(aShot.clip[1]);
        anim.SetBool("Attack", true);

        //正面を向かせる
        transform.localRotation = Quaternion.LookRotation(forward);
        //transform.position = initTrans.position;

        // 蹴った時のParticle
        if (kickEffect != null)
        {
            Instantiate(kickEffect, transform.position, transform.rotation);
        }

        // 飛ばす為の初期化
        _transform = transform;
        viY = Mathf.Sin(angle * Mathf.Deg2Rad) * scalar;
        viX = Mathf.Cos(angle * Mathf.Deg2Rad) * scalar;
    }
    public void AttackSwitching(Vector3 cPosition,Vector3 forward,Vector3 vec,float angle,float scalar)
    {

        agent.enabled = false;
        number = -1;
        formationPosition.list.Remove(gameObject);
        formationPosition.list.Add(gameObject);
        number = formationPosition.list.Count-1;
        isAttack = true;

        transform.position=cPosition;

        aShot.OneShot(aShot.clip[0]);
        aShot.OneShot(aShot.clip[1]);
        anim.SetBool("Attack", true);

        //正面を向かせる
        transform.localRotation = Quaternion.LookRotation(forward);
        transform.eulerAngles+=vec;
        //transform.position = initTrans.position;

        // 蹴った時のParticle
        if (kickEffect != null)
        {
            Instantiate(kickEffect, transform.position, transform.rotation);
        }

        // 飛ばす為の初期化
        _transform = transform;
        viY = Mathf.Sin(angle * Mathf.Deg2Rad) * scalar;
        viX = Mathf.Cos(angle * Mathf.Deg2Rad) * scalar;


    }

public void AttackSwitching(Vector3 cPosition,Vector3 forward,Vector3 vec,float angle,float scalar,float time)
    {

        agent.enabled = false;
        number = -1;
        formationPosition.list.Remove(gameObject);
        formationPosition.list.Add(gameObject);
        number = formationPosition.list.Count-1;
        isAttack = true;

        transform.position=cPosition;

        aShot.OneShot(aShot.clip[0]);
        aShot.OneShot(aShot.clip[1]);
        anim.SetBool("Attack", true);

        //正面を向かせる
        transform.localRotation = Quaternion.LookRotation(forward);
        transform.eulerAngles+=vec;
        //transform.position = initTrans.position;

        // 蹴った時のParticle
        if (kickEffect != null)
        {
            Instantiate(kickEffect, transform.position, transform.rotation);
        }

        // 飛ばす為の初期化
        _transform = transform;
        viY = Mathf.Sin(angle * Mathf.Deg2Rad) * scalar;
        viX = Mathf.Cos(angle * Mathf.Deg2Rad) * scalar;


    }

    public void AngryAttack(Vector3 vec,float angle,float scalar)
    {
        agent.enabled = false; 
        anim.SetBool("AngryAttack", true);

        vec-=transform.position;
        transform.localRotation = Quaternion.LookRotation(vec);
        // 飛ばす為の初期化
        _transform = transform;
        isAttack = true;
        viY = Mathf.Sin(angle * Mathf.Deg2Rad) * scalar;
        viX = Mathf.Cos(angle * Mathf.Deg2Rad) * scalar;
    }

   
}

