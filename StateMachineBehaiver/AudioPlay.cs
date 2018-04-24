using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : StateMachineBehaviour {

    
    [System.Serializable]
    public class AudioClipper
    {
        [System.NonSerialized]
        public AudioSource audioPlayer;
        public string sourceName;        
        public AudioClip ac;
        [SerializeField, Range(0, 1)]
        public float execute_Time;
        [System.NonSerialized]
        public bool isExecute;
    }
    public AudioClipper[] audioClipper;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < audioClipper.Length; i++)
        {
            audioClipper[i].audioPlayer = GameObject.Find(audioClipper[i].sourceName).GetComponent<AudioSource>();
            audioClipper[i].isExecute = false ;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        for (int i = 0; i < audioClipper.Length; i++)
        {
            if (audioClipper[i].execute_Time < stateInfo.normalizedTime && !audioClipper[i].isExecute)
            {
                audioClipper[i].isExecute = true;
                audioClipper[i].audioPlayer.PlayOneShot(audioClipper[i].ac);
            }
        }


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; i < audioClipper.Length; i++)
        {
            audioClipper[i].isExecute = false;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
