using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextFadeoutDestroy : MonoBehaviour {
    [SerializeField]
    float fadeOutInSeconds = 5.0f;

    // Start is called before the first frame update
    void Start() {
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        foreach (TMP_Text t in texts) {
            t.CrossFadeAlpha(0.0f, fadeOutInSeconds, false);
        }
        Destroy(gameObject, fadeOutInSeconds);
    }
}
