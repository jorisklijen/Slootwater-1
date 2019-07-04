using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockKamikaze : MonoBehaviour {
    void Awake() {
        // VRIJHEID!!!!
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible   = true;
        Destroy(gameObject);
    }
}
