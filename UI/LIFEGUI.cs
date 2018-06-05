
/// <summary>
@file   LIFEGUI.cs
@brief  LIFEのGUIの表示
@author 齊藤未来
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LIFEGUI : MonoBehaviour {

    public Player_State playerState; // プレイヤーのHP
    public EnemyState EnemyState; // 敵のHP

    int Life;
    Image image;

    void Start () 
    {
        image = GetComponent<Image>();
        if (playerState != null)
        {
            Life = playerState.Player_Life;
        }
        else
        {
            Life = EnemyState.Enemy_Life;
        }
    }
	
	void Update () 
    {
        if (playerState != null)
        {
            image.fillAmount= (float)playerState.Player_Life/Life;
        }
        else
        {
            image.fillAmount = (float)EnemyState.Enemy_Life / Life;
        }
    }
}
