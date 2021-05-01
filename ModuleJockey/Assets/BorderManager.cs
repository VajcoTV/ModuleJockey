using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            app.gamemanager.ChangeScene("Map", "Layer1_level " + 0);
        }
    }
}
