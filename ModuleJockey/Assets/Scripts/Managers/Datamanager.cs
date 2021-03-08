using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Datamanager : MonoBehaviour
{
    public GameData gamedata;
    public string gamedatapath;
    private void Awake()
    {
        app.datamanager = this;
        gamedatapath = Application.persistentDataPath + "/GameData.json";
    }
    private void Start()
    {
        if(File.Exists(gamedatapath))
        {
            gamedata = LoadData<GameData>(Application.persistentDataPath + "/GameData.json");
        }else
        {
            gamedata = new GameData();
            SaveGame();

        }
    }
    public void SaveData(object data, string path)
    {
        string JsonData = JsonConvert.SerializeObject(data);
        try
        {
            File.WriteAllText(path, JsonData);
            
        }catch(System.Exception e)
        {
            Debug.Log(e);
        }
    }
    public T LoadData<T>(string path)
    {
        try
        {
            StreamReader reader = new StreamReader(path);
            string jsondata = reader.ReadToEnd();
            reader.Close();
            return JsonConvert.DeserializeObject<T>(jsondata);
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            return default;
        }
    }
    public void SaveGame() //ukladanie
    {
        SaveData(gamedata, gamedatapath);
    }
    public int GetCurrentScene()
    {
        return gamedata.Currentscene;
    }

}
