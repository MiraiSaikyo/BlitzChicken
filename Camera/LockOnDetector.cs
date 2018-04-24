using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnDetector : MonoBehaviour {
    private GameObject target;
    private int value = 0;
    void OnTriggerStay(Collider c)
    {
        if (c.gameObject.tag == "LockOn")
        {

            if (target == null)
            {
                target = c.gameObject.GetComponent<LockOnList>().lockParts[value];
                c.gameObject.GetComponent<LockOnList>().value = value;
                //Debug.Log("targetCheck");
            }



            if (Input.GetButton("R_Button"))
            {
                ++value;
            }

            //if(Input.GetAxis("Mouse X")>=0.9)
            //{
            //    ++value;
            //}
            //else if(Input.GetAxis("Mouse X") <= -0.9)
            //{
            //    --value;

            //}
            if (value >= c.gameObject.GetComponent<LockOnList>().lockParts.Length)
            {
                value = 0;
            }
            if (value <= -1)
            {
                value = c.gameObject.GetComponent<LockOnList>().lockParts.Length-1;
            }

        }
    }
    void OnTriggerExit(Collider c)
    {
        {
            if(c.gameObject)
            {
                target = null;
            }
        }
    }
    public GameObject getTarget()
    {
        return this.target;
    }
}
