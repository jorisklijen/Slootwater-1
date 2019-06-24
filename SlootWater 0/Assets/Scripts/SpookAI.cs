using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpookAI : MonoBehaviour {
    private GameObject player;
    private Animator anim;
    private SkinnedMeshRenderer[] mrs;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float idleDistance = 1.0f;

    [SerializeField]
    private float bopDivider = 200.0f;

    [SerializeField]
    private float revealDistance = 0.5f;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        anim   = GetComponent<Animator>();
        mrs    = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        // Turn towards player on only the Y rotation
        transform.LookAt(player.transform);
        float rot = transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0.0f, rot, 0.0f);

        // Move towards player
        bool inRange = Vector3.Distance(transform.position, player.transform.position) <= idleDistance;

        // float modifier = (Mathf.Sin(Time.time) + 1.0f) / 2.0f;

        if (!inRange) {
            Vector3 dir = player.transform.position - transform.position;
            transform.position += dir * speed;
        }

        // anim.SetFloat("walkSpeed", (inRange) ? 1.0f : 0.0f);
        // Bop Y up and down
        float bop = Mathf.Sin(Time.time) / bopDivider;
        transform.position = new Vector3(transform.position.x, transform.position.y + bop, transform.position.z);

        bool shouldHide = Vector3.Distance(transform.position, player.transform.position) > revealDistance;
        if (shouldHide) {
            foreach (SkinnedMeshRenderer mr in mrs) {
                mr.enabled = !shouldHide;
            }
        }
    }
}
