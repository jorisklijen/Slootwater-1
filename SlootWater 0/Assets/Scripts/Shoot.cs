using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Ammo")]
    public int maxAmmo = 10;
    public int maggazijnOpslag = 3;
    private int currentAmmo;
    private int currentMags;
    public float reloadTime = 1f;
    private bool isReloding = false;
    public TextMeshProUGUI ammoReserveDispaly;
    public TextMeshProUGUI ammoInuseDispaly;

    [Header("Adio")]
    public AudioSource shootSound;
    public AudioSource ReloadSound;
    

    [Header("Animaties")]
    public Animator animator;

    private void Start() {
        //zet de publieke variable om naar prive
        currentAmmo = maxAmmo;
        currentMags = maggazijnOpslag;
    }

    void OnEnable() {
        //verkomt dat als je van wapen wisselt terwijl je herlaad een bugg krijgt waar in je wapen niet meer wilt schieten
        isReloding = false;
        animator.SetBool("Reloding", false);
    }

    // Update is called once per frame
    void Update() {
        // geeft de ammo weer op het scherm 
        ammoReserveDispaly.text = currentMags.ToString();
        ammoInuseDispaly.text = currentAmmo.ToString();

        // verkomt dat je 2x tegelijk reload
        if (isReloding) {
            return;
        }

        //kijkt of je genoeg magazijenen hebt om te herlaaden. 
        //herlaat als je op R klikt of als je magazijn leeg is.
        if ((currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R)) && currentMags > 0) {
            StartCoroutine(Reload());
            return;
        }


        fireTimer += Time.deltaTime;
        if (Input.GetButton("Fire1") && fireTimer > fireRateInSeconds && currentAmmo > 0) {
            fireTimer = 0.0f;
            Shooting(); // calls the schooting function. 
            shootSound.Play(); // starts teh soot sound.
        }
    }


    IEnumerator Reload()
    {
        ReloadSound.Play();

        isReloding = true;
        animator.SetBool("Reloding", true);
        yield return new WaitForSeconds(reloadTime - .25f); // de .25f is voor de animaatie die anders niet is afgeloopen als je herladen hebt.
        animator.SetBool("Reloding", false);
        yield return new WaitForSeconds(.25f);


        //hier t magazijn systeem
        currentMags--;    // haalt een magazijn weg 
        currentAmmo = maxAmmo; // zet het magazijn in gebruik weer treug naar zijn maximaale hoeveelhijd
        isReloding = false;
    }

    void Shooting() {
        //haalt ammo uit ja magazijn
        currentAmmo--;

        //sound




        // Bullet hole
        GameObject obj = Instantiate(muzzlePrefab, muzzleSpawn.transform);
        obj.transform.position = muzzleSpawn.transform.position;

        RaycastHit hit;

        //de masks waar op de kogels op bebaald worden
        int levelBeton = 1 << LayerMask.NameToLayer("LevelBeton");
        int levelStaal = 1 << LayerMask.NameToLayer("LevelStaal");
        int levelVlees = 1 << LayerMask.NameToLayer("LevelVlees");

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelBeton)) {
            //Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitBeton, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op beton.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelStaal)) {
            //Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitStaal, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op staal.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, levelVlees)) {
            // Debug.Log("Hit transform: " + hit.transform.name);
            Vector3 decalPos = hit.point - transform.forward * 0.01f;
            GameObject impactObj = Instantiate(hitVlees, decalPos, Quaternion.LookRotation(hit.normal)); // spawns a bullet inpact op vhijand.
            impactObj.transform.Rotate(Vector3.forward, Random.Range(0.0f, 360.0f));

            Destroy(impactObj, 10);
        }
    }
}

