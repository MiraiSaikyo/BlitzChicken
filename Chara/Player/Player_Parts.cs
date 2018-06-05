
/// <summary>
@file   Player_Parts.cs
@brief  プレイヤーの部位毎の当たり判定
@author 齊藤未来
/// </summary>
/// 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Parts : MonoBehaviour {
    Player_State player;
    int stunValue;

    void Start()
    {
        player = transform.root.gameObject.GetComponent<Player_State>();
    }
    // 敵の攻撃から呼ばれる
    public void Damage(int power)
    {
        player.Player_Life -= power;
    }

}
