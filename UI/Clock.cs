
/// <summary>
@file   Clock.cs
@brief  時間を数え、表示。クエストの成否を判定
@author 齊藤未来
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {
    public GameObject []chara;
    [System.NonSerialized]
    public int []second=new int[4];
    float time;
    public int chicken;
    float fps;
    public SceneController sceneCon;
    float endTime;
    bool endFlag;
	// Use this for initialization
	void Start () {
        for(int i=0;i<second.Length;i++)
        {
            second[i]=0;
        }

        fps=0;
        time = 0;
        endTime=0;
        endFlag=false;
        chicken = 0;
        

        for(int i=0;i<3;i++)
        {
        QuestData.Instance.flag[i]=false;　// Questのクリア条件をリセット
        }
    }
	
	// Update is called once per frame
	void Update () {
        if ((chara[0].GetComponent<EnemyState>().Enemy_Life >0)
            &&(chara[1].GetComponent<Player_State>().Player_Life>0))
        {   
            // 時間数える
            time += Time.deltaTime;
            if(time>10)
            {
                second[1]++;
                time = 0;
            }
            if(second[1]>5)
            {
                second[2]++;
                second[1]=0;
                time=0;
            }
            if(second[2]>10)
            {
                second[3]++;
                second[2]=0;
                second[1]=0;
                time=0;
            }
            second[0]=(int)time;
        }
        else
        {
            endTime+=Time.deltaTime;


            if((endTime>=10f)&&(!endFlag))
            {
                //プレイヤーが死亡していた場合Titleに遷移する
                if(chara[1].GetComponent<Player_State>().Player_Life<=0)
                {
                    sceneCon.ChangeScene("title");
                }
                // 敵が死亡していた場合Resultに遷移する
                else
                {
                sceneCon.ChangeScene("result");

                // Questがクリアされているか確認する
                if(second[2]<2)
                {
                    QuestData.Instance.flag[1]=true;   
                }

                if(chicken<100)
                {
                    QuestData.Instance.flag[2]=true;
                }
                }
                endFlag=true;
            }
        }
	}
}
