using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainZelenyDuchCs : MonoBehaviour
{
    Animator animator;
    Transform Player;
    
    void Start()
    {
        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
        animator = GetComponent<Animator>();
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
}
