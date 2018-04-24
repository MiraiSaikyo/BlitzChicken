using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Parts : MonoBehaviour {


    public string partsName;
    EnemyState enemy;
    [SerializeField,Range(0,100)]
    public float defensePower=100;
    static float defenseRate=100;
    float defenceValue;
    public int stunValue=100;
    float resistance=0.2f;
    int stunInit;
	// Use this for initialization
	void Start () {
        stunInit = stunValue;
        enemy=transform.root.gameObject.GetComponent<EnemyState>();
        defenceValue=defensePower/defenseRate;
	}

    // Update is called once per frame
    void Update() {
        if (stunValue <= 0)
        {
            if(enemy.GetComponent<EnemyState>().Enemy_Life>0)
            {
                enemy.GetComponent<Animator>().SetTrigger("Reaction");
                stunValue = stunInit+(int)(stunInit*resistance);
            }
        }
    }


    public void Damage(int power)
    {
        if (enemy.Enemy_Life > 0)
        {
            transform.root.gameObject.GetComponent<Arbor.ParameterContainer>().SetBool("Warning", true);
            enemy.Enemy_Life -= (int)((float)power*defenceValue);
            stunValue -= power;

           // Debug.Log(power);
            //Debug.Log((int)((float)power*(defensePower/defenseRate)));
        }
        //Enemy_Life -= power;

    }

    public float getDefenceValue()
    {
        return defenceValue;
    }
}
