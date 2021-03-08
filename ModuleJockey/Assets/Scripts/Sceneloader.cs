using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sceneloader : MonoBehaviour
{
    [SerializeField] Texture2D cursor;
    [SerializeField] Texture2D cursorondown;
    private void Awake()
    {
        app.sceneloader = this;
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
    public void LoadScene(string scenename)
    {

        StartCoroutine(LoadScenecorutine(scenename));
    }
    public IEnumerator LoadScenecorutine(string scenename)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(scenename, LoadSceneMode.Additive);
        async.allowSceneActivation = false;
        while (async.isDone == false)
        {
            if(async.progress >= 0.9f && async.allowSceneActivation == false)
            {
                async.allowSceneActivation = true;
                
            }
            yield return null;
        }
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scenename));
    }
    public IEnumerator UnLoadScenecorutine(string scenename)
    {
        AsyncOperation async = SceneManager.UnloadSceneAsync(scenename);
        while (async.isDone == false)
        {
            yield return null;
        }
       
    }
    public void UnloadScene(string scenename)
    {
        StartCoroutine(UnLoadScenecorutine(scenename));
    }
 



}
