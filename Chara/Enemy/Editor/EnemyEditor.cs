using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyState))]
[CanEditMultipleObjects]

public class EnemyEditor : Editor {
	Vector2 scrollValue=Vector2.zero;
	int toolbar=0;
	public override void OnInspectorGUI()
	{
	//	skin=Resources.LoadAssetAtPath<GUISkin>("EnemyEditor");

		
		EnemyState es=target as EnemyState;
		
	　　

		toolbar=GUILayout.Toolbar(toolbar, new string[] { "総ステータス", "部位ステータス", "攻撃ステータス" });

		switch(toolbar)
		{
			case 0:
			StateView(es);
			break;
			case 1:
			PartsView(es);
			break;
			case 2:
			AttackView(es);
			break;
		}
	
	
	
	}




	
	void StateView(EnemyState es)
	{
		// 体力の表示
		EditorGUILayout.LabelField("LIFE(Now/MAX)");
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.IntField(es.Enemy_Life,GUILayout.Width(48));
		es.Enemy_Max_Life=EditorGUILayout.IntField(es.Enemy_Max_Life,GUILayout.Width(48));
		EditorGUILayout.EndHorizontal();
	}
	void PartsView(EnemyState es)
	{

		List<Enemy_Parts> list=es.enemy_Parts;

		EditorGUILayout.BeginVertical(GUI.skin.box);
		EditorGUILayout.LabelField("部位/ひるみ値/耐性値");
		scrollValue=EditorGUILayout.BeginScrollView(scrollValue,GUI.skin.box);
		for(int i=0;i<list.Count;i++)
		{
			if(list[i]==null)
			{
				list.RemoveAt(i);
			}
			else
			{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);
			EditorGUILayout.LabelField(list[i].partsName,GUI.skin.customStyles[0],GUILayout.Width(40));			 
			list[i].stunValue=EditorGUILayout.IntField(list[i].stunValue,GUILayout.Width(40));			
			list[i].defensePower=EditorGUILayout.Slider(list[i].defensePower,0,100,GUILayout.Width(150));
			EditorGUILayout.Space();
			if(GUILayout.Button("▲",GUILayout.Width(30)))
			{
				if(i>0)
				{
				Enemy_Parts epBox=list[i];
				list[i]=list[i-1];
				list[i-1]=epBox;				
				}
			}
			else if(GUILayout.Button("▼",GUILayout.Width(30)))
			{
				if(i<list.Count-1)
				{
				Enemy_Parts epBox=list[i];
				list[i]=list[i+1];
				list[i+1]=epBox;				
				}
			}
			if(GUILayout.Button("Remove",GUILayout.Width(70)))
			{
				list.Remove(list[i]);
			}
			EditorGUILayout.EndHorizontal();

			}
		}
		EditorGUILayout.EndScrollView();
	
		EditorGUILayout.BeginHorizontal();

		
		Enemy_Parts ep=EditorGUILayout.ObjectField("Add Parts",null,typeof(Enemy_Parts),true) as Enemy_Parts;
		if(ep!=null)
		{
			list.Add(ep);
		}
		if(GUILayout.Button("自動追加"))
		{
			list.Clear();
			var childTrans=es.gameObject.GetComponentsInChildren<Transform>();
			foreach(Transform child in childTrans)
			{
				if(child.GetComponent<Enemy_Parts>()!=null)
				{
				list.Add(child.GetComponent<Enemy_Parts>());
				}
			}
		}
		EditorGUILayout.EndHorizontal();


		EditorGUILayout.EndVertical();
	}
	void AttackView(EnemyState es)
	{		
		List<EnemyAttack> list=es.enemyAttack;

		EditorGUILayout.BeginVertical(GUI.skin.box);
		scrollValue=EditorGUILayout.BeginScrollView(scrollValue,GUI.skin.box);
		for(int i=0;i<list.Count;i++)
		{
			if(list[i]==null)
			{
				list.RemoveAt(i);
			}
			else
			{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);
			EditorGUILayout.LabelField(list[i].text);
			list[i].power=EditorGUILayout.IntField(list[i].power);

			
			
			
			EditorGUILayout.Space();
			if(GUILayout.Button("▲",GUILayout.Width(30)))
			{
				if(i>0)
				{
				EnemyAttack epBox=list[i];
				list[i]=list[i-1];
				list[i-1]=epBox;				
				}
			}
			else if(GUILayout.Button("▼",GUILayout.Width(30)))
			{
				if(i<list.Count-1)
				{
				EnemyAttack epBox=list[i];
				list[i]=list[i+1];
				list[i+1]=epBox;				
				}
			}

		

			if(GUILayout.Button("Remove",GUILayout.Width(70)))
			{
				list.Remove(list[i]);
			}
			EditorGUILayout.EndHorizontal();

			}
		}
		EditorGUILayout.EndScrollView();

		EditorGUILayout.BeginHorizontal();
		
		EnemyAttack ep=EditorGUILayout.ObjectField("Add Attack",null,typeof(EnemyAttack),true) as EnemyAttack;
		if(ep!=null)
		{
			list.Add(ep);
		}
		if(GUILayout.Button("自動追加"))
		{
			list.Clear();
			var childTrans=es.gameObject.GetComponentsInChildren<Transform>();
			foreach(Transform child in childTrans)
			{
				if(child.GetComponent<EnemyAttack>()!=null)
				{
				list.Add(child.GetComponent<EnemyAttack>());
				}
			}
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
	}

	
}
