using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radkit : MonoBehaviour {

    [SerializeField]
    private float health = 15.0f;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            other.GetComponent<Health>().AddMax(health);
            Destroy(gameObject);
        }
    }
}
