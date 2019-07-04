using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinDoor : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player" && other.GetComponent<PlayerController>().HasKey()) {
            SceneManager.LoadScene("Win");
        }
    }
}
