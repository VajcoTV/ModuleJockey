using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainZelenyDuchCs : MonoBehaviour
{
    Animator animator;
    Transform Player;
    [SerializeField]int EnemyHealth = 10;
    GameObject player;
    public GameObject DmgText;

    void Start()
    {

        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
        Movement.attack1 += TakeDmg1;
        Movement.attack2 += TakeDmg2;
        Movement.attack3 += TakeDmg3;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    void PlayerNormalDimensionSwitch()
    {
        animator.SetBool("Switch", false);
    }
    void PlayerMagicDimensionSwitch()
    {
        animator.SetBool("Switch", true);
    }
    void TakeDmg1()
    {
        if (this != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3f)
            {
                TakeDamege(4);
            }
        }
    }
    void TakeDmg2()
    {
        if (this != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3f)
            {
               
                TakeDamege(2);
            }
        }
    }
    void TakeDmg3()
    {
        if (this != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 3f)
            {
          
                TakeDamege(4);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            TakeDamege(3);
        }
    }
    public void TakeDamege(int dmg)
    {
        EnemyHealth -= dmg;
        var go = Instantiate(DmgText, transform.position, Quaternion.identity, transform);
        go.GetComponent<TextMesh>().text = dmg.ToString();
        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }

}
