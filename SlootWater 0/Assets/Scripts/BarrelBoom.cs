using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelBoom : MonoBehaviour
{
    public GameObject explotion;
    public float expolotionDamage;

    private Health barrelHealth;
    private Health playerHealth;
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();

        barrelHealth  = GetComponent<Health>();
        barrelHealth.OnDeath += BarrelHealth_OnDeath;
    }

    private void BarrelHealth_OnDeath()
    {
        StartCoroutine(ExploderOfBarrels());
    }

    IEnumerator ExploderOfBarrels()
    {
        Instantiate(explotion);
        yield return null;
    }


    private void Update()
    {
        
    }

}
