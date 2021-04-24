using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerpref;
    public GameObject player;
    
    private void Awake()
    {
        app.playermanager = this;
        player = Instantiate(playerpref, new Vector3(45, 15, 20), Quaternion.identity);
        Debug.Log(player);
    }

}
