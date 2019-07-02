using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    [SerializeField, Range(0.0f, 100.0f)]
    private float rifleChance;

    [SerializeField]
    private Shoot pistol;

    [SerializeField]
    private Shoot rifle;

    private bool isRifleAmmo;

    // Start is called before the first frame update
    void Start() {
        isRifleAmmo = Random.Range(1.0f, 100.0f) <= rifleChance;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            Debug.LogFormat("Adding {0} ammo", (isRifleAmmo) ? "Rifle" : "Pistol");
        
            if (isRifleAmmo) {
                rifle.AddMagazines();
            } else {
                pistol.AddMagazines();
            }

            Destroy(gameObject);
        }
    }

}
