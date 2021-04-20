
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("References")]
    private Animator animator;
    CharacterController controller;
    [Header("Health")]
    public float Health = 3;
    [Header("Combo")]
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;
    [Header("Movement")]
    Vector3 slopeNormal;
    bool grounded;
    float verticalVelocity;
    [Header("Movement config")]
    [SerializeField] float speedX = 5;
    [SerializeField] float speedY = 5;
    [SerializeField] float gravity = 0.25f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] float terminalVelocity = 5.0f;
    [Header("Raycast")]
    [SerializeField] float extremitiesOffset = 0.05f;
    [SerializeField] float innerVerticalOffset = 0.25f;
    [SerializeField] float distanceGrounded = 0.15f;
    [SerializeField] float slopeThreshold = 0.55f;


    //Awake
    void Awake()
    {

        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
    }
    //Start
    void Start()
    {
        
    }
    //Update
    private void Update()
    {
        Swing();

        Vector3 inputVector = PoolInput();
        Vector3 moveVector = new Vector3(inputVector.x * speedX, 0, inputVector.y * speedY);
        grounded = Grounded();
        animator.SetBool("Down", grounded);
        if (grounded)
        {
            

            verticalVelocity = -1;


            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetTrigger("Jump");
                verticalVelocity = jumpForce;
                slopeNormal = Vector3.up;
            }
        }
        else
        {
            verticalVelocity -= gravity;
            slopeNormal = Vector3.up;
            if (verticalVelocity < -terminalVelocity)
                verticalVelocity = -terminalVelocity;
        }
        moveVector.y = verticalVelocity;
        if (slopeNormal != Vector3.up) moveVector = FollowFloor(moveVector);
        controller.Move(moveVector * Time.deltaTime);
        float r = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if (r + v == 0 && v != -1 && r != -1)
        {
            animator.SetBool("Run", false);
        }
        else
        {
            animator.SetBool("Run", true);
        }
        if (r < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (r > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    //FixedUpdate
    private void FixedUpdate()
    {
       
    }
    private Vector3 PoolInput()
    {
        Vector3 r = default(Vector3);
        r.x = Input.GetAxisRaw("Horizontal");
        r.y = Input.GetAxisRaw("Vertical");
        return (r.magnitude > 1) ? r.normalized : r;
       
    }
    public bool Grounded()
    {
        if (verticalVelocity > 0)
            return false;

        float yRay = (controller.bounds.center.y - (controller.height * 0.5f)) // Bottom of the character controller
                     + innerVerticalOffset;

        RaycastHit hit;
       

        // Mid
        if (Physics.Raycast(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            Debug.DrawRay(new Vector3(controller.bounds.center.x, yRay, controller.bounds.center.z), -Vector3.up * (innerVerticalOffset + distanceGrounded), Color.red);
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Front-Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Front-Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z + (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Back Right
        if (Physics.Raycast(new Vector3(controller.bounds.center.x + (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }
        // Back Left
        if (Physics.Raycast(new Vector3(controller.bounds.center.x - (controller.bounds.extents.x - extremitiesOffset), yRay, controller.bounds.center.z - (controller.bounds.extents.z - extremitiesOffset)), -Vector3.up, out hit, innerVerticalOffset + distanceGrounded))
        {
            slopeNormal = hit.normal;
            return (slopeNormal.y > slopeThreshold) ? true : false;
        }

        return false;
    }
    private Vector3 FollowFloor(Vector3 moveVector)
    {
        Vector3 right = new Vector3(slopeNormal.y, -slopeNormal.x, 0).normalized;
        Vector3 forward = new Vector3(0, -slopeNormal.z, slopeNormal.y).normalized;
        return right * moveVector.x + forward * moveVector.z;
    }

    //OnCollisionEnter
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "playercollider")
        {
            
           
        }
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
    public void PlayerNormalDimensionSwitch()
    {
        Debug.Log("Normalna dimenzia");

    }
    public void PlayerMagicDimensionSwitch()
    {
        Debug.Log("Magic dimenzia");
    }
   

    
}