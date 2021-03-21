
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Enemy enemy;
    [SerializeField] float JumpForce;
    [SerializeField] float movement;
    [SerializeField] float movementSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] bool isGrounded = true;
    private Vector3 targetVelocity;
    private Vector3 LastVelocity = Vector3.zero;
    private int colliders = 0;
    private Transform transform;
    private Animator animator;
    public float Health = 3;
    public bool ICanSwing;
    public bool canbehit;
    public bool canrun = true;
    public bool canjump = true;
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;




    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        InputJump();
        Swing();
    }
    private void FixedUpdate()
    {
        RightRun();
    }
    private void Jump()
    {
        rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playercollider")
        {
            isGrounded = true;
        }
    }
    private void InputJump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded || Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
            isGrounded = false;
            Jump();
        }
        if (isGrounded)
        {
           
            animator.SetBool("Down", false);
        }else
        {
      
            animator.SetBool("Down", true);
        }

    }
    private void RightRun()
    {


        movement = Input.GetAxis("Horizontal");
        targetVelocity = new Vector3(movement * movementSpeed, rb.velocity.y);
        rb.velocity = targetVelocity;
        if (Input.GetAxis("Horizontal") != 0 && rb.velocity.x > 0f)
        {
            if (transform.localScale.x < 0f)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
            }
            animator.SetBool("Run", true);

        }
        if (Input.GetAxis("Horizontal") != 0 && rb.velocity.x < 0f)
        {
            if (transform.localScale.x > 0f)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
            }
            animator.SetBool("Run", true);

        }
        if (rb.velocity.x == 0f)
        {
            animator.SetBool("Run", false);
        }


    }
   
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
    public void turnoffplayer()
    {
        canjump = false;
        canrun = false;

    }
    public void turnonplayer()
    {
        canjump = true;
        canrun = true;
    }
    public void Icantswing()
    {
        ICanSwing = true;
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.GetComponentInParent<Enemy>();
        }
        if (collision.CompareTag("Player destroyer"))
        {

            
        }
    }
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
}