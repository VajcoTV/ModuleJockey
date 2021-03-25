
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //**new movement**//
    [SerializeField] float hSpeed = 10f;
    [SerializeField] float vSpeed = 6f;
    Rigidbody2D rb2D;
    public bool canMove = true;
    bool facingRight = true;
    [Range(0, 1.0f)]
    [SerializeField] float movementSmooth = 0.5f;
    private Vector3 velocity = Vector3.zero;
    //**movement**//
    [SerializeField] float JumpForce;
    [SerializeField] float movement;
    [SerializeField] float movementSpeed;
    
    [SerializeField] bool isGrounded = true;
    private Vector3 targetVelocity;
    private Vector3 LastVelocity = Vector3.zero;
    private Transform transform;

    //**Animator**//
    private Animator animator;

    //**Health**//
    public float Health = 3;

    //**Combo Attack**//
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;

    //**Dash**//
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    private bool candash = true;

    //Awake
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    //Start
    void Start()
    {
        animator = GetComponent<Animator>();
        transform = GetComponent<Transform>();
        dashTime = startDashTime;
    }
    //Update
    void Update()
    {
        Swing();
    }
    //FixedUpdate
    private void FixedUpdate()
    {
        
        Dash();
    }
    //OnCollisionEnter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playercollider")
        {
            isGrounded = true;
            candash = true;
        }
    }
    //Logic Behind the Run
    public void Move(float hMove, float vMove, bool jump)
    {
       if(canMove)
        {
          
            Vector3 targetVelocity = new Vector2(hMove * hSpeed, vMove * vSpeed);
            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmooth);
            if(hMove > 0 && !facingRight)
            {
                animator.SetBool("Run", true);
                Flip();
            }
            else if(hMove < 0 && facingRight)
            {
                animator.SetBool("Run", true);
                Flip();
            }
           
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
            transform.Rotate(0, 180, 0);
    }
   //Logic Behind the Swing
    public void Swing()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                animator.SetBool("Attack1", true);
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }
    //Atack Stuff returning bools
    public void return1()
    {
        if (noOfClicks >= 2)
        {
            animator.SetBool("Attack2", true);
        }
        else
        {
            animator.SetBool("Attack1", false);
            noOfClicks = 0;
        }
    }

    public void return2()
    {
        if (noOfClicks >= 3)
        {
            animator.SetBool("Attack3", true);
        }
        else
        {
            animator.SetBool("Attack2", false);
            noOfClicks = 0;
        }
    }

    public void return3()
    {
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        noOfClicks = 0;
    }
    //Dash
    public void Dash()
    {
        if(direction == 0)
        {
            if(Input.GetKeyDown(KeyCode.LeftControl))
            {
                if(movement < 0)
                {
                    direction = 1;
                }else if(movement > 0)
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if(dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb2D.velocity = Vector2.zero;
            }else
            {
                dashTime -= Time.deltaTime;
                if(direction == 1 && candash)
                {
                    rb2D.velocity = Vector2.left * dashSpeed;
                    candash = false;
                }else if (direction == 2 && candash)
                {
                    rb2D.velocity = Vector2.right * dashSpeed;
                    candash = false;
                }
            }
        }
    }

    
}