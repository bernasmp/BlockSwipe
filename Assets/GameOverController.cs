using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public bool dead = false;
    public bool shield = false;
    public bool shieldToggle = false;
    bool shieldIconActive = false;
    public GameObject shieldIcon;
    public int points = 0;
    public TextMeshProUGUI pointsText;
    public LevelController lc;
    public int portalSpawn1 = 10;
    public int portalSpawn2 = 10;
    public int portalActivated = 0;
    public Vector3[] portalSpots;
    public GameObject mainSquare;
    public GameObject mainSquareLight;
    bool animDone = false;
    public float darkTimer = 0f;
    bool debugDarkToggle = false;
    public float goldTimer = 0f;
    bool debugGoldToggle = false;
    public int goldMultiplier = 1;
    public int normalPoints = 1;
    public int shieldPoints = 3;
    public int portalPoints = 2;
    public float darkTimeForOrb = 10f;
    public float goldTimeForOrb = 15f;
    public int interactionPoints;

    //////////////////Serialized variables //////////////////////////////
    public int highscore;
    public int emeralds;
    public bool shieldUnlocked;
    public bool portalUnlocked;
    public bool darkUnlocked;
    public bool goldUnlocked;
    public int shieldLvl = 0;
    public int portalLvl = 0;
    public int darkLvl = 0;
    public int goldLvl = 0;
    
    public GameObject sceneTransitioner;
    public GameObject gameOverBackground;
    public GameObject gameOverScore;
    public TextMeshProUGUI gameOverScoreText;
    public GameObject gameOverHighscore;
    public TextMeshProUGUI gameOverHighscoreText;
    public GameObject gameOverContinue;
    public GameObject gameOverRestart;
    public GameObject gameOverHome;
    public GameObject gameOverNoThanks;
    public TextMeshProUGUI emeraldsText;
    public GameObject emeraldsGameObject;
    Color32 transitionNormalColor;
    Color32 transitionNoAlphaColor;
    public string currentScene;
    bool transitionDisabled = false;
    bool emeraldAnimDone = true;
    public bool unshielded = false;
    public LevelPlayAds lpa;
    public bool continueToggle = false;
    bool firstRun = true;
    int continuePoints = 0;
    int startingEmeralds;
    bool startingEmeraldsUpdated = false;

    //public GameOverController instance;

    // Start is called before the first frame update
    void Start() {
        currentScene = SceneManager.GetActiveScene().name;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 300;
        LoadGoc();
        transitionNoAlphaColor = new Color32(25, 25, 25, 0);
        transitionNormalColor = new Color32(25, 25, 25, 255);
        if (currentScene == "MainGame") {
            InsertOrbValues();
            shieldIcon.transform.localScale = new Vector3(0, 0, 0);
            emeraldsText.text = emeralds.ToString();
            emeraldsGameObject.SetActive(false);
        }
        lpa.LoadInterstitial();
    }

    // Update is called once per frame
    void Update() {
        if (lpa.rewardedVideoFinished && continueToggle) {
            MoveAllOrbs();
            StartCoroutine("ReviveAnimation");
            continueToggle = false;
            lpa.rewardedVideoFinished = false;
        }
        if (currentScene == "MainGame") {
            if (emeraldAnimDone && emeraldsGameObject.activeSelf == true) {
                print("hello?");
                emeraldsText.text = emeralds.ToString();
                emeraldAnimDone = false;
            }
            if (!transitionDisabled) {
                sceneTransitioner.GetComponent<Image>().color = transitionNormalColor;
                sceneTransitioner.GetComponent<Image>().DOColor(transitionNoAlphaColor, 0.5f);
                StartCoroutine("MainMenuDisableTransition");
                transitionDisabled = true;
            }
            if (firstRun) {
                continuePoints = points;
            }
            pointsText.text = points.ToString();
            gameOverScoreText.text = points.ToString();
            gameOverHighscoreText.text = highscore.ToString();
            if (points > highscore) {
                highscore = points;
            }

            if (darkTimer > 0) {
                debugDarkToggle = true;
                darkTimer -= Time.deltaTime;
            }

            if (debugDarkToggle && darkTimer <= 0) {
                darkTimer = 0;
                debugDarkToggle = false;
            }

            if (goldTimer > 0) {
                goldMultiplier = 2;
                debugGoldToggle = true;
                goldTimer -= Time.deltaTime;
            }

            if (debugGoldToggle && goldTimer <= 0) {
                goldMultiplier = 1;
                goldTimer = 0;
                debugGoldToggle = false;
            }

            if (dead) {
                if (!animDone) {
                    StartCoroutine("FadeLight");
                    StartCoroutine("DeathAnimation");
                    StartCoroutine("RestartGame");
                    animDone = true;
                }
            }
            if (shield && !shieldToggle) {
                shieldIcon.SetActive(true);
                shieldToggle = true;
                shieldIconActive = true;
                StartCoroutine("ShieldIconAnim");
            } else if (!shield && !shieldToggle && shieldIconActive) {
                shieldToggle = true;
                shieldIconActive = false;
                StartCoroutine("ShieldIconAnimRev");
            }
        } else if (currentScene == "MainMenu") {
            emeraldsText.text = emeralds.ToString();
            if (emeraldAnimDone) {
                emeraldsText.text = emeralds.ToString();
                emeraldAnimDone = false;
            }
            if (!transitionDisabled) {
                sceneTransitioner.GetComponent<Image>().color = transitionNormalColor;
                sceneTransitioner.GetComponent<Image>().DOColor(transitionNoAlphaColor, 0.5f);
                StartCoroutine("MainMenuDisableTransition");
                transitionDisabled = true;
            }
        }
    }

    public void SaveGoc() {
        SaveSystem.SaveGoc(this);
    }

    public void LoadGoc() {
        PlayerData data = SaveSystem.LoadGoc();
        if (data != null) {
            highscore = data.highscore;
            emeralds = data.emeralds;
            shieldUnlocked = data.shieldUnlocked;
            portalUnlocked = data.portalUnlocked;
            darkUnlocked = data.darkUnlocked;
            goldUnlocked = data.goldUnlocked;
            shieldLvl = data.shieldLvl;
            portalLvl = data.portalLvl;
            darkLvl = data.darkLvl;
            goldLvl = data.goldLvl;
        }
    }

    IEnumerator RestartGame() {
        yield return new WaitForSeconds(2f);
        StartCoroutine("GameOverAnim");
    }

    IEnumerator ShieldIconAnim() {
        shieldIcon.transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.4f, 0.4f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.9f, 0.9f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(1.15f, 1.15f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(1f, 1f, 0);
    }

    IEnumerator ShieldIconAnimRev() {
        shieldIcon.transform.localScale = new Vector3(1f, 1f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(1.15f, 1.15f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(1.3f, 1.3f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.9f, 0.9f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.4f, 0.4f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0.1f, 0.1f, 0);
        yield return new WaitForSeconds(0.016f);
        shieldIcon.transform.localScale = new Vector3(0, 0, 0);
        shieldIcon.SetActive(false);
    }

    IEnumerator DeathAnimation() {
        yield return new WaitForSeconds(1);
        mainSquare.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.78f, 0.78f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.76f, 0.76f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.72f, 0.72f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.45f, 0.45f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.12f, 0.12f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.03f, 0.03f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0, 0, 1);
    }

    IEnumerator ReviveAnimation() {
        Color32 alphaColor = new Color32(25, 25, 25, 0);
        Color32 noThanksAlphaColor = new Color32(255, 255, 255, 0);
        gameOverHome.SetActive(false);
        emeraldsGameObject.SetActive(false);
        gameOverNoThanks.GetComponent<TextMeshProUGUI>().DOColor(noThanksAlphaColor, 0.05f);
        yield return new WaitForSeconds(0.05f);
        gameOverNoThanks.SetActive(false);
        gameOverContinue.transform.DOLocalMoveX(1500f, 0.05f, false);
        yield return new WaitForSeconds(0.05f);
        gameOverHighscore.transform.DOLocalMoveX(-1500f, 0.05f, false);
        yield return new WaitForSeconds(0.05f);
        gameOverScore.transform.DOLocalMoveX(1500f, 0.05f, false);
        yield return new WaitForSeconds(0.05f);
        gameOverBackground.GetComponent<Image>().DOColor(alphaColor, 0.05f);
        // --- //
        yield return new WaitForSeconds(1);
        mainSquare.transform.localScale = new Vector3(0, 0, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.03f, 0.03f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.12f, 0.12f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.45f, 0.45f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.72f, 0.72f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.76f, 0.76f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.78f, 0.78f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.8f, 0.8f, 1);
        yield return new WaitForSeconds(0.016f);
        mainSquare.transform.localScale = new Vector3(0.75f, 0.75f, 1);
        dead = false;
        animDone = false;
    }

    IEnumerator FadeLight() {
        Light light = mainSquareLight.GetComponent<Light>();
        yield return new WaitForSeconds(1f);
        light.range += 0.1f;
        yield return new WaitForSeconds(0.016f);
        light.range += 0.2f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.1f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.1f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.1f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.2f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.2f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.2f;
        yield return new WaitForSeconds(0.016f);
        light.range -= 0.24f;
    }

    IEnumerator GameOverAnim() {
        lpa.ShowInterstitial();
        Color32 normalColor = new Color32(25, 25, 25, 255);
        Color32 noThanksNormalColor = new Color32(255, 255, 255, 36);
        gameOverBackground.GetComponent<Image>().DOColor(normalColor, 0.1f);
        yield return new WaitForSeconds(0.1f);
        gameOverHome.SetActive(true);
        emeraldsGameObject.SetActive(true);
        //yield return new WaitForSeconds(0.2f);
        gameOverScore.transform.DOLocalMoveX(0f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);
        gameOverHighscore.transform.DOLocalMoveX(0f, 0.3f, false);
        yield return new WaitForSeconds(0.3f);
        if (firstRun) {
            gameOverRestart.transform.DOLocalMoveX(1500f, 0.016f, false);
            gameOverContinue.transform.DOLocalMoveX(1500f, 0.016f, false);
            gameOverContinue.transform.DOLocalMoveX(0f, 0.3f, false);
        } else {
            gameOverRestart.transform.DOLocalMoveX(1500f, 0.016f, false);
            gameOverContinue.transform.DOLocalMoveX(1500f, 0.016f, false);
            gameOverRestart.transform.DOLocalMoveX(0f, 0.3f, false);
        }
        if (firstRun) {
            AddEmeralds(points);
            firstRun = false;
        } else {
            AddEmeralds(points - continuePoints);
        }
        //AgentCopy();
        yield return new WaitForSeconds(3f);
        gameOverNoThanks.SetActive(true);
        gameOverNoThanks.GetComponent<TextMeshProUGUI>().DOColor(noThanksNormalColor, 0.2f);
    }

    public void NoThanks() {
        StartCoroutine("NoThanksCoroutine");
    }

    IEnumerator NoThanksCoroutine() {
        gameOverContinue.transform.DOLocalMoveX(-1500f, 1f, false);
        gameOverRestart.transform.DOLocalMoveX(0f, 1f, false);
        yield return new WaitForSeconds(0.1f);
    }

    public IEnumerator RestartCurrentSceneCoroutine() {
        //sceneTransitioner.SetActive(true);
        sceneTransitioner.GetComponent<Image>().DOColor(transitionNormalColor, 0.1f);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RestartCurrentScene() {
        StartCoroutine("RestartCurrentSceneCoroutine");
    }

    public void StartGame() {
        StartCoroutine("StartGameCoroutine");
    }

    public void LoadMainMenu() {
        StartCoroutine("LoadMainMenuCoroutine");
    }

    public IEnumerator LoadMainMenuCoroutine() {
        //sceneTransitioner.SetActive(true);
        sceneTransitioner.GetComponent<Image>().DOColor(transitionNormalColor, 0.1f);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(0);
    }

    public IEnumerator StartGameCoroutine() {
        //sceneTransitioner.SetActive(true);
        sceneTransitioner.GetComponent<Image>().DOColor(transitionNormalColor, 0.1f);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }

    public IEnumerator MainMenuDisableTransition() {
        yield return new WaitForSeconds(0.6f);
        //sceneTransitioner.SetActive(false);
    }

    //void SaveGame() {
        //ZSerialize.SaveScene();
    //}

    void AddEmeralds(int amount) {
        startingEmeralds = emeralds;
        emeralds += amount;
        SaveGoc();
        StartCoroutine(AddEmeraldsCoroutine(emeralds));
    }

    IEnumerator AddEmeraldsCoroutine(int max) {
        float delay = 0.032f;
        int sum;
        yield return new WaitForSeconds(0.5f);
        for (int i = startingEmeralds; i < max; i++) {
            sum = int.Parse(emeraldsText.text) + 1;
            emeraldsText.text = sum.ToString();
            yield return new WaitForSeconds(delay);
        }
        startingEmeralds = emeralds;
        emeraldAnimDone = true;
    }

    void InsertOrbValues() {
        if (shieldUnlocked) {
            shieldPoints = 2 * shieldLvl + 3;
        }
        if (portalUnlocked) {
            portalPoints = portalLvl + 2;
        }
        if (darkUnlocked) {
            darkTimeForOrb = 5 * darkLvl + 10;
        }
        if (goldUnlocked) {
            goldTimeForOrb = 5 * goldLvl + 15;
        }
    }

    void MoveAllOrbs() {
        // get root objects in scene
        List<GameObject> rootObjects = new List<GameObject>();
        Scene scene = SceneManager.GetActiveScene();
        scene.GetRootGameObjects(rootObjects);

        // iterate root objects and do something
        for (int i = 0; i < rootObjects.Count; ++i) {
            GameObject thing = rootObjects[i];
            if (thing.tag == "Point" || thing.tag == "Bullet" || thing.tag == "Mega") {
                thing.transform.position = new Vector3(-8f, 4.3f, -1);
            } else if (thing.tag == "NucleusSpotThing") {
                thing.transform.position = new Vector3(-8f, 20f, -1);
            }
        }
    }
}
