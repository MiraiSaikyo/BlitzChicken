/// <summary>
@file   TargetRotate.cs
@brief  敵がプレイヤーの方向を向く処理
@author 齊藤未来
@details　Arbor上でしか使えないので注意
/// </summary>

using UnityEngine;
using System.Collections;
using Arbor;

public class TargetRotate : StateBehaviour {
    private Vector3 target;

    [SerializeField]
    private float RotateTime;
    [SerializeField]
    private float angleOffset;
    [SerializeField]
    private StateLink NextScene;

    public string rightName;
    public string leftName;

    Animator anim;

    public override void OnStateBegin()
    {
        anim = GetComponent<Animator>();
        // プレイヤーの座標を取得
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void Update() 
    {
        // プレイヤーの方向を取得
        Vector3 vec = new Vector3(target.x, 0, target.z) - new Vector3(transform.position.x, 0, transform.position.z);

        // プレイヤーの方向に対して自分の向きの差がangleOffset以下なら攻撃このステートを通過
        if (Vector3.Angle(vec.normalized, transform.forward) <= angleOffset)
        {
            Transition(NextScene);
            anim.SetBool(leftName, false);
            anim.SetBool(rightName, false);
        }
        else
        {
            // 回転自体はアニメーションでしている

            // 右回転
            if (vec.x>=angleOffset)
            {
                anim.SetBool(leftName, false);
                anim.SetBool(rightName, true);
            }
            // 左回転
            else if((vec.x<=angleOffset))
            {
                anim.SetBool(rightName, false);
                anim.SetBool(leftName, true);
            }
        }
    }
}
