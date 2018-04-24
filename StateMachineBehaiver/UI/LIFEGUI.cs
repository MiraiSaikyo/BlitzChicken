using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LIFEGUI : MonoBehaviour {

    public Player_State playerState;
    public EnemyState EnemyState;

    int Life;
    Image image;
    // Use this for initialization
    void Start () {
        image = GetComponent<Image>();
        if (playerState != null)
        {
            Life = playerState.Player_Life;
        }
        else
        {
            Life = EnemyState.Enemy_Life;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (playerState != null)
        {
            image.fillAmount= (float)playerState.Player_Life/Life;
        }
        else
        {
            image.fillAmount = (float)EnemyState.Enemy_Life / Life;

        }

    }
}
