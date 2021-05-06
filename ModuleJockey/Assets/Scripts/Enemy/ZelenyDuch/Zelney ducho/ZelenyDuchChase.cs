using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZelenyDuchChase : StateMachineBehaviour
{
    Transform player;
    Rigidbody rb;
    float speed = 10;
    Transform enemy;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = app.playermanager.player.transform;
        rb = animator.GetComponent<Rigidbody>();
        enemy = animator.GetComponent<Transform>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 target = new Vector3(player.position.x,player.position.y, player.position.z);
        Vector3 newpos = Vector3.MoveTowards(rb.position, target, speed * Time.deltaTime);
        rb.MovePosition(newpos);
        if(Vector3.Distance(player.transform.position, enemy.transform.position) < 1f)
        {
            animator.SetTrigger("Attack");
        }
        if(Vector3.Distance(player.transform.position, enemy.transform.position) > 20f)
        {
            animator.SetBool("Run", false);
        }

        if(player.position.x < enemy.position.x)
        {
            enemy.eulerAngles =  new Vector3(0, 0, 0);
        }
        else if (player.position.x > enemy.position.x)
        {
            enemy.eulerAngles = new Vector3(0, 180, 0);
        }
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    

}
