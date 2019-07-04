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
    private bool canAttack = true;

    [SerializeField]
    private Vector2 stepSoundInSeconds;
    private float stepSoundTarget = 0.0f;
    private float stepSoundTimer = 0.0f;

    [SerializeField]
    private AudioClip[] stepSounds;

    [SerializeField]
    private AudioSource stepSource;

    [SerializeField]
    private AudioClip[] attackSounds;

    [SerializeField]
    private AudioSource attackSource;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        playerHealth = player.GetComponent<Health>();
        enemyHealth = GetComponent<Health>();
        enemyHealth.OnDeath += EnemyHealth_OnDeath;
        anim = GetComponent<Animator>();

        stepSoundTarget = Random.Range(stepSoundInSeconds.x, stepSoundInSeconds.y);
    }

    private void EnemyHealth_OnDeath()
    {
        Debug.Log("IK BEN NU SUSHI");
        canAttack = false;
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
        PlayAttackSound();
        anim.SetBool("Attack", true);
        playerHealth.Subtract(damageToPlayer);
        attackTimer = 0.0f;
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("Attack", false);
        yield return null;
    }


    void PlayStepSound() {
        int r = Random.Range(0, stepSounds.Length);
        stepSource.clip = stepSounds[r];
        stepSource.PlayOneShot(stepSource.clip);
    }


    void PlayAttackSound() {
        int r = Random.Range(0, attackSounds.Length);
        attackSource.clip = attackSounds[r];
        attackSource.PlayOneShot(attackSource.clip);
    }

    private void Update()
    {
        if (isAlive == true)
        {
            if (nav.enabled)
            {
                nav.SetDestination(player.transform.position);

                stepSoundTimer += Time.deltaTime;
                if (stepSoundTimer >= stepSoundTarget) {
                    stepSoundTarget = Random.Range(stepSoundInSeconds.x, stepSoundInSeconds.y);
                    stepSoundTimer = 0.0f;

                    PlayStepSound();
                }
            }

            attackTimer += Time.deltaTime;
            if (Vector3.Distance(transform.position, player.transform.position) <= minAttackRange && attackTimer >= attackSpeedInSeconds)
            {
                if (canAttack == true)
                {
                    StartCoroutine(AttackPlayer());
                }
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
