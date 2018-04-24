using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class FormationPosition : MonoBehaviour {

    public GameObject targetPos;
    public float distance;

    public Transform FormationPos;

    public Transform adsPoint;
    GameObject[] chicken;
    public int chickenLength;

    public List<GameObject> list = new List<GameObject>();
    int halfSize;
    
    public float offset;
    public bool isCluck = false;
    public bool isAngry=false;
    private void Update()
    {
        chickenLength = list.Count;


        if(targetPos.GetComponent<Player_State>().isAds)
        {
            FormationPos.position = Vector3.Lerp(FormationPos.position, 
            adsPoint.position+new Vector3(0,0.3f), 100 * Time.deltaTime);
            FormationPos.LookAt(adsPoint);
            offset = 0.1f;
        }
        else
        {
                        FormationPos.position = Vector3.Lerp(FormationPos.position, 
            adsPoint.position+new Vector3(0,0.3f), 100 * Time.deltaTime);
            FormationPos.LookAt(adsPoint);

            //FormationPos.position = new Vector3(FormationPos.position.x, targetPos.transform.position.y, FormationPos.position.z);
            offset =0.3f;
        }

        Vector3 Apos = FormationPos.position;
        Vector3 Bpos = targetPos.transform.position;
        float dis = Vector3.Distance(Apos, Bpos);

        if (dis > distance)
        {
            FormationPos.position = Vector3.Lerp(FormationPos.position, targetPos.transform.position, 10*Time.deltaTime);
        }
        list.Distinct().ToArray();
        for (int i = 0; i < list.Count; i++)
        {

            if(list[i]==null)
            {
                list.RemoveAt(i);
            }

            else if (list[i].GetComponent<TargetMove>().number <= -1)
            {
                list[i].GetComponent<NavMeshAgent>().enabled = false;
                list.RemoveAt(i);
                
            }
        }


        if (list.Count != 0)
        {
            halfSize=Mathf.RoundToInt(Mathf.Sqrt(list.Count));

            for (int j = 0; 0 <halfSize; j++)
            {
                for (int i = list.Count/halfSize*j; i < list.Count / halfSize * (j+1); i++)
                {
                    if(i==list.Count)
                    { return; }

                    if (list[i].GetComponent<TargetMove>().number >= 0)
                    {
                        if (list[i].GetComponent<TargetMove>().number != i)
                        {
                            list[i].GetComponent<TargetMove>().number = i;
                        }
                        else
                        {
                            //list[i].GetComponent<NavMeshAgent>().SetDestination(
                            //       (FormationPos.position 
                            //    + (FormationPos.forward  * (i-(list.Count/halfSize*j)-halfSize/2)*offset)
                            //    + (FormationPos.right  * ((j - halfSize / 2)))*offset));

                            list[i].GetComponent<TargetMove>().targetPos = (FormationPos.position
                                + (FormationPos.forward * (i - (list.Count / halfSize * j) - halfSize / 2) * offset)
                                + (FormationPos.right * ((j - halfSize / 2))) * offset);
                        }
                    }
                    
                }
            }
        }



        if(isAngry)
        {

        }
    }



    private void OnTriggerStay(Collider coll)
    {
        if (isCluck)
        {
            if (coll.gameObject.tag == "Chicken")
            {
                //    var tm = coll.GetComponent<TargetMove>();
                //    tm.targetPos = pos;
                //    coll.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pos.position);
                if (coll.gameObject.GetComponent<TargetMove>().number == -1)
                {
                    list.Add(coll.gameObject);
                    coll.gameObject.GetComponent<TargetMove>().number = list.Count-1;
                } 
            }
        }

         if(isAngry)
        {
            
            if (coll.gameObject.tag == "Chicken")
            {
                //    var tm = coll.GetComponent<TargetMove>();
                //    tm.targetPos = pos;
                //    coll.GetComponent<UnityEngine.AI.NavMeshAgent>().SetDestination(pos.position);
                coll.gameObject.GetComponent<TargetMove>().AngryAttack(targetPos.transform.position,20,10f);
              
            }
        }
    }
}
