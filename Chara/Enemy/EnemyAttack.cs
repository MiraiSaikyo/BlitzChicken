﻿
/// <summary>
@file   EnemyAttack.cs
@brief  敵の攻撃処理
@author 齊藤未来
/// </summary>
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyAttack : MonoBehaviour{
    
    public string text;
    public int power;
    public float  time;
    public GameObject damageEffect;

    protected virtual IEnumerator DestroyHit()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            c.GetComponent<Player_State>().Damage(power);
            if (!(damageEffect == null))
            {
                Instantiate(damageEffect, transform.position, Quaternion.identity);
            }
        }

        if (c.tag == "Chicken")
        {
            c.GetComponent<TargetMove>().Damage(power);
            if (!(damageEffect == null))
            {
                Instantiate(damageEffect, transform.position, Quaternion.identity);
            } 
        }
    }
}