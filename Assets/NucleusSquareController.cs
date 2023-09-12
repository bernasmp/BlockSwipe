using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NucleusSquareController : MonoBehaviour
{
    public GameOverController goc;
    public float moveSpeed;
    private Rigidbody2D rb;
    public Vector3 direction;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public LevelController lc;
    public Transform pointsPopUp;
    public Vector3[] portalSpots;
    public GameObject portal;
    public Vector3 spawnCoord;
    public bool spotChosen = false;
    public int spawnSpot;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        moveSpeed = lc.moveSpeed;

        if (!goc.dead) {
            rb.velocity = direction * moveSpeed;
        } else if (goc.dead) {
            rb.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "SquareSpot") {
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Player") {
            while (!spotChosen) {
                spawnSpot = Random.Range(0, 7);
                if (spawnSpot != goc.portalSpawn1 && spawnSpot != goc.portalSpawn2) {
                    spawnCoord = portalSpots[spawnSpot];
                    spotChosen = true;
                }
            }
            if (goc.portalSpawn1 == 10) {
                goc.portalSpawn1 = spawnSpot;
            } else if (goc.portalSpawn2 == 10) {
                goc.portalSpawn2 = spawnSpot;
            }
            Instantiate(portal, spawnCoord, Quaternion.identity);
            SquarePosition portalController = portal.transform.GetChild(3).GetComponent<SquarePosition>();
            portalController.thisCoord = spawnCoord;
            Transform newPopUpTransform = Instantiate(pointsPopUp, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            TextPopUp transformTextPopUp = newPopUpTransform.GetComponent<TextPopUp>();
            transformTextPopUp.Setup("+PORTAL", 252, 165, 255, 255);
            transformTextPopUp.ready = true;
            goc.portalActivated += 1;
            audioSource.PlayOneShot(audioClip);
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Nucleus") {
            print("destroyed purple");
            Destroy(gameObject);
        }
    }
}
