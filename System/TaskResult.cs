using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskResult : MonoBehaviour {

	public Toggle[] task;

	int starCounter=0;
	public GameObject Evaluation;
	public TextMeshProUGUI textMeshPro;
	bool[] flag;

	public AudioClip[] ac;



	// Use this for initialization
	void Start () {
		starCounter=0;
		flag=new bool[task.Length];
		flag[0]=true;
		for(int i=1;i<task.Length;i++)
		{
			flag[i]=QuestData.Instance.flag[i];
		}
		StartCoroutine (taskUpdate());  

	}
	
	// Update is called once per frame
	void Update () 
	{

		if((Evaluation.active==true)&&Input.GetButtonDown("Fire1"))
		{
			GameObject.Find("SceneController").GetComponent<SceneController>().ChangeScene("title");
		}



	}


	IEnumerator taskUpdate()
	{
		yield return new WaitForSeconds (3f);  
		 for (int i = 0; i < 3; i++) {  
			 task[i].isOn=flag[i];

			 if(task[i].isOn==true)
			 {
			 starCounter++;
			 }
			 if(Input.GetButtonDown("Fire1")&&(task[i]!=false))
			 {
				 yield return null;
			 }	 
            yield return new WaitForSeconds (1f);  
		 }

		Evaluation.active=true;
		 for(int i=0;i<starCounter;i++)
			 {
				 if(ac[i]!=null)
				 {
				 Evaluation.GetComponent<AudioSource>().PlayOneShot(ac[i]);
				 }
				 else
				 {
					 break;
				 }
			 }



		 switch(starCounter)
		 {
			 case 1:
			 textMeshPro.GetComponent<TextMeshProUGUI>().text="NORMAL CHICKEN";
			 break;

			 case 2 :			 
			 textMeshPro.GetComponent<TextMeshProUGUI>().text="GREAT CHICKEN!";
			 break;

			 case 3:
			 textMeshPro.GetComponent<TextMeshProUGUI>().text="BLITZ<br> CHICKEN!!";
			 GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger("Blitz");
			 break;

			 
		 }
	}
}
