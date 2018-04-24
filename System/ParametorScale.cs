using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametorScale : MonoBehaviour {
    [System.Serializable]
    public struct ParametorBonb
    {
        public GameObject parts;
        [SerializeField, Range(0.5f,2)]
        public float scale ;


            [SerializeField, Range(0.5f, 2)]
            public float x;
            [SerializeField, Range(0.5f, 2)]
            public float y;
            [SerializeField, Range(0.5f, 2)]
            public float z;
        
    }

    public ParametorBonb[] pB;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        for(int i=0;i<pB.Length;i++)
        {
            pB[i].parts.transform.localScale = new Vector3(pB[i].x, pB[i].y, pB[i].z)*pB[i].scale;
        }
		
	}
}
