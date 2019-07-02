using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radiation : MonoBehaviour {

    [SerializeField, Min(0.0f)]
    private float range;

    [SerializeField]
    private float damage;

    Health playerHealth;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        GameObject player = GameObject.Find("Player");
        playerHealth      = player.GetComponent<Health>();
        playerController  = player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(playerHealth.transform.position, transform.position) <= range) {
            // RADIATION AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
            playerHealth.SubtractMax(damage);
            playerController.TriggerRadiation();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0.5f, 1.0f, 0.5f, 1.0f);
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
