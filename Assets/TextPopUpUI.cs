using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPopUpUI : MonoBehaviour
{
    public bool ready;
    Color yoyoColor;
    public int value;
    public TextMeshProUGUI popUpText;
    public float fadingDuration;
    public float fadeTimer;
    public float fadingDistance;
    float destroyTime = 3f;

    // Start is called before the first frame update
    void Start() {
        popUpText = transform.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        if (ready) {
            float moveYSpeed = 1f;
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
            fadeTimer -= Time.deltaTime;
            if (fadeTimer < 0) {
                float fadeSpeed = 2f;
                yoyoColor.a -= fadeSpeed * Time.deltaTime;
                popUpText.color = yoyoColor;
            }
        }
        if (fadeTimer < -destroyTime) {
            Destroy(gameObject);
        }
    }

    public void Setup(string s, byte r, byte g, byte b, byte a) {
        popUpText.SetText(s);
        popUpText.color = new Color32(r, g, b, a);
        yoyoColor = popUpText.color;
    }
}
