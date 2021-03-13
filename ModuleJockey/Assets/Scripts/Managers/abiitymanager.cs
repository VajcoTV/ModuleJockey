using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abiitymanager : MonoBehaviour
{
    [Header("ability 1")]
    public Image image1;
    public float imageC1;
    bool isCooldown1 = false;
    public KeyCode ability1;
    [Header("ability 2")]
    public Image image2;
    public float imageC2;
    bool isCooldown2 = false;
    public KeyCode ability2;
    [Header("ability 3")]
    public Image image3;
    public float imageC3;
    bool isCooldown3 = false;
    public KeyCode ability3;
    private void Start()
    {
        image1.fillAmount = 0;
        image2.fillAmount = 0;
        image3.fillAmount = 0;
    }
    void Update()
    {
        Ability1();
        Ability2();
        Ability3();
    }
    void Ability1()
    {
        if(Input.GetKey(ability1) && isCooldown1 == false)
        {
            app.datamanager.gamedata.SetHealth(20);
            isCooldown1 = true;
            image1.fillAmount = 1;
        }
        if(isCooldown1)
        {
            image1.fillAmount -= 1 / imageC1 * Time.deltaTime;
            if(image1.fillAmount <=0)
            {
                image1.fillAmount = 0;
                isCooldown1 = false;
            }
        }
    }
    void Ability2()
    {
        if (Input.GetKey(ability2) && isCooldown2 == false)
        {
            app.datamanager.gamedata.SetHealth(30);
            isCooldown2 = true;
            image2.fillAmount = 1;
        }
        if (isCooldown2)
        {
            image2.fillAmount -= 1 / imageC2 * Time.deltaTime;
            if (image2.fillAmount <= 0)
            {
                image2.fillAmount = 0;
                isCooldown2 = false;
            }
        }
    }
    void Ability3()
    {
        if (Input.GetKey(ability3) && isCooldown3 == false)
        {
            app.datamanager.gamedata.SetHealth(5);
            isCooldown3 = true;
            image3.fillAmount = 1;
        }
        if (isCooldown3)
        {
            image3.fillAmount -= 1 / imageC3 * Time.deltaTime;
            if (image3.fillAmount <= 0)
            {
                image3.fillAmount = 0;
                isCooldown3 = false;
            }
        }
    }
}
