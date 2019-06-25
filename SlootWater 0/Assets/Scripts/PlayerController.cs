using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour {

    Health health;
    FirstPersonController fps;

    // Respawn is automatically generated where the player first spawns
    GameObject respawn;

    // Start is called before the first frame update
    void Start() {
        if (respawn == null) {
            respawn = new GameObject("Respawn");
            respawn.transform.position = transform.position;
        }

        fps = GetComponent<FirstPersonController>();
        health = GetComponent<Health>();
        health.OnDeath += HandleDeath;
    }

    void HandleDeath() {
        // Return health
        health.Add(health.GetMax());

        StartCoroutine(Respawn());
    }

    IEnumerator Respawn() {
        fps.enabled = false;
        yield return null;
        transform.position = respawn.transform.position;
        yield return null;
        fps.enabled = true;
        yield return null;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Minus)) {
            health.Subtract(0.5f);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Plus)) {
            health.Add(0.5f);
        }
    }
}
