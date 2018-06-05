
/// <summary>
@file   CameraController.cs
@brief  カメラを切り替える処理
@author 齊藤未来
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
    public GameObject playerCamera;
    public GameObject playerDeathCamera;
    bool isDeathPlayer = false;

    public GameObject enemy;
    public GameObject enemyCamera;
    bool isDeathEnemy = false;

    public AudioSource audioSource;
    public AudioClip[] audioClip;

    void Start() 
    {
        enemyCamera.SetActive(false); 
    }

    // Update is called once per frame
    void Update
    {
        // プレイヤーが死亡したときカメラを変える
        if ((player.GetComponent<Player_State>().Player_Life <= 0) && (!isDeathPlayer))
        {
            isDeathPlayer = true;
            playerDeathCamera.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip[1]);
        }

        // 敵が死亡したときカメラを変える
        else if ((enemy.GetComponent<EnemyState>().Enemy_Life <= 0) &&(!isDeathEnemy) &&(!isDeathPlayer))
        {
            enemyCamera.SetActive(true);
            isDeathEnemy = true;
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip[0]);
        }

        // カメラの演出が終わった後に元のカメラに戻す
        if (enemyCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End")
        ||playerDeathCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            enemyCamera.SetActive(false);
            playerDeathCamera.SetActive(false);
        }
    }

    // 演出に合わせて音を再生
    public IEnumerator BGMTimer(AudioClip ac,float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(ac);
    }
}
