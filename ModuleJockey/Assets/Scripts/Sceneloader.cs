using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    public Scene[] sceneAssets;
    public int index;
    public void LoadScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
        
    }
    public void Save()
    {
      
    }
    public void Load()
    {
        
    }

}
