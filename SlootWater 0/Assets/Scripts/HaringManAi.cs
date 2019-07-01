using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HaringManAi : MonoBehaviour
{
    public float fovAngele = 110.0f;
    public bool playerInSight;
    public Vector3 personallastSighting;
    public float minAttackRange = 2.0f;
    public float damageToPlayer = 3.0f;
    public float attackSpeedInSeconds = 1.0f;


    private NavMeshAgent nav;


    private GameObject player;
    private Health playerHealth;
    private float attackTimer = 0.0f;
    private Health enemyHealth;


    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += EnemyHealth_OnDeath;
    }

    private void EnemyHealth_OnDeath()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        nav.SetDestination(player.transform.position);


        attackTimer += Time.deltaTime;
        if(Vector3.Distance(transform.position , player.transform.position) <= minAttackRange && attackTimer >= attackSpeedInSeconds )
        {
            playerHealth.Subtract(damageToPlayer);
            attackTimer = 0.0f;
        }

    }


}
