
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public delegate void Attack1();
    public static event Attack1 attack1;
    public delegate void Attack2();
    public static event Attack2 attack2;
    public delegate void Attack3();
    public static event Attack3 attack3;
    [Header("References")]
    private Animator animator;
    CharacterController controller;
    [Header("Player Stats")]
    public float Health = 3;
    public int swing1dmg = 1;
    public int swing2dmg = 1;
    public int swing3dmg = 3;
    public int Knifedmg = 1;
    public int armor = 1;
    public int numberofenemy = 0;
    public int MaxNumberofenemy = 2;
    [Header("Combo")]
    public int noOfClicks = 0;
    float lastClickedTime = 0;
    public float maxComboDelay = 1.2f;
    [Header("Throw")]
    [SerializeField] Transform firePoint;
    [SerializeField] Rigidbody Knifepref;
    [SerializeField] float Knifeforce;
    public float blades;
    public float ThrowDelay = 1.2f;
    Vector3 destination;
    bool canthrow = true;
    [Header("Movement")]
    Vector3 slopeNormal;
    bool grounded;
    float verticalVelocity;
    bool canmove = true;
    [Header("Movement config")]
    [SerializeField] float speedX = 5;
    [SerializeField] float speedY = 5;
    [SerializeField] float gravity = 0.25f;
    [SerializeField] float jumpForce = 8.0f;
    [SerializeField] float terminalVelocity = 5.0f;
    [SerializeField] bool Jumping = false;
    [SerializeField] float Radius = 5f;
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
    private void FixedUpdate()
    {
       Jump();
    }
    //Update
    private void Update()
    {
        Swing();
        Throw();
        moving();
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jumping = true;
        }

    }
    //Jump
    public void Jump()
    {
        grounded = Grounded();
        animator.SetBool("Down", grounded);
        if (grounded)
        {


            verticalVelocity = -1;


            if (Jumping)
            {
                animator.SetTrigger("Jump");
                verticalVelocity += jumpForce; //nie
                slopeNormal = Vector3.up;
                Jumping = false;
            }
        }
        else
        {
            verticalVelocity -= gravity; //pochybujem
            slopeNormal = Vector3.up; //tu urcite nie
            if (verticalVelocity < -terminalVelocity)
                verticalVelocity = -terminalVelocity; //? mozno ale skor nie
        }
    }
    //cely movement
    public void moving()
    {
        Vector3 inputVector = PoolInput();
        Vector3 moveVector = new Vector3(inputVector.x * speedX, 0, inputVector.y * speedY);
        if (canmove)
        {
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
    }
    //Input Checker
    private Vector3 PoolInput()
    {
        Vector3 r = default(Vector3);
        r.x = Input.GetAxisRaw("Horizontal");
        r.y = Input.GetAxisRaw("Vertical");
        return (r.magnitude > 1) ? r.normalized : r;
       
    }
    //grounded checker
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

   //Logic Behind the Swing
    public void Swing()
    {
        if (Time.time - lastClickedTime > maxComboDelay)
        {
            noOfClicks = 0;
           
        }

        if (Input.GetKeyDown(KeyCode.E) && grounded)
        {
            lastClickedTime = Time.time;
            noOfClicks++;
            if (noOfClicks == 1)
            {
                animator.SetBool("Attack1", true);
                if (grounded)
                {
                    canmove = false;
                }
            }
            noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);
        }
    }
    //cheknutie inputu na zacatie korutiny pred throw nozika
    public void Throw()
    {
        if (Input.GetKeyDown(KeyCode.F) && canthrow && blades > 0)
        {
            StartCoroutine("CooldownThrow");
        }
    }
    //Combo system 
    //Combo system 
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
        canmove = true;
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Attack3", false);
        noOfClicks = 0;
    }
    //odpalenie eventov na sekanie
    public void Swing1()
    {
        if (attack1 != null)
        {
            attack1();
        }
    }
    public void Swing2()
    {
        if (attack2 != null)
        {
            attack2();
          
        }
    }
    public void Swing3()
    {
        if (attack3 != null)
        {
            attack3();
        }
    }
    //switch dimenzii
    public void PlayerNormalDimensionSwitch()
    {
        Debug.Log("Normalna dimenzia");

    }
    public void PlayerMagicDimensionSwitch()
    {
        Debug.Log("Magic dimenzia");
    }
    //hadzanie nozikom
    IEnumerator CooldownThrow()
    {
        var projectileInstance = Instantiate(Knifepref, firePoint.position, firePoint.rotation);
        projectileInstance.AddForce(firePoint.forward * Knifeforce);
        canthrow = false;
        blades -= 1;
       yield return new WaitForSeconds(ThrowDelay);
        canthrow = true;
        
    }
    //funkcia pouzivaju ju enemaci ked chcu poskodit playera
    public void TakeDmg(int dmg)
    {

        Health = Health + armor - dmg;
        if(Health <= 0)
        {
            Debug.Log("Player ded");
        }
    }
    public Vector3 PlayerRadius(float yaxis)
    {
        Vector2 playerradius = Random.insideUnitCircle * Radius;
      
        return new Vector3(playerradius.x + transform.position.x, yaxis, playerradius.y + transform.position.z);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, Radius);
    }
    IEnumerator AttackUnStuck()
    {
       
        yield return new WaitForSeconds(1.2f);
        return3();

    }




}