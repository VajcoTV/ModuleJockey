using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] Movement characterMovement;
    float horizontalMove;
    float verticalMove;
    Animator animator;
    Transform playerpos;
    private void Awake()
    {
        playerpos = GetComponent<Transform>();
        characterMovement = GetComponent<Movement>();
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        verticalMove = Input.GetAxis("Vertical");
        horizontalMove = Input.GetAxis("Horizontal");
        if(horizontalMove == 0 && verticalMove == 0)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
    }
    private void FixedUpdate()
    {
        Vector2 viewPos = transform.position;
        viewPos.y = Mathf.Clamp(viewPos.y, 8f, 15f);
        transform.position = viewPos;
        characterMovement.Move(horizontalMove, verticalMove, false);
    }
}
