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
    public float attackSpeedInSeconds = 1.5f;


    private NavMeshAgent nav;
    private SphereCollider col;
    private Vector3 previousSighting;

    private GameObject player;
    private Health playerHealth;
    private float attackTimer = 0.0f;
    private Health enemyHealth;
    private Animator anim;
    private bool isAlive = true;
    private float sinkTimer = 0.0f;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += EnemyHealth_OnDeath;
        anim = GetComponent<Animator>();


    }

    private void EnemyHealth_OnDeath()
    {
        anim.SetTrigger("HaringDood");
        nav.enabled = false;

        StartCoroutine(SetDead());
    }

    IEnumerator SetDead()
    {
        yield return new WaitForSeconds(3.0f);
        isAlive = false;
        yield return null;
    }

    IEnumerator AttackPlayer()
    {
        anim.SetBool("Attack", true);
        playerHealth.Subtract(damageToPlayer);
        attackTimer = 0.0f;
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Attack", false);
        yield return null;
    }


    private void Update()
    {
        if (isAlive == true)
        {
            if (nav.enabled)
            {
                nav.SetDestination(player.transform.position);
            }

            attackTimer += Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) <= minAttackRange && attackTimer >= attackSpeedInSeconds)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            sinkTimer += Time.deltaTime;

            const float SINK_SECONDS = 5.0f;
            if (sinkTimer >= SINK_SECONDS)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position -= new Vector3(0.0f, 0.003f, 0.0f);
            }
        }
    }


}
