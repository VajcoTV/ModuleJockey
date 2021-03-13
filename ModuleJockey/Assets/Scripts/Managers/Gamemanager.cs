using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Gamemanager : MonoBehaviour
{
    public SceneAsset[] sceneassets;
    private void Awake()
    {
        app.gamemanager = this;
    }
    private void Start()
    {
        app.sceneloader.LoadScene("UI"); //nalouduj veci co sa maju
        

    }
    public void ChangeScene(string currentscene, string previousscene)
    {
        app.sceneloader.LoadScene(currentscene);
        app.sceneloader.UnloadScene(previousscene);
        app.datamanager.SaveGame();

    }
    public void NextLevel() //ked dokoncim level
    {
        if (app.datamanager.GetCurrentScene() < sceneassets.Length)
        {
            int previousscene = app.datamanager.gamedata.Currentscene;
            app.datamanager.gamedata.Currentscene++;
            ChangeScene("Layer1_level " + app.datamanager.gamedata.Currentscene, "Layer1_level " + previousscene);
        }
    }
    public void LoadTown() //ked idem z menu do loadu
    {
        app.sceneloader.LoadScene("Town");
    }
    public void LoadLevel() // ked idem z town do levelu
    {
        ChangeScene("Layer1_level " + app.datamanager.gamedata.Currentscene, "Town");
    }
    public void ReloadLevel()
    {

    }
}
