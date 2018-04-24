using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollDestroy : MonoBehaviour {
    public string tag;

void OnTriggerStay(Collider coll)
    {
        if (coll.gameObject.tag == tag)
        {
            Destroy(gameObject);
        }

    }
}
