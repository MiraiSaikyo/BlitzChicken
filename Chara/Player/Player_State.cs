using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.PostProcessing;
public class Player_State : MonoBehaviour
{
    //public//
    public static Vector3 initGravity = new Vector3(0f, Physics.gravity.y, 0f);// 重力
    public int Player_Life = 100;　　// プレイヤーのヒットポイント
    public float move;  // プレイヤーの移動力
    public float jump;  // ジャンプ力
    public float rotate;// 回転力
    public float hitStopTime;
    [System.NonSerialized]
    public float speed = 1;
    [System.NonSerialized]
    public bool isHitStop;
    public LayerMask mask;//RaycastのMask;
    public bool isDeath = false;

    //private変数//
    private float moveX = 0f,moveY = 0f,moveZ = 0f; // 移動時のInputを格納
    private float inputPower   = 0f;          // 移動の力 
    private float time         = 0f;          // カウント用
    private float invinTime   = 0f;
    private float range        = 0.2f;        // Raycastの長さ
    private float GravityLimit = -10f;
    private Vector3 MoveExecute(float movePower)// 代入する移動量
    {
        return (transform.forward * inputPower * movePower);
    }
    private Vector3 velocityPower = Vector3.zero;// この変数をVeloctyに代入する
    private Vector3 Gravity       = Vector3.zero;// 重力計算用
    private bool isAttack     = false; // 攻撃用
    public bool isAds      = false; // ブリッツ用
    private bool isGround     = true;  // 地面と接しているか
    private bool isRaycast    = true;  // Raycastを投げるか
    private bool isJump       = false; // ジャンプをするか
    [SerializeField]
    private bool isInvincible = false; // ダメージ後の無敵用
    bool isRun=false;
    public bool pause=false;

    public enum Mode
    {
        Idle,
        Attack,
        Jump,
        Invincible,
        Death
    }
    [SerializeField]
    public Mode chickenState = Mode.Idle;
    private Rigidbody rb;
    private Animator  anim;
    private AudioShot audioShot;
    private PostProcessingBehaviour post;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        audioShot = GetComponent<AudioShot>();
        post = Camera.main.GetComponent<PostProcessingBehaviour>();

