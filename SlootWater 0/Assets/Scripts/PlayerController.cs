using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour {

    Health health;
    FirstPersonController fps;
    
    [SerializeField]
    AudioSource geigerSound;
    bool inRadiation = false;

    bool hasKey = false;

    public void TriggerRadiation() {
        inRadiation = true;
    }

    public void TriggerKey() {
        hasKey = true;
    }

    public bool HasKey() {
        return hasKey;
    }

    // Start is called before the first frame update
    void Start() {
        fps = GetComponent<FirstPersonController>();
        health = GetComponent<Health>();
        health.OnDeath += HandleDeath;
    }

    void HandleDeath() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void LateUpdate() {
        if (inRadiation) {
            if (!geigerSound.isPlaying) {
                geigerSound.Play();
                Debug.Log("PLAY!");
            }
        } else if (geigerSound.isPlaying) {
            Debug.Log("PAUSE!");
            geigerSound.Pause();
        }

        inRadiation = false;
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.Minus)) {
            health.Subtract(0.25f);
        }

        if (Input.GetKey(KeyCode.Plus) && Input.GetKey(KeyCode.LeftShift)) {
            health.Add(0.25f);
        }
    }
}
