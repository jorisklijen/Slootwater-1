using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBoom : MonoBehaviour {
    public GameObject explotion;
    public float explotionDamage;
    public float explotionRange;

    private Health barrelHealth;

    private void Start() {
        barrelHealth = GetComponent<Health>();
        barrelHealth.OnDeath += BarrelHealth_OnDeath;
    }

    private void BarrelHealth_OnDeath() {
        StartCoroutine(ExploderOfBarrels());

        Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRange);
        foreach (Collider c in colliders) {
            // Doe jezelf geen damage anders zit je vast in een oneindige
            // loop van barrels die zichzelf vermoordern en steeds OnDeath aanroepen
            if (c.gameObject == gameObject) continue;

            Health health = c.GetComponent<Health>();

            // Als dit ding een health script heeft
            if (health) {
                health.Subtract(explotionDamage);
            }
        }

        Destroy(gameObject);
    }

    IEnumerator ExploderOfBarrels() {
        Instantiate(explotion, transform.position, new Quaternion());
        yield return null;
    }


}
