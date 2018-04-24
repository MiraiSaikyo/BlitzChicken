using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeedManager : MonoBehaviour {

    Animator anim;
    [System.NonSerialized]
    public float speed=1;


    public float hitStopTime;
    public bool isHitStop;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHitStop)
        {
            StartCoroutine(attackHitStop(anim, hitStopTime));
        }
        else
        {
            anim.speed = speed;
        }
    }


    void s()
    {
        if (isHitStop)
        {
            StartCoroutine(attackHitStop(anim, hitStopTime));
        }
        else
        {
            anim.speed = speed;
        }
    }
    public IEnumerator attackHitStop(Animator animator, float stopTime)// ヒットストップ
    {
        animator.speed = 0.1f;
        yield return new WaitForSeconds(stopTime);
        animator.speed = 1f;
        isHitStop = false;
    }
}
