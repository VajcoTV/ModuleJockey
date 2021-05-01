using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject playerpref;
    public GameObject player;
    public GameObject PlayerSpawner;
    
    private void Awake()
    {
        app.playermanager = this;
        player = Instantiate(playerpref, PlayerSpawner.transform.position, Quaternion.identity);
        player.transform.SetParent(PlayerSpawner.transform);

    }
  


}
