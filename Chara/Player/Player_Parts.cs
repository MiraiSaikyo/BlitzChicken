using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Parts : MonoBehaviour {

    Player_State player;

    int stunValue;
    // Use this for initialization
    void Start()
    {
        player = transform.root.gameObject.GetComponent<Player_State>();
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void Damage(int power)
    {
        player.Player_Life -= power;

        //Enemy_Life -= power;

    }

}
