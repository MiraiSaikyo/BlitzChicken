using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour {

    // 
    public GameObject player;
    public  GameObject mainCamera;

    //特定の座標
    public GameObject cameraPos;
    public Transform adsPoint;
    public Transform endPoint;
    public Transform deathPoint;
    
    //カメラの感度
    public  float sensibility;
    
    // カメラの回転を制限する値
    [SerializeField,Range(-80,0)]
    public  float angleLimit_Min = 0;
    [SerializeField, Range(0,80)]
    public  float angleLimit_Max = 0;

    // ロックオンする機能
    private GameObject     lockOnTarget;
    private LockOnDetector lod;
    public bool isLockOn;
    public bool isAds;

    public LayerMask mask;

    // カメラを揺らす機能
    private float lifeTime;
    private float shakeRange;

    bool isPlayerDeath;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main.gameObject;
        //target = GameObject.FindGameObjectWithTag("XXXX");  
        endPoint.position = mainCamera.transform.position;
        lod = GetComponent<LockOnDetector>();
        lifeTime = 0f;
        shakeRange = 0f;
	}

    // Update is called once per frame
    void Update() {
        RotateClamp(angleLimit_Min, angleLimit_Max);

        if (player.GetComponent<Player_State>().isDeath)
        {
            //mainCamera.transform.position = deathPoint.position;
            //mainCamera.transform.rotation = deathPoint.rotation;
            // GetComponent<Animator>().enabled = true;
        }
        else
        {
            transform.position = cameraPos.transform.position;
            AdsMode(Input.GetAxis("R_Trigger") <= -0.9f, 10);
            RayZoom();
             LockOnMode();
            if (lockOnTarget)
            {
                TargetLockOn(lockOnTarget);
            }
            else
            {
                RotateReset();
            }

            Shake();
            RotateCameraAngle();
        }
        RotateClamp(angleLimit_Min, angleLimit_Max);
    }



    //入力されるとカメラの向きが変わる
    void RotateCameraAngle()
    {
        if (isLockOn)
        {
           Vector3 angle = new Vector3(
         Input.GetAxis("Mouse Y") * sensibility, 0);

           transform.eulerAngles += new Vector3(AngleClamp(angle.x, -10, 10), 0);
        }
        else
        {
            Vector3 angle = new Vector3(
              Input.GetAxis("Mouse Y") * sensibility,
                Input.GetAxis("Mouse X") * sensibility,
                0);
            transform.eulerAngles += new Vector3(AngleClamp(angle.x, -10,10), angle.y);
        }
    }
    
    //カメラが引数のオブジェクトの方向を向く
    void TargetLockOn(GameObject Lock)
    {
        Quaternion myQ = Quaternion.LookRotation((Lock.transform.position-mainCamera.transform.position).normalized);
        transform.rotation = Quaternion.Lerp(transform.rotation, myQ, 10 * Time.deltaTime);
        transform.eulerAngles-=new Vector3(0,0,transform.localEulerAngles.z);
        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
    } 
    //入力されるとロックオンモードになる
    void LockOnMode()
    {
        if (lod.getTarget() == null)
        {
            lockOnTarget = null;
            isLockOn = false;
        }
        else
        {
            if (Input.GetAxis("R_Trigger") <= -0.9f)
            {
                GameObject target = lod.getTarget();
                if (target != null)
                {
                    lockOnTarget = target;
                    isLockOn = true;
                }
            }
            else
            {
                lockOnTarget = null;
                isLockOn = false;
            }
       
        }
    }

    // カメラが寄る
    void AdsMode(bool xflag, float speed)
    {
        if (xflag)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, adsPoint.position, speed * Time.deltaTime);
            isAds = true;
        }
        else
        {
            //mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, endPoint.position, speed * Time.deltaTime);
            isAds = false;
        }
    }
    
    //入力されるとカメラがプレイヤーの向いている向きになる
    void RotateReset()
    {
        if (Input.GetButtonDown("L_Button"))
        {
            transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            cameraPos.transform.eulerAngles.y,
            transform.eulerAngles.z);
        }
    }

    //Y軸の回転制限 
    float AngleClamp(float angle,float min,float max)
    {
       if(angle<-360)
        { angle += 360; }
       if(angle>360)
        { angle -= 360; }
        return Mathf.Clamp(angle, min, max);
    }
    void RotateClamp(float min,float max)
    {
        float angle_x = 180f <= transform.eulerAngles.x ? transform.eulerAngles.x - 360 : transform.eulerAngles.x;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(angle_x, min, max),
            transform.eulerAngles.y,
            transform.eulerAngles.z);
    }

    // 壁越しとかになるとプレイヤーにカメラが寄る
    void RayZoom()
    {
        RaycastHit hit;
        if (!isAds)
        {
            if (Physics.Raycast(transform.position, (endPoint.position - transform.position).normalized, out hit, 2, mask))
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, hit.point, 5 * Time.deltaTime);
            }
            else
            {
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, endPoint.position, 5 * Time.deltaTime);
                //mainCamera.transform.position = endPoint.position;
            }
        }

        //Debug.DrawRay(transform.position, ( endPoint.position- transform.position ).normalized);
    }



    // カメラを揺らす
    void Shake()
        {
            if (lifeTime > 0.0f)
            {
                lifeTime -= Time.deltaTime;
                float x_val = Random.Range(-shakeRange, shakeRange);
                float y_val = Random.Range(-shakeRange, shakeRange);
                transform.position += new Vector3(x_val, y_val, 0);
            }
        }
    // 揺らすカメラのステータスを取得
    public void setShake(float setTime,float setRange)
    {
        lifeTime = setTime;
        shakeRange = setRange;
    }


}
