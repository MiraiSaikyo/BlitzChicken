using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroy : MonoBehaviour {

    public float TimeDes;


	// Use this for initialization
	void Start () {
        Invoke("Des", TimeDes);
	}
	
    void Des()
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "EnemyAttack" || other.tag == "Shield")
        {
            Des();
        }
    }
}
