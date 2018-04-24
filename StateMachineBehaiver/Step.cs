using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : StateMachineBehaviour {
    [SerializeField]
    float forward=1.5f,right,up;

    public bool isGravity;
    Rigidbody rb;
    [SerializeField, Range(0, 1)]
    float execute_time;
    [SerializeField, Range(0, 1)]
    float discontinuation_time;

    float moveX, moveZ;
    [SerializeField]
    float rotate =0.5f;

    public bool isNotVelocity = false;


    Player_State player;


    //float time;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();


        if (isGravity)
        {
            up = Physics.gravity.y ;
        }

        rb = animator.gameObject.GetComponent<Rigidbody>();
        player.SetVelocity(Vector3.zero);
    }
	//OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
       // time += Time.deltaTime;
        if (execute_time <= stateInfo.normalizedTime)
        {
            if (discontinuation_time > stateInfo.normalizedTime)
            {
                player.SetVelocity((animator.gameObject.transform.forward * forward)
                           +(animator.gameObject.transform.right   * right)
                           +(animator.gameObject.transform.up      * up));
            }
            else if(isNotVelocity)
            {

            }
            else
            {
                player.SetVelocity( new Vector3(0, Physics.gravity.y*Time.deltaTime, 0));
            }

        }

        PlayerMove(animator.gameObject);



        
    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player.SetVelocity(Vector3.zero);

    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}





    void PlayerMove(GameObject obj)
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        //カメラの方向からx-z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;


        //方向キーの入力値とカメラの向きから移動方向を決定
        Vector3 moveForward = cameraForward * moveZ + Camera.main.transform.right * moveX;
        //キャラクターの進行方向に向きを
        if (moveForward.magnitude > 0.1f)
        {
            Quaternion myQ = Quaternion.LookRotation(moveForward);
            obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, myQ, rotate * Time.deltaTime);
            
        }  
    }




}
