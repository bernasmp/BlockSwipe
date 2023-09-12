using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SwipeController : MonoBehaviour
{
    public GameOverController goc;
    public GameObject[] squareSpots;
    private int currentSpot = 0;
    private float moveSpeed = 7f;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;

    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClip2;
    public AudioClip audioClip3;
    private bool changedPos = false;
    bool darkFlickToggle = false;
    bool goldFlickToggle = false;
    bool darkNow = false;
    bool goldNow = false;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start() {
        transform.position = squareSpots[currentSpot].transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, squareSpots[currentSpot].transform.position, moveSpeed * Time.deltaTime);
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && goc.dead == false) {
            touchStartPos = Input.GetTouch(0).position;
            //touchEndPos = touchStartPos + new Vector2(0, 50);
        } else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && goc.dead == false) {
            touchEndPos = Input.GetTouch(0).position;
            Vector2 direction = touchEndPos - touchStartPos;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) {
                // horizontal swipe
                if (direction.x > 0) {
                    if (currentSpot == 0) {
                        currentSpot = 1;
                        changedPos = true;
                    } else if (currentSpot == 2) {
                        currentSpot = 3;
                        changedPos = true;
                    }
                } else {
                    if (currentSpot == 1) {
                        currentSpot = 0;
                        changedPos = true;
                    } else if (currentSpot == 3) {
                        currentSpot = 2;
                        changedPos = true;
                    }
                }
            } else {
                // vertical swipe
                if (direction.y < 0) {
                    if (currentSpot == 1) {
                        currentSpot = 3;
                        changedPos = true;
                    } else if(currentSpot == 0) {
                        currentSpot = 2;
                        changedPos = true;
                    }
                } else {
                    if (currentSpot == 2) {
                        currentSpot = 0;
                        changedPos = true;
                    } else if (currentSpot == 3) {
                        currentSpot = 1;
                        changedPos = true;
                    }
                }
            }
            //transform.position = squareSpots[currentSpot].transform.position;
            if (changedPos) {
                audioSource.PlayOneShot(audioClip);
                changedPos = false;
            }
        }
        if (goc.darkTimer > 2.24f && !darkNow) {
            Color32 colorBlack = new Color32(29, 29, 29, 255);
            spriteRenderer.DOColor(colorBlack, 0.05f);
            print("pls work ffs");
            darkNow = true;
        } if (goc.darkTimer == 0f && darkNow){
            spriteRenderer.color = new Color32(86, 167, 214, 255);
            print("this line fucked me");
            darkNow = false;
        } if (goc.darkTimer <= 2.24f && !darkFlickToggle && darkNow) {
            darkFlickToggle = true;
            StartCoroutine("DarkEndFlicker");
        }

        if (goc.goldTimer > 2.24f && !goldNow) {
            Color32 colorGold = new Color32(221, 209, 68, 255);
            spriteRenderer.DOColor(colorGold, 0.05f);
            print("pls work ffs");
            goldNow = true;
        } if (goc.goldTimer == 0f && goldNow) {
            spriteRenderer.color = new Color32(86, 167, 214, 255);
            print("this line fucked me");
            goldNow = false;
        } if (goc.goldTimer <= 2.24f && !goldFlickToggle && goldNow) {
            goldFlickToggle = true;
            StartCoroutine("GoldEndFlicker");
        }
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Point") {
            StartCoroutine("AbsorveAnim");
        } else if (col.gameObject.tag == "Bullet") {
            if (goc.darkTimer == 0) {
                StartCoroutine("AbsorveAnim");
                audioSource.PlayOneShot(audioClip2);
            } else {
                StartCoroutine("AbsorveAnim");
                //audioSource.PlayOneShot(audioClip3);
            }
        }
    }

    IEnumerator AbsorveAnim() {
        transform.localScale = new Vector3(0.73f, 0.73f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.76f, 0.76f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.75f, 0.75f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.725f, 0.725f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.715f, 0.715f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.71f, 0.71f, 1f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.7f, 0.7f, 1f);
    }

    IEnumerator DarkEndFlicker() {
        Color32 colorBlack = new Color32(29, 29, 29, 255);
        Color32 colorBlue = new Color32(86, 167, 214, 255);
        spriteRenderer.DOColor(colorBlack, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlack, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlack, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlack, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        darkFlickToggle = false;
    }

    IEnumerator GoldEndFlicker() {
        Color32 colorGold = new Color32(221, 209, 68, 255);
        Color32 colorBlue = new Color32(86, 167, 214, 255);
        spriteRenderer.DOColor(colorGold, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorGold, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorGold, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorGold, 0.28f);
        yield return new WaitForSeconds(0.28f);
        spriteRenderer.DOColor(colorBlue, 0.28f);
        yield return new WaitForSeconds(0.28f);
        goldFlickToggle = false;
    }
}