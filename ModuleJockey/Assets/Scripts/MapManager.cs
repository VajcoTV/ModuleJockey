using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
  public void LoadLevel()
    {
        app.sceneloader.UnloadScene("Map");
        app.sceneloader.LoadScene("Layer1_level 0");
     


    }
}