        SetVelocity(Vector3.zero);
    }
    void Update()
    {
        SetVelocity(Vector3.zero);
        AnimationSpeedController();

        if (Input.GetAxis("R_Trigger")<=-0.9)
        {
            Quaternion myQ = Quaternion.LookRotation(Camera.main.transform.forward*5);
            myQ = new Quaternion(0, myQ.y, 0, myQ.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, myQ, 100 * Time.deltaTime);
            isAds = true;
        }
        else
        {
            isAds = false;
        }


        switch (chickenState)
        {
            case Mode.Idle:
                isInvincible=false;
                OnGroundRaycast();
                ActionCommand();
                break;

            case Mode.Attack:
                AttackExecute();
                break;

            case Mode.Jump:
                JumpExecute();
                OnGroundRaycast();
                break;
        }


        //OnRaycast();
        InvincibleExecute();

        if (Player_Life <= 0&&!isDeath)
        {
            anim.SetTrigger("Death");
            chickenState = Mode.Death;
            isDeath = true;
            isAttack = false;
        }


        if(isAds)
        {
            anim.SetBool("isAds", true);
        }
        else
        {
            anim.SetBool("isAds", false);
        }

    }
    void FixedUpdate()
    {
        rb.velocity = velocityPower;
    }


    public void Damage(int power)// ダメージ処理
    {
        if (!isInvincible)
        {
            SetVelocity(Gravity);
            Player_Life -= power;
            if (Player_Life <= 0)
            {
                
            }
            else
            {
                anim.SetBool("Attack", false);
                anim.SetBool("BAttack", false);
                anim.SetBool("jump", false);
                anim.SetBool("Step", false);
                isInvincible = true;

                isAttack = false;
                anim.SetTrigger("Wound");
                //chickenState = Mode.Invincible;
            }
        }
    }
    public void SetVelocity(Vector3 velo)// 移動量を代入
    {
        velocityPower = velo;
    }
    public void ResetGravity()
    {
        Gravity = Vector3.zero;
    }
    public IEnumerator attackHitStop(Animator animator, float stopTime)// ヒットストップ
    {
        //animator.speed = 0.4f;
        post.profile.motionBlur.enabled = true;
        yield return new WaitForSeconds(stopTime);
        post.profile.motionBlur.enabled = false;

        animator.speed = 1f;
        isHitStop = false;
    }


    void ActionCommand()// 入力の管理
    {
        //ResetGravity();
                Gravity.y=-10f;

        SetVelocity(MoveExecute(move));
        PlayerMove();
       
        //Jump
        // if (Input.GetButtonDown("Jump") && isGround)
        // {
        //     chickenState = Mode.Jump;
        //     isJump = true;
        //     isGround = false;
        //     isRaycast = false;
        //     ResetGravity();
        //     anim.SetBool("jump", true);
        //     return;

        // }
    }
    void AttackExecute()// 攻撃の管理
    {
        
        anim.SetFloat("move", 0f);
        PlayerMove();
        // if (isAttack)
        // {
        //     if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //     {
        //         isAttack = false;
        //         chickenState = Mode.Idle;
        //     }
        // }
        // if (!(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
        // {
        //     isAttack = true;
        // }
    }
    void JumpExecute()// ジャンプの管理
    {
        anim.SetFloat("move", 0f);
       // ResetGravity();
        // if (isGround)
        // {
           
        // }
        // else
        {
            PlayerMove();  
            // isJumpフラグがオンの時にY軸方向に移動量を加算
            // 重力を移動量に加算
            //if (isJump)
            {
                SetVelocity(MoveExecute(move * 0.7f) + Gravity + Vector3.up * jump);
            }
            // else
            // {
            //     SetVelocity(MoveExecute(move * 0.7f) + Gravity);
            // }
        }


        // 徐々に重力を足していく
        Gravity += initGravity * Time.deltaTime;

        // 重力の制限　長押しでふわふわできる
        if(Input.GetButton("Jump"))
        {
            GravityLimit = -6f;
        }    
        else
        {
            GravityLimit = -10f;
        }
        
        if (Gravity.y<=GravityLimit)
        {
            Gravity.y = GravityLimit;
        }
    }
    void InvincibleExecute()// 無敵状態の管理
    {
        // if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        // {
        //     isInvincible = false;
        //     //chickenState = Mode.Idle;
        // }
    }
    void DeathExecute()// 死亡時の管理
    {


    }
    void PlayerMove()// プレイヤーの移動,回転の管理
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        //カメラの方向からx-z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        //方向キーの入力値とカメラの向きから移動方向を決定
        Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;

        if (isAds)
        {
            velocityPower = (transform.right * moveX * (move/2)) + (transform.forward * moveZ * (move / 2));
        }
        else
        {
            //キャラクターの進行方向に向きを
            if (moveForward.magnitude > 0.1f)
            {
                Quaternion myQ = Quaternion.LookRotation(moveForward);
                transform.rotation = Quaternion.Lerp(transform.rotation, myQ, rotate * Time.deltaTime);

                if(Input.GetButton("R_Button"))
                {
                    isRun = true;
                }

                if (Input.GetButton("R_Button") && (inputPower>=0.8))
                {

                    inputPower = moveForward.magnitude + 0.2f;

                }
                else
                {
                    inputPower = moveForward.magnitude;
                    
                }
            }
            else
            {
                isRun = false;
                inputPower = 0f;
            }
        }
        anim.SetFloat("move", inputPower);
       
    }
    void OnRaycast()// 前方向にRaycastを投げ移動量を制限する
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.2f, 0), transform.forward);

        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.2f, out hit, 0.2f))
        {
            SetVelocity(new Vector3(velocityPower.x, velocityPower.y, 0));
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red, range, false);
        }
    }
    void OnGroundRaycast()// 地面と接しているかを判定
    {
        Ray ray = new Ray(transform.position + new Vector3(0, range+0.1f, 0), transform.up * -1);

        RaycastHit hit;
        if (isRaycast)
        {
            if (Physics.SphereCast(ray, 0.15f, out hit, range, mask))
            {
                isGround = true;
                anim.SetBool("Fall", false);
                // anim.SetBool("jump", false);
            }
            else
            {
                Debug.DrawRay(ray.origin, ray.direction, Color.red, range, false);

                
               //chickenState = Mode.Jump;
               
                isGround = false;
                anim.SetBool("Fall", true);
                //anim.SetBool("jump", true);
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time > 0.5f)
            {
                //
                time = 0f;
                isRaycast = true;
            }
        }
    }
    void AnimationSpeedController()
    {
        if (isHitStop)
        {
            //StartCoroutine(attackHitStop(anim, hitStopTime));
        }
        else
        {
            anim.speed = speed;
        }
    }


    public void setSkill(int id)
    {
        switch(id)
        {
            case 0:
            skill0();
            break;
            case 1:
            break;
            case 2:
            break;
            case 3:
            break;
            
        }
    }

    void skill0()
    {
        anim.SetBool("Cluck",true);
    }
    



}
