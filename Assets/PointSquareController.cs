using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSquareController : MonoBehaviour
{
    public GameOverController goc;
    public float moveSpeed;
    private Rigidbody2D rb;
    public Vector3 direction;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public LevelController lc;
    public BulletSpawner bs;
    public Transform pointsPopUp;

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
            if (!goc.dead) {
                audioSource.PlayOneShot(audioClip);
                goc.interactionPoints = goc.normalPoints * goc.goldMultiplier;
                if (goc.points < 90) {
                    lc.moveSpeed += 0.0125f * goc.interactionPoints;
                    lc.spawnTime -= 0.003125f * goc.interactionPoints;
                }
                goc.points += goc.interactionPoints;
                Transform newPopUpTransform = Instantiate(pointsPopUp, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
                TextPopUp transformTextPopUp = newPopUpTransform.GetComponent<TextPopUp>();
                transformTextPopUp.Setup("+" + goc.interactionPoints.ToString(), 132, 255, 88, 255);
                transformTextPopUp.ready = true;
            }
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Nucleus") {
            Destroy(gameObject);
        }
    }
}
