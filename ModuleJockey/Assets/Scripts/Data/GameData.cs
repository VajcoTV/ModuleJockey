using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameData 
{
    public int Currentscene;
    public int DMG;
    public int health;
    public GameData()
    {
        health = 100;
        Currentscene = 10;

    }
    public void SetDMG(int amount) //takto sa prepisuje niako 
    {
        DMG = amount;
        app.datamanager.SaveGame();
    }
    public void SetHealth(int amount)
    {
        health -= amount;
        app.datamanager.SaveGame();
    }
    
}
