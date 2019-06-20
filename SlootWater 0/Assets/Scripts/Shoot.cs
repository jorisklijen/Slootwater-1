using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    [Header("Stats")]
    public float damage = 10f;
    public float range = 100f;
    public float fireRateInSeconds = 15f;
    private float fireTimer = 0.0f;

    [Header("Main camera")]
    public Camera fpsCam;

    [Header("Shoot effect")]
    public GameObject muzzleSpawn;
    public GameObject muzzlePrefab;

    [Header("Hit Ffx")]
    public GameObject hitBeton;
    public GameObject hitStaal;
    public GameObject hitVlees;

    // Update is called once per frame
    void Update() {
        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRateInSeconds) {
            fireTimer = 0.0f;
            Shooting(); // calls the schooting function. 
        }
    }

    void Shooting() {
        // Bullet hole
        GameObject obj = Instantiate(muzzlePrefab, muzzleSpawn.transform);
        obj.transform.position = muzzleSpawn.transform.position;

        RaycastHit hit;

        int levelBeton = 1 << LayerMask.NameToLayer("LevelBeton");
        int levelStaal = 1 << LayerMask.NameToLayer("LevelStaal");
        int levelVlees = 1 << LayerMask.NameToLayer("LevelVlees");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelBeton)) {
            Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitBeton, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op beton.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelStaal)) {
            Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitStaal, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op staal.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelVlees)){
            Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitVlees, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op vhijand.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }
    }
}

