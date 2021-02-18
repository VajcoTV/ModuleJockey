using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class GameData 
{
    public int Currentscene;
    public string playername;
    public int[] player;
    public int DMG;
    public GameData()
    {
        
        Currentscene = 10;
        playername = "lmao";
       

    }
    public void SetDMG(int amount) //takto sa prepisuje niako 
    {
        DMG = amount;
        app.datamanager.SaveGame();
    }
    
}
