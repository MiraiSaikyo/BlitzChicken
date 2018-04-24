using UnityEngine;
using System.Collections;
using Arbor;
using UnityEngine.AI;

public class Sensing : StateBehaviour {

    public Transform target;
    public Transform rayStart;
    public float LimitDistance = 10f;
    [SerializeField]
    private StateLink NextScene;
    NavMeshAgent agent;
    [SerializeField]
    LayerMask mask;
    // Use this for initialization
    void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    // Use this for awake state
    public override void OnStateAwake() {
    }

    // Use this for exit state
    public override void OnStateEnd() {
    }

    // Update is called once per frame
    void Update() {


        sence();
        //agent.SetDestination(target.position);
    }

    void sence()
    {
        RaycastHit hit;
        Physics.Raycast(rayStart.position, (target.position - rayStart.position).normalized, out hit,Mathf.Infinity,mask);


        Vector3 Apos = transform.position;
        Vector3 Bpos = target.position;
        float dis = Vector3.Distance(Apos, Bpos);
        //Debug.Log(dis);
        Debug.Log(hit.collider.tag);


        if ((hit.collider.tag == "wall")||(dis>=LimitDistance))
        {

            GetComponent<Animator>().SetBool("Walk", true);
            agent.SetDestination(target.position);
            Debug.DrawRay(rayStart.position, (target.position - rayStart.position).normalized * LimitDistance);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
            agent.SetDestination(rayStart.position);
            agent.velocity = Vector3.zero;

            Transition(NextScene);
        }
    }
}
