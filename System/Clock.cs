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
        QuestData.Instance.flag[i]=false;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if ((chara[0].GetComponent<EnemyState>().Enemy_Life >0)
            &&(chara[1].GetComponent<Player_State>().Player_Life>0))
        {   
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
                if(chara[1].GetComponent<Player_State>().Player_Life<=0)
                {
                    sceneCon.ChangeScene("title");
                }
                else
                {
                sceneCon.ChangeScene("result");
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
    

        //fps=1f/Time.deltaTime;


	}


    private void OnGUI()
    {
        // int iFps=(int)fps;

        // GUILayout.Label(iFps.ToString()+"FPS");
    //     GUILayout.Label(second[3].ToString()+second[2].ToString()+":"+second[1].ToString()+second[0].ToString());
    //     GUILayout.Label(chicken.ToString()+"羽やってしまった");
    }
}
