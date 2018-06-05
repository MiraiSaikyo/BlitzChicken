
/// <summary>
@file   RandomChoice.cs
@brief  データを保存
@author 齊藤未来
@details　ScriptbleObject使えば良かった...
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData{
	
	public readonly static QuestData Instance = new QuestData();
	public bool[] flag=new bool[3];
}
