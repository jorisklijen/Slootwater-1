using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBoom : MonoBehaviour {
    public GameObject explotion;
    public float explotionDamage;
    public float explotionRange;

    private Health barrelHealth;
    private Health playerHealth;
    private GameObject player;
    private Health enemyHealth;

    private void Start() {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();

        barrelHealth = GetComponent<Health>();
        barrelHealth.OnDeath += BarrelHealth_OnDeath;
    }

    private void BarrelHealth_OnDeath() {
        Debug.Log("kaas?? op dood");
        StartCoroutine(ExploderOfBarrels());

        // GameObject[] enemys = GameObject.FindGameObjectsWithTag("enemy");


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
        // if (Vector3.Distance(transform.position, player.transform.position) <= explotionRange)
        // {
        //     playerHealth.Subtract(explotionDamage);
        //     return;
        // }
        //  if (enemys  )    //tom help
        //                   //ik kom hier hiet uit het is hier de bedoeling dat de haring mannen ook schade neemen als het vat ondploft net als andere vaten als die te dicht bij staan. 
        //  {
        //
        //
        //      return;
        //  }
    }

    IEnumerator ExploderOfBarrels() {
        Instantiate(explotion, transform.position, new Quaternion());
        yield return null;
    }


}
