/// <summary>
@file   EnemyEditor.cs
@brief  敵のステータスをInspector上に表示する拡張
@author 齊藤未来
/// </summary>
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
		EnemyState es=target as EnemyState;
		
		// タブ表示
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

	// ステータスの初期値と現在値を表示
	void StateView(EnemyState es)
	{
		EditorGUILayout.LabelField("LIFE(Now/MAX)");

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.IntField(es.Enemy_Life,GUILayout.Width(48));// 現在のHP
		es.Enemy_Max_Life=EditorGUILayout.IntField(es.Enemy_Max_Life,GUILayout.Width(48)); // HPの最大値(初期値)
		EditorGUILayout.EndHorizontal();
	}
	// 部位ごとのひるみ値、耐性値を表示
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
		if(GUILayout.Button("自動追加"))　// このボタン押すとリストがリセットされて全ての部位を取得し直す
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
	// 攻撃ごとの攻撃力を表示
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
		if(GUILayout.Button("自動追加"))// このボタン押すとリストがリセットされて全ての攻撃オブジェクトを取得し直す
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
