using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeDoor : MonoBehaviour {

    [SerializeField]
    GameObject fakeText;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player" && other.GetComponent<PlayerController>().HasKey()) {
            fakeText.SetActive(true);

            // Verwijder dit script
            Destroy(this);
        }
    }
}
