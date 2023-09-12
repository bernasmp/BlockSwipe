using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquarePosition : MonoBehaviour{
    public int squaresAbsorbed;
    public int pointsAbsorbed;
    int maxSquares = 2;
    public Transform pointsPopUp;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public AudioClip audioClip2;
    public GameOverController goc;
    public Vector3 thisCoord;
    public int arrayPos;
    public LevelController lc;

    void Start(){

    }
    void Update(){
        if (squaresAbsorbed == maxSquares) {
            goc.interactionPoints = pointsAbsorbed * goc.goldMultiplier;
            Transform newPopUpTransform = Instantiate(pointsPopUp, new Vector3(transform.position.x, transform.position.y, -1), Quaternion.identity);
            TextPopUp transformTextPopUp = newPopUpTransform.GetComponent<TextPopUp>();
            transformTextPopUp.Setup("+" + goc.interactionPoints.ToString(), 252, 165, 255, 255);
            transformTextPopUp.ready = true;
            if (goc.points < 90) {
                lc.moveSpeed += 0.0125f * goc.interactionPoints;
                lc.spawnTime -= 0.003125f * goc.interactionPoints;
            }
            goc.points += goc.interactionPoints;
            FindThatFuckingCoord();
            goc.portalActivated -= 1;
            Destroy(transform.parent.gameObject);
        }
    }
    IEnumerator AbsorveAnim() {
        transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.38f, 0.38f, 0.38f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        yield return new WaitForSeconds(0.016f);
        transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Point" || col.gameObject.tag == "Mega") {
            //StartCoroutine("AbsorveAnim");
            pointsAbsorbed += goc.portalPoints;
            squaresAbsorbed += 1;
            if (squaresAbsorbed != maxSquares) {
                audioSource.PlayOneShot(audioClip);
                StartCoroutine("AbsorveAnim");
                //transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f, 0f);
            } else {
                audioSource.PlayOneShot(audioClip2);
            }
        } else if (col.gameObject.tag == "Bullet") {
            //StartCoroutine("AbsorveAnim");
            pointsAbsorbed += goc.portalPoints;
            squaresAbsorbed += 1;
            if (squaresAbsorbed != maxSquares) {
                audioSource.PlayOneShot(audioClip);
                StartCoroutine("AbsorveAnim");
                //transform.localScale = transform.localScale + new Vector3(0.1f, 0.1f, 0f);
            } else {
                audioSource.PlayOneShot(audioClip2);
            }
        } 
    }

    void FindThatFuckingCoord() {
        for (int i=0; i<goc.portalSpots.Length; i++) {
            if (thisCoord == goc.portalSpots[i]) {
                arrayPos = i;
            }
        }
        if (goc.portalSpawn1 == arrayPos) {
            goc.portalSpawn1 = 10;
        } else if (goc.portalSpawn2 == arrayPos) {
            goc.portalSpawn2 = 10;
        }
    }
}
