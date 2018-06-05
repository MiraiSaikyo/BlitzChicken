/// <summary>
@file   Enemy_Parts.cs
@brief  敵の当たり判定を一つの部位にまとめてダメージ計算
@author 齊藤未来
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parts : MonoBehaviour {

    public string partsName; // Editorで取得するために名前を決めとく
    EnemyState enemy;

    [SerializeField,Range(0,100)]
    public float defensePower=100; 
    static float defenseRate=100; // defensePowerを割る用
    
    float defenceValue;　// プレイヤーの攻撃力を何割通すかの値
    
    public int stunValue=100;// 0になると敵がひるむ
    
    float resistance=0.2f;
    int stunInit;
	
	void Start () 
    {
        stunInit = stunValue;
        enemy=transform.root.gameObject.GetComponent<EnemyState>();
        defenceValue=defensePower/defenseRate; // 防御力を計算
	}

    void Update() 
    {
        // ひるみ値が0以下になるとひるむ
        if (stunValue <= 0)
        {
            if(enemy.GetComponent<EnemyState>().Enemy_Life>0)
            {
                enemy.GetComponent<Animator>().SetTrigger("Reaction");
                stunValue = stunInit+(int)(stunInit*resistance);
            }
        }
    }

    public void Damage(int power)
    {
        // HPが0より大きかったらダメージ計算をする
        if (enemy.Enemy_Life > 0)
        {   
            transform.root.gameObject.GetComponent<Arbor.ParameterContainer>().SetBool("Warning", true);
            enemy.Enemy_Life -= (int)((float)power*defenceValue);　// 敵のHP -= プレイヤーの攻撃力*耐久値
            stunValue -= power; // スタン値を減算
        }
    }
    
    public float getDefenceValue()
    {
        return defenceValue; //　防御力
    }
}