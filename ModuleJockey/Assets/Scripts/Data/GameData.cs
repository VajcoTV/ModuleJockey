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
    public int Armor;
    public GameData()
    {
        health = 100;
        Currentscene = 10;
        Armor = 0;

    }
    public void SetDMG(int amount) //takto sa prepisuje niako 
    {
        DMG = amount;
        app.datamanager.SaveGame();
    }
    public void SetHealth(int amount)
    {
       amount = amount - Armor;
        if(amount < Armor)
        {
            health -= amount;
        }
        else
        {
            health -= amount;
        }
        app.datamanager.SaveGame();
    }
    
}
