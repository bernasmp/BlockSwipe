using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public GameObject[] squareSpots;
    private int currentSpot = 0;

    //private Vector2 touchStartPos;
    //private Vector2 touchEndPos;

    

    // Start is called before the first frame update
    void Start() {
        transform.position = squareSpots[currentSpot].transform.position;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            if (currentSpot == 0) {
                currentSpot = 1;
            } else if (currentSpot == 2) {
                currentSpot = 3;
            }
        } else if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            if (currentSpot == 1) {
                currentSpot = 0;
            } else if (currentSpot == 3) {
                currentSpot = 2;
            }
        } else if (Input.GetKeyUp(KeyCode.DownArrow)) {
            if (currentSpot == 1) {
                currentSpot = 3;
            } else if (currentSpot == 0) {
                currentSpot = 2;
            }
        } else if (Input.GetKeyUp(KeyCode.UpArrow)) {
            if (currentSpot == 2) {
                currentSpot = 0;
            } else if (currentSpot == 3) {
                currentSpot = 1;
            }
        }
        
        transform.position = squareSpots[currentSpot].transform.position;
    }
}
