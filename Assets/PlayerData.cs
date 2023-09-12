using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int highscore;
    public int emeralds;
    public bool shieldUnlocked;
    public bool portalUnlocked;
    public bool darkUnlocked;
    public bool goldUnlocked;
    public int shieldLvl;
    public int portalLvl;
    public int darkLvl;
    public int goldLvl;

    public PlayerData (GameOverController goc) {
        highscore = goc.highscore;
        emeralds = goc.emeralds;
        shieldUnlocked = goc.shieldUnlocked;
        portalUnlocked = goc.portalUnlocked;
        darkUnlocked = goc.darkUnlocked;
        goldUnlocked = goc.goldUnlocked;
        shieldLvl = goc.shieldLvl;
        portalLvl = goc.portalLvl;
        darkLvl = goc.darkLvl;
        goldLvl = goc.goldLvl;
    }

}
