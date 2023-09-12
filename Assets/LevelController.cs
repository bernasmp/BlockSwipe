using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelController : MonoBehaviour
{
    public BulletSquareController bsc;
    public PointSquareController psc;
    public ShieldSquareController ssc;
    public BulletSpawner bs;
    public float moveSpeed;
    public float spawnTime;
    public GameOverController goc;
    public TextMeshProUGUI levelText;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        //level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (goc.points >= 90) {
            moveSpeed = 2.625f;
            spawnTime = 0.71875f;
        }
    }
}
