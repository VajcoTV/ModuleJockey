using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Datamanager : MonoBehaviour
{
    public GameData gamedata;
    private void Awake()
    {
        app.datamanager = this;
    }
    private void Start()
    {
        gamedata = new GameData();
    }

}
