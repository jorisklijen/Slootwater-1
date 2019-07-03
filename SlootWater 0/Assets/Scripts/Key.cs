using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    [SerializeField]
    GameObject victoryText;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            victoryText.SetActive(true);
            other.GetComponent<PlayerController>().TriggerKey();
            Destroy(gameObject);
        }
    }
}
