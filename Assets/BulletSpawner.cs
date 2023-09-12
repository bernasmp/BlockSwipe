using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bullet;
    public GameObject point;
    public GameObject shield;
    public GameObject portal;
    public GameObject dark;
    public GameObject gold;
    public Vector3[] spawnPoints;
    public GameObject[] squareSpots;
    public int chosenPoint;
    public BulletSquareController bsc;
    public PointSquareController psc;
    public ShieldSquareController ssc;
    public NucleusSquareController nsc;
    public DarkSquareController dsc;
    public GoldSquareController gsc;
    public LevelController lc;
    public GameOverController goc;
    public float spawnTime;
    public float timeSinceLastSpawn;
    int prev;
    public bool spawnChangeReady = false;

    int randomNum;
    int type;
    public int bulletUnits = 0;
    public int bulletUnitsMax = 4;
    public GameObject[] megaOrbs;
    public GameObject[] portalOrbs;

    // Start is called before the first frame update
    void Start()
    {
        spawnTime = lc.spawnTime;
        prev = 0;
        bsc = bullet.GetComponent<BulletSquareController>();
        spawnPoints[0] = new Vector3(squareSpots[0].transform.position.x, 6f, -1f);
        spawnPoints[1] = new Vector3(squareSpots[1].transform.position.x, 6f, -1f);
        spawnPoints[2] = new Vector3(squareSpots[2].transform.position.x, -6f, -1f);
        spawnPoints[3] = new Vector3(squareSpots[3].transform.position.x, -6f, -1f);
        spawnPoints[4] = new Vector3(-4f, squareSpots[0].transform.position.y, -1f);
        spawnPoints[5] = new Vector3(-4f, squareSpots[2].transform.position.y, -1f);
        spawnPoints[6] = new Vector3(4f, squareSpots[1].transform.position.y, -1f);
        spawnPoints[7] = new Vector3(4f, squareSpots[3].transform.position.y, -1f);

        //InvokeRepeating("SpawnBullet", spawnTime, spawnTime);
    }
    void Update() {
        spawnTime = lc.spawnTime;
        timeSinceLastSpawn += Time.deltaTime;
        //if (spawnChangeReady) {
        //CancelInvoke();
        //InvokeRepeating("SpawnBullet", spawnTime, spawnTime);
        //spawnChangeReady = false;
        //Debug.Log("yoyo");
        //}
        if (timeSinceLastSpawn > spawnTime && !goc.dead) {
            SpawnBullet();
            timeSinceLastSpawn = 0.0f;
        }
    }

    void SpawnBullet() {
        megaOrbs = GameObject.FindGameObjectsWithTag("Mega");
        portalOrbs = GameObject.FindGameObjectsWithTag("Portal");
        prev = chosenPoint;
        while (prev == chosenPoint) {
            chosenPoint = Random.Range(0, 7);
        }

        randomNum = Random.Range(1, 102);
        if (randomNum >= 1 && randomNum <= 33) {
            type = 1;
        } else if (randomNum >= 34 && randomNum <= 94) {
            type = 2;
        } else if (randomNum >= 95 && randomNum <= 102) {
            type = 3;
        }

        if (chosenPoint == 0 || chosenPoint == 1) {
            bsc.direction = new Vector3(0f, -1f, 0f);
            psc.direction = new Vector3(0f, -1f, 0f);
            ssc.direction = new Vector3(0f, -1f, 0f);
            nsc.direction = new Vector3(0f, -1f, 0f);
            dsc.direction = new Vector3(0f, -1f, 0f);
            gsc.direction = new Vector3(0f, -1f, 0f);
        } else if (chosenPoint == 2 || chosenPoint == 3) {
            bsc.direction = new Vector3(0f, 1f, 0f);
            psc.direction = new Vector3(0f, 1f, 0f);
            ssc.direction = new Vector3(0f, 1f, 0f);
            nsc.direction = new Vector3(0f, 1f, 0f);
            dsc.direction = new Vector3(0f, 1f, 0f);
            gsc.direction = new Vector3(0f, 1f, 0f);
        } else if (chosenPoint == 4 || chosenPoint == 5) {
            bsc.direction = new Vector3(1f, 0f, 0f);
            psc.direction = new Vector3(1f, 0f, 0f);
            ssc.direction = new Vector3(1f, 0f, 0f);
            nsc.direction = new Vector3(1f, 0f, 0f);
            dsc.direction = new Vector3(1f, 0f, 0f);
            gsc.direction = new Vector3(1f, 0f, 0f);
        } else if (chosenPoint == 6 || chosenPoint == 7) {
            bsc.direction = new Vector3(-1f, 0f, 0f);
            psc.direction = new Vector3(-1f, 0f, 0f);
            ssc.direction = new Vector3(-1f, 0f, 0f);
            nsc.direction = new Vector3(-1f, 0f, 0f);
            dsc.direction = new Vector3(-1f, 0f, 0f);
            gsc.direction = new Vector3(-1f, 0f, 0f);
        }
        if (type == 1) {
            Instantiate(point, spawnPoints[chosenPoint], Quaternion.identity);
        } else if (type == 2) {
            Instantiate(bullet, spawnPoints[chosenPoint], Quaternion.identity);
            //bulletUnits += 1;
        } else if (type == 3 && (goc.shieldUnlocked || goc.portalUnlocked || goc.darkUnlocked || goc.goldUnlocked)){
            bool specialChosen = false;
            while (!specialChosen) {
                randomNum = Random.Range(1, 100);
                if (randomNum >= 1 && randomNum <= 25) {
                    if (!goc.unshielded) {
                        Instantiate(shield, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    } else {
                        print("Unshielded, spawned Green Orb");
                        Instantiate(point, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    }
                } else if (randomNum >= 26 && randomNum <= 50 && goc.portalUnlocked) {
                    if (goc.portalActivated < 2 && portalOrbs.Length == 1) {
                        Instantiate(portal, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    } else {
                        Instantiate(point, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    }
                } else if (randomNum >= 51 && randomNum <= 75 && goc.darkUnlocked) {
                    if (goc.darkTimer == 0 && goc.goldTimer == 0 && megaOrbs.Length < 3) {
                        Instantiate(dark, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    } else {
                        print("Mega Orb already on scene");
                    }
                } else if (randomNum >= 76 && randomNum <= 100 && goc.goldUnlocked) {
                    if (goc.darkTimer == 0 && goc.goldTimer == 0 && megaOrbs.Length < 3) {
                        Instantiate(gold, spawnPoints[chosenPoint], Quaternion.identity);
                        specialChosen = true;
                    } else {
                        print("Mega Orb already on scene");
                    }
                }
            }
        } else {
            Instantiate(point, spawnPoints[chosenPoint], Quaternion.identity);
        }
    }
}
