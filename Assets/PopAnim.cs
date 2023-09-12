using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PopAnim : MonoBehaviour
{
    bool newCycle = true;
    Vector3 normalScale;
    Vector3 popScale;
    private void Start() {
        normalScale = transform.localScale;
        popScale = normalScale + new Vector3(0.1f, 0.1f, 0.1f);
    }
    void Update()
    {
        if (newCycle) {
            StartCoroutine("Pop");
            newCycle = false;
        }
    }

    IEnumerator Pop() {
        transform.DOScale(popScale, 0.5f);
        yield return new WaitForSeconds(0.5f);
        transform.DOScale(normalScale, 0.5f);
        yield return new WaitForSeconds(1f);
        newCycle = true;
    }
}
