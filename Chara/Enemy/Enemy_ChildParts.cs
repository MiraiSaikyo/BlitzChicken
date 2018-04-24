using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ChildParts : MonoBehaviour {
	public Enemy_Parts enemy_Parts;
	// Use this for initialization
	void Start () {
		
	}

	public void OnDamage(int power)
	{
		enemy_Parts.Damage(power);
	}
}
