using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endoflevelmanager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            app.gamemanager.ChangeScene("Layer1_level " + app.datamanager.gamedata.Currentscene, "Layer1_level " + 0);
        }
    }
}
