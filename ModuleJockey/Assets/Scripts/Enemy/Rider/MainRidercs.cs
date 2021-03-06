﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainRidercs : MonoBehaviour
{
    GameObject player;
    Animator animator;
    [Header("EnemyConfigs")]
    [SerializeField] int EnemyHealth = 10;
    [SerializeField] int DMG = 1;
    [SerializeField] float PlayerDistance = 3;
    [SerializeField] float HitPlayerDistance = 2;
    [SerializeField] int Armor = 1;
    [Header("References")]
    public GameObject DmgText;
    //if this bool true you are in normal dimension
    bool CurrentDimension = true;
    public GameObject ZelenyDuch;


    void Start()
    {
        //nastavovanie eventu pre dimenzie
        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
        //nastavovanie eventu pre dostavanie dmg
        Movement.attack1 += TakeDmg1;
        Movement.attack2 += TakeDmg2;
        Movement.attack3 += TakeDmg3;
        //ziskavanie referencii
        animator = GetComponent<Animator>();
        player = app.playermanager.player;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void PlayerNormalDimensionSwitch()
    {
        CurrentDimension = true;
        if (animator != null)
        {
            animator.SetBool("Switch", false);
        }

    }
    void PlayerMagicDimensionSwitch()
    {
        CurrentDimension = false;
        if (animator != null)
        {
            animator.SetBool("Switch", true);
        }
    }
    //ak je zavolany event v tomto pripade player swingne a sme od neho v urcitej vzdialenosti odpal funkciu zober si dmg 
    void TakeDmg1()
    {
        if (this != null)
        {

            if (Vector3.Distance(player.transform.position, transform.position) < PlayerDistance)
            {
                if (!CurrentDimension)
                {
                    TakeDamege(player.GetComponent<Movement>().swing1dmg);

                    Instantiate(ZelenyDuch, transform.position, Quaternion.identity);
                }
                else
                {
                    TakeDamege(player.GetComponent<Movement>().swing1dmg);
                }
            }
        }
    }
    void TakeDmg2()
    {
        if (this != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < PlayerDistance)
            {
                if (!CurrentDimension)
                {
                    TakeDamege(player.GetComponent<Movement>().swing2dmg);

                    Instantiate(ZelenyDuch, transform.position, Quaternion.identity);
                }
                else
                {
                    TakeDamege(player.GetComponent<Movement>().swing2dmg);
                }
            }
        }
    }
    void TakeDmg3()
    {
        if (this != null)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < PlayerDistance)
            {
                if (!CurrentDimension)
                {
                    TakeDamege(player.GetComponent<Movement>().swing3dmg);

                    Instantiate(ZelenyDuch, transform.position, Quaternion.identity);
                }
                else
                {
                    TakeDamege(player.GetComponent<Movement>().swing3dmg);
                }
            }
        }
    }
    //checkovanie toho ci sme dostali kudlov
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (CurrentDimension)
            {
                TakeDamege(player.GetComponent<Movement>().Knifedmg);
            }
            else
            {
                TakeDamege(player.GetComponent<Movement>().Knifedmg);

                Instantiate(ZelenyDuch, transform.position, Quaternion.identity);
            }


        }
    }
    //Funkcia na dostavanie dmg
    public void TakeDamege(int dmg)
    {
        EnemyHealth = EnemyHealth + Armor - dmg;
        dmg = dmg - Armor;
        var go = Instantiate(DmgText, transform.position, Quaternion.identity);
        go.GetComponent<TextMesh>().text = dmg.ToString();
        if (EnemyHealth <= 0)
        {
            Destroy(this.gameObject);
        }

    }
    public void EnemyDestroy()
    {
        Destroy(this.gameObject);
    }
    //funkcia na davanie dmg playerovi cez animacie Event key co sa odpali
    public void GiveDmg()
    {
        if (Vector3.Distance(player.transform.position, transform.position) <= HitPlayerDistance)
        {
            player.GetComponent<Movement>().TakeDmg(DMG);
        }
    }
}
