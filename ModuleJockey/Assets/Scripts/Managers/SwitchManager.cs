using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchManager : MonoBehaviour
{
    public delegate void NormalDimensionswitch();
    public static event NormalDimensionswitch NormalSwitch;
    public delegate void MagicDimensionSwitch();
    public static event MagicDimensionSwitch MagicSwitch;
    public bool CurrentDimension = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CurrentDimension)
        {
            CurrentDimension = !CurrentDimension;
            if(NormalSwitch != null)
            {
                NormalSwitch();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Q) && !CurrentDimension)
        {
            CurrentDimension = !CurrentDimension;
            if(MagicSwitch != null)
            {
                MagicSwitch();
            }
        }
      
    }
    private void Start()
    {
        NormalSwitch();
       CurrentDimension = !CurrentDimension;
    }
}
