using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameData 
{
    public int Currentscene;
    public float DMG;
    public float maxhealth;
    public float Armor;
    public GameData()
    {
        maxhealth = 100;
        Currentscene = 0;
        Armor = 0.5f;

    }
    public void SetDMG(int amount) //takto sa prepisuje niako 
    {
        DMG = amount;
        app.datamanager.SaveGame();
    }
    public void SetHealth(float enemydemage)
    {
        float AR = enemydemage * Armor;
        enemydemage -= AR;
        maxhealth -= enemydemage;

        //app.datamanager.SaveGame();

    }
    public void SetExp(int xp)
    {

    }
    
}
