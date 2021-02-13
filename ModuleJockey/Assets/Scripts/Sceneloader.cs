using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    Scene currentscene;
    Scene previousscene;
    
    private void Awake()
    {
        currentscene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(this.gameObject);
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentscene.buildIndex + 1);
    }
    public void LoadMainMenu()
    {
        previousscene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(0);
    }
    public void LoadMainMenufromTown()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadTown()
    {
        previousscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(1);
    }
   
    public void Quit()
    {
        Application.Quit();
    }
    public void ReloadScene()
    {
        currentscene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentscene.name);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player destroyer"))
        {
            ReloadScene();
        }
    }
    
}
