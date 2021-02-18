﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    StateMachine stateMachine = new StateMachine();
    public float rotationspeed;
    public float raymax;
    public float speed;
    public bool goright;
    public bool icanrun;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        stateMachine.ChangeState(new IdleState());
    }

    void Update()
    {
        Gumba();
        if (goright)
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, transform.right, raymax);

            if (hitinfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitinfo.point, Color.green);
                if (hitinfo.collider.CompareTag("Stena"))
                {
                    transform.eulerAngles = new Vector2(0, 180);
                    goright = false;
                }

                
            }
            Debug.DrawLine(transform.position, transform.position + transform.right * raymax, Color.red);
        }
        else if(!goright)
        {
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, Vector2.left, raymax);
            if (hitinfo.collider != null)
            {
                Debug.DrawLine(transform.position, hitinfo.point, Color.green);
                if (hitinfo.collider.CompareTag("Stena"))
                {
                    transform.eulerAngles = new Vector2(0, 0);
                    goright = true;
                }
            }

            Debug.DrawLine(transform.position, transform.position + transform.right * raymax, Color.red);

        }
    }
    public void Gumba()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    public void Test()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            app.datamanager.gamedata.SetDMG(10); //volanie a prepisanie ulozenie premenej
        }
    }
}
