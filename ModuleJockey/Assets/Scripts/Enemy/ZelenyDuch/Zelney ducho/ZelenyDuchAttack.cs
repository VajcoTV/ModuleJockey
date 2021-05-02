using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZelenyDuchAttack : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    float speed = 10;
    Transform enemy;
    Vector3 pos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody>();
        enemy = animator.GetComponent<Transform>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(player.transform.position, enemy.transform.position) < 3f)
        {
            player.GetComponent<Movement>().TakeDmg(1);
        }
    }

 
}
