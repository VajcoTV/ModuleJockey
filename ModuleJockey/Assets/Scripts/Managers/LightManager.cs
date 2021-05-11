using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [SerializeField] Color Ncolor;
    [SerializeField] Color Ccolor;
    public UnityEngine.Experimental.Rendering.Universal.Light2D SUN;

    private void Start()
    {
        
        SwitchManager.NormalSwitch += PlayerNormalDimensionSwitch;
        SwitchManager.MagicSwitch += PlayerMagicDimensionSwitch;
        SUN = GetComponent<UnityEngine.Experimental.Rendering.Universal.Light2D>();
    }

    void PlayerNormalDimensionSwitch()
    {
        SUN.color = Ncolor;
    }
    void PlayerMagicDimensionSwitch()
    {
        SUN.color = Ccolor;
    }
}
