using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour {

    [SerializeField]
    private float health = 15.0f;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            other.GetComponent<Health>().Add(health);
            Destroy(gameObject);
        }
    }
}
