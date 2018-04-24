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

    // Use this for initialization
    void Start() {
        enemyCamera.SetActive(false);
        
    }

    // Update is called once per frame
    void Update() {


        if ((player.GetComponent<Player_State>().Player_Life <= 0) &&
         (!isDeathPlayer))
        {
            isDeathPlayer = true;
            //playerCamera.SetActive(false);
            playerDeathCamera.SetActive(true);
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip[1]);
        }


        else if ((enemy.GetComponent<EnemyState>().Enemy_Life <= 0) &&
            (!isDeathEnemy) &&
            (!isDeathPlayer))
        {
           // playerCamera.SetActive(false);
            enemyCamera.SetActive(true);
            isDeathEnemy = true;
            audioSource.Stop();
            audioSource.PlayOneShot(audioClip[0]);
        }



        if (enemyCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End")
        ||playerDeathCamera.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("End"))
        {
            //playerCamera.SetActive(true);
            enemyCamera.SetActive(false);
            playerDeathCamera.SetActive(false);
        }


    }

    public IEnumerator BGMTimer(AudioClip ac,float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(ac);

    }

}
