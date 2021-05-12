using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ZelenyDuchPatrol : StateMachineBehaviour
{
    GameObject player;
    Rigidbody rb;
    float speed = 10;
    Transform enemy;
    
    Vector3 PatrolPos;
    bool canchase = true;
    bool newposition = true;
    float timer = 3;
    Vector3 newpos;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = app.playermanager.player;
        rb = animator.GetComponent<Rigidbody>();
        enemy = animator.GetComponent<Transform>();
        canchase = true;

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (newposition)
        {
            PatrolPos = player.GetComponent<Movement>().PlayerRadius(enemy.position.y);
            Debug.Log("Yes Nova pozicia");
            newposition = false;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 3;
                newposition = true;
            }
        }
        newpos = Vector3.MoveTowards(rb.position, PatrolPos, speed * Time.deltaTime);
        rb.MovePosition(newpos);

        if (Vector3.Distance(player.transform.position, enemy.transform.position) < 1f)
        {
            animator.SetTrigger("Attack");
        }
        if (Vector3.Distance(player.transform.position, enemy.transform.position) > 40f)
        {
            animator.SetBool("Run", false);
        }
        if (player.GetComponent<Movement>().numberofenemy < player.GetComponent<Movement>().MaxNumberofenemy && canchase)
        {
            player.GetComponent<Movement>().numberofenemy += 1;
            animator.SetBool("Chase", true);
            canchase = false;
           
        }

        if (player.transform.position.x < enemy.position.x)
        {
            enemy.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (player.transform.position.x > enemy.position.x)
        {
            enemy.eulerAngles = new Vector3(0, 180, 0);
        }
        
        

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        newposition = true;
    }
    



}
