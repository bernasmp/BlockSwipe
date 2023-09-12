using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class AbilitiesPageController : MonoBehaviour
{
    public GameOverController goc;
    public Transform pointsPopUp;
    public TextMeshProUGUI noEmeralds;
    public TextMeshProUGUI unlockFirst;
    public GameObject shieldText;
    public GameObject shieldUnlockButton;
    public GameObject shieldBuyButton;
    public GameObject shieldCircles;
    public GameObject portalText;
    public GameObject portalUnlockButton;
    public GameObject portalBuyButton;
    public GameObject portalCircles;
    public GameObject darkText;
    public GameObject darkUnlockButton;
    public GameObject darkBuyButton;
    public GameObject darkCircles;
    public GameObject goldText;
    public GameObject goldUnlockButton;
    public GameObject goldBuyButton;
    public GameObject goldCircles;
    public MainMenuController mmc;
    bool abilityPageDone = false;

    public void Update() {
        if (mmc.abilitiesOpened && !abilityPageDone) {
            if (goc.shieldUnlocked) {
                shieldBuyButton.SetActive(true);
                shieldText.SetActive(true);
                shieldUnlockButton.SetActive(false);
                for (int i = goc.shieldLvl; i >= 0; i--) {
                    shieldCircles.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            if (goc.portalUnlocked) {
                portalBuyButton.SetActive(true);
                portalText.SetActive(true);
                portalUnlockButton.SetActive(false);
                for (int i = goc.portalLvl; i >= 0; i--) {
                    portalCircles.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            if (goc.darkUnlocked) {
                darkBuyButton.SetActive(true);
                darkText.SetActive(true);
                darkUnlockButton.SetActive(false);
                for (int i = goc.darkLvl; i >= 0; i--) {
                    darkCircles.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            if (goc.goldUnlocked) {
                goldBuyButton.SetActive(true);
                goldText.SetActive(true);
                goldUnlockButton.SetActive(false);
                for (int i = goc.goldLvl; i >= 0; i--) {
                    goldCircles.transform.GetChild(i).gameObject.SetActive(true);
                }
            }
            abilityPageDone = true;
        }
    }

    public void AbilityUnlock(int ability) {
        if (goc.emeralds >= 250) {
            if (ability == 0) {
                goc.shieldUnlocked = true;
                goc.emeralds -= 250;
                SaveSystem.SaveGoc(goc);
            } else if (ability == 1) {
                if (goc.shieldUnlocked) {
                    goc.portalUnlocked = true;
                    goc.emeralds -= 250;
                    SaveSystem.SaveGoc(goc);
                } else {
                    StopAllCoroutines();
                    UnlockFirst("Shield Orb");
                }
            } else if (ability == 2) {
                if (goc.portalUnlocked) {
                    goc.darkUnlocked = true;
                    goc.emeralds -= 250;
                    SaveSystem.SaveGoc(goc);
                } else {
                    StopAllCoroutines();
                    UnlockFirst("Portal Orb");
                }
            } else if (ability == 3) {
                if (goc.darkUnlocked) {
                    goc.goldUnlocked = true;
                    goc.emeralds -= 250;
                    SaveSystem.SaveGoc(goc);
                } else {
                    StopAllCoroutines();
                    UnlockFirst("Dark Orb");
                }
            }
            abilityPageDone = false;
        } else {
            StopAllCoroutines();
            StartCoroutine("NoEmeralds");
        }
    }

    public void AbilityLevelUp(int ability) {
        if (goc.emeralds >= 500) {
            if (ability == 0) {
                if (goc.shieldLvl < 2) {
                    goc.shieldLvl += 1;
                    goc.emeralds -= 500;
                    SaveSystem.SaveGoc(goc);
                } else {
                    AlreadyMaxLevel();
                }
            } else if (ability == 1) {
                if (goc.portalLvl < 2) {
                    goc.portalLvl += 1;
                    goc.emeralds -= 500;
                    SaveSystem.SaveGoc(goc);
                } else {
                    AlreadyMaxLevel();
                }
            } else if (ability == 2) {
                if (goc.darkLvl < 2) {
                    goc.darkLvl += 1;
                    goc.emeralds -= 500;
                    SaveSystem.SaveGoc(goc);
                } else {
                    AlreadyMaxLevel();
                }
            } else if (ability == 3) {
                if (goc.goldLvl < 2) {
                    goc.goldLvl += 1;
                    goc.emeralds -= 500;
                    SaveSystem.SaveGoc(goc);
                } else {
                    AlreadyMaxLevel();
                }
            }
            abilityPageDone = false;
        } else {
            StopAllCoroutines();
            StartCoroutine("NoEmeralds");
        }
    }

    IEnumerator NoEmeralds() {
        Color32 alphaColor = new Color32(255, 255, 255, 0);
        Color32 normalColor = new Color32(255, 255, 255, 170);
        noEmeralds.DOColor(normalColor, 0.5f);
        yield return new WaitForSeconds(2f);
        noEmeralds.DOColor(alphaColor, 0.5f);
    }

    void UnlockFirst(string abilityName) {
        unlockFirst.SetText("Unlock " + abilityName + " First");
        StartCoroutine("UnlockFirstCo");
    }

    IEnumerator UnlockFirstCo() {
        Color32 alphaColor = new Color32(255, 255, 255, 0);
        Color32 normalColor = new Color32(255, 255, 255, 170);
        unlockFirst.DOColor(normalColor, 0.5f);
        yield return new WaitForSeconds(2f);
        unlockFirst.DOColor(alphaColor, 0.5f);
    }

    void AlreadyMaxLevel() {
        unlockFirst.SetText("Already Max Level");
        StartCoroutine("UnlockFirstCo");
    }
}
