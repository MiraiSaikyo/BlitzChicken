using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnList : MonoBehaviour {

    public GameObject []lockParts;
    public GameObject Cursor;
    [System.NonSerialized]
    public int value;
    public Camera2 camera;
    [System.NonSerialized]
    public bool isCursor=false;
 
    private void Update()
    {
        isCursor = camera.GetComponent<Camera2>().isLockOn;

        if (isCursor)
        {
            Cursor.transform.LookAt(Camera.main.transform);
            Cursor.transform.position = lockParts[value].transform.position;
        }
    }
}
