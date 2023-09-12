using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuController : MonoBehaviour
{
    public GameObject BackButton;
    public GameObject Background;
    public GameObject AchievementsPage;
    public GameObject AbilitiesPage;
    public GameObject InfoPage;
    public GameObject StorePage;
    public bool abilitiesOpened = false;

    public void OpenAbilities() {
        StartCoroutine("OpenAbilitiesCo");
    }

    public void CloseAbilities() {
        StartCoroutine("CloseAbilitiesCo");
    }

    IEnumerator OpenAbilitiesCo() {
        abilitiesOpened = true;
        Color32 normalColor = new Color32(25, 25, 25, 255);
        Background.GetComponent<Image>().DOColor(normalColor, 0.1f);
        yield return new WaitForSeconds(0.1f);
        BackButton.SetActive(true);
        AbilitiesPage.transform.DOLocalMoveX(0f, 0.5f, false);
    }

    IEnumerator CloseAbilitiesCo() {
        Color32 alphaColor = new Color32(25, 25, 25, 0);
        AbilitiesPage.transform.DOLocalMoveX(1500f, 0.5f, false);
        yield return new WaitForSeconds(0.2f);
        BackButton.SetActive(false);
        Background.GetComponent<Image>().DOColor(alphaColor, 0.3f);
    }
}
