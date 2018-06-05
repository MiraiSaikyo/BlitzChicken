
/// <summary>
@file   Player_Attack.cs
@brief  プレイヤーの蹴る処理
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody))]
public class PlayerAttack : MonoBehaviour
{
    public int attackPower; // 直接蹴った攻撃力　友達の攻撃力とは関係ない
    // 友達の飛んでいく向き
    public float anglePercent = 1, scalePersent = 1; 
    float angle;
    float scale;

    public float time;
    public float hitStopTime;
    public GameObject kickEffect;
    public GameObject damageEffect;
    Player_State ps;
    public bool isHitStop = false;
    [SerializeField, Range(0f, 30f)]
    public float rand = 0.5f;
    public int length;
    private int collCount = 0;
    public bool isAds;

    int power;

    public enum shotPaturn
    {
        Normal,Diffusion,Upper
    }
    public shotPaturn shot;
    public LayerMask mask;
    public bool isCharge;
    bool flag=false;

    protected void Start()
    {
        angle = anglePercent * 10f;
        scale = scalePersent * 10f;

        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        if ((time != 0)&&(!isCharge))
        {
            StartCoroutine(DestroyHit());
        }
    }
    
    protected virtual IEnumerator DestroyHit()
    {
        switch(shot)
        {
            case shotPaturn.Normal:
            Normal();
            break;
            case shotPaturn.Diffusion:
            Diffusion(-rand);
            break;
            case shotPaturn.Upper:
            Upper();
            break;
        }
        yield return null;
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        // 敵を直接蹴る
        if (c.tag == "Enemy")
        {
            c.GetComponent<Enemy_ChildParts>().OnDamage(attackPower);
            if (!(damageEffect == null))
            {
                ps.isHitStop = true;
                Instantiate(damageEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    // ADS状態の時の蹴り方
    void AdsAttack(Collider hit)
    {
        ps.isHitStop = true;
        if ((collCount >= length)&&isAds)
        {
            return;
        }
        ++collCount;
        if (!(kickEffect == null))
        {
            Instantiate(kickEffect, hit.ClosestPoint(hit.transform.position), Quaternion.identity);
        }
        float _x = Random.Range(-rand, rand);
        Vector3 vec;
        vec = transform.forward * 5 + new Vector3(_x, 0);

        var chicken = hit.GetComponent<TargetMove>();   
    }

    // 
    // 鶏を蹴った時の処理
    // 普通の蹴り
    void Normal()
    {
        Collider[] hitColliders = 
        Physics.OverlapBox(transform.position,transform.lossyScale,Quaternion.identity,mask);
         
        if(hitColliders.Length!=0)
        {
            int value;
            if(hitColliders.Length<length)
            {
                value=hitColliders.Length;
            }
            else
            {
                value=length;
            }

            for(int i=0;i<value;i++)
            {
                float randX=Random.Range(-rand,rand);
                Vector3 vec =new Vector3(0,randX,0);
                hitColliders[i].GetComponent<TargetMove>().AttackSwitching(transform.position,transform.forward,vec,angle,scale);
            }
        }
    } 

    // 拡散
   void Diffusion(float spread)
   {
         Collider[] hitColliders = 
         Physics.OverlapBox(transform.position,transform.lossyScale,Quaternion.identity,mask);
         
         if(hitColliders.Length!=0)
         {
             int value;
             if(hitColliders.Length<length)
             {
                 value=hitColliders.Length;
             }
             else
             {
                 value=length;
             }
            
             for(int i=0;i<value;i++)
             {
             Vector3 vec;
             vec=new Vector3(0,spread+(Mathf.Abs(spread)*2/value*(i+1)), 0);
             hitColliders[i].GetComponent<TargetMove>().AttackSwitching(transform.position,transform.forward,vec,angle,scale);
             }
         }
   }

   // 蹴り上げ
   void Upper(){
       Collider[] hitColliders = 
       Physics.OverlapBox(transform.position,transform.lossyScale,Quaternion.identity,mask);
       for(int i=0;i<hitColliders.Length;i++)
        {     
            hitColliders[i].GetComponent<TargetMove>().AttackSwitching(angle,scale);
        }
    }
}
