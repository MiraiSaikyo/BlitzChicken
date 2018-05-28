/// <summary>
@file   Enemy_ChildParts.cs
@brief  敵の当たり判定
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ChildParts : MonoBehaviour {
	public Enemy_Parts enemy_Parts;
	
	// プレイヤーの攻撃から呼び出される
	public void OnDamage(int power)
	{
		enemy_Parts.Damage(power);
	}
}
