using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    
    public GameObject NormalFloor;
    public GameObject NormalBG;
    public GameObject MagicFloor;
    public GameObject MagicBG;


    void Start()
    {
        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
        
    }
    public void PlayerNormalDimensionSwitch()
    {
        NormalBG.SetActive(true);
        NormalFloor.SetActive(true);
        MagicBG.SetActive(false);
        MagicFloor.SetActive(false);

    }
    public void PlayerMagicDimensionSwitch()
    {
        NormalBG.SetActive(false);
        NormalFloor.SetActive(false);
        MagicBG.SetActive(true);
        MagicFloor.SetActive(true);
    }


}
