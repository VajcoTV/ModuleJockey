
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
    [SerializeField] Animator animator;
    public float Health = 3;
    public bool ICanSwing;
    public bool canbehit;
    public bool canrun = true;
    public bool canjump = true;
  

    void Start()
    {
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
        if (collision.gameObject.tag == "Stena")
        {
            isGrounded = true;
        }
    }
    private void InputJump()
    {


        if (Input.GetKeyDown(KeyCode.W) && isGrounded || Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
           // animator.SetTrigger("Jump");
            isGrounded = false;
            Jump();
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
           // animator.SetBool("InRun", true);

        }
        if (Input.GetAxis("Horizontal") != 0 && rb.velocity.x < 0f)
        {
            if (transform.localScale.x > 0f)
            {
                transform.localScale = Vector3.Scale(transform.localScale, new Vector3(-1, 1, 1));
            }
            //animator.SetBool("InRun", true);

        }
        if (rb.velocity.x == 0f)
        {
           // animator.SetBool("InRun", false);
        }


    }
   
    public void Swing()
    {
        if (enemy != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(enemy.transform.position, transform.position) < 4f)
            {
                Debug.Log("ide swing");
                animator.SetTrigger("Stab");
                Destroy(enemy.gameObject, 1.5f);
                // StartCoroutine(Destroythisguy());

            }
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
}