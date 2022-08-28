using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleRandomStateMachineBehaviour : StateMachineBehaviour
{
    #region Variables

    //확장된 애니메이션의 갯수
    public int numberOfStates = 2;
    public float minNormTime = 0f;
    public float maxNormTime = 5f;

    public float randomNormalTime;

    // String끼리의 비교보다 정수끼리의 비교가 훨씬 비용이 적기에 'RandomIdle' String 값의
    //해쉬값을 사용.
    readonly int hashRadomIdle = Animator.StringToHash("RandomIdle");

    #endregion Variables
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Radomly decide a time at which to transition.
        randomNormalTime = Random.Range(minNormTime, maxNormTime);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // If transitioning away from this state, reset the random idle parameter to -1
        if(animator.IsInTransition(0) && animator.GetCurrentAnimatorStateInfo(0).fullPathHash != stateInfo.fullPathHash)
        {
            animator.SetInteger(hashRadomIdle, -1);
        }
        
        // If the state is beyond the ranomly decide normalised time and not yet transitioning
        if(stateInfo.normalizedTime > randomNormalTime && !animator.IsInTransition(0))
        {
            animator.SetInteger(hashRadomIdle, Random.Range(0, numberOfStates + 1));
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
