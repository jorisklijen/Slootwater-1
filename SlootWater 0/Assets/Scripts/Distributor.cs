using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour {

    GameObject[] spawnPoints;
    Health playerHealth;

    [System.Serializable]
    struct SpawnChance {
        [Tooltip("Hoeveel health de speler minimaal moet hebben voor deze case"), Range(0.0f, 100.0f)]
        public float health;

        [Tooltip("Procent kans om een medkit the spawnen"), Range(0.0f, 100.0f)]
        public float chance;
    }

    [SerializeField, Header("Tijd wanneer het een nieuwe medkit probeert te spawnen")]
    private float medkitSpawnTime = 60.0f;

    [SerializeField]
    GameObject medkitPrefab;

    [SerializeField, Header("-1 voor geen limiet")]
    private int maxMedkits = -1;

    [SerializeField]
    List<SpawnChance> medkitRates;

    // Start is called before the first frame update
    void Start() {
        spawnPoints  = GameObject.FindGameObjectsWithTag("ItemSpawn");
        playerHealth = GameObject.Find("Player").GetComponent<Health>();

        medkitRates.OrderByDescending(x => x.health);

        InvokeRepeating("TrySpawnMedkit", medkitSpawnTime, medkitSpawnTime);
    }

    void TrySpawnMedkit() {
        // Check hoeveel medkits we hebben
        GameObject[] medkits = GameObject.FindGameObjectsWithTag("Medkit");

        if (medkits.Length >= maxMedkits && maxMedkits != -1) return;

        // Probeer een nieuwe medkit te spawnen
        foreach (SpawnChance sc in medkitRates) {
            if (playerHealth.Get() <= sc.health) {
                bool spawn = Random.Range(0.0f, 100.0f) <= sc.chance;

                if (spawn) {
                    GameObject spawnPoint = null;
                    bool spawnValid = false;
                    for (int i = 0; i < spawnPoints.Length; ++i) {
                        spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

                        if (spawnPoint.transform.childCount == 0) {
                            spawnValid = true;
                            break;
                        }
                    }

                    // Alle spawns die we hebben gecheckt zijn vol.
                    // Dit kan betekenen dat alle spawn spots vol zijn
                    // of dat alle spawns die wij willekeurig geselecteerd
                    // hebben vol zitten. In de tweede scenario kunnen wij
                    // in de volgende check nog spawns vinden.
                    if (!spawnValid) return;

                    GameObject obj = Instantiate(medkitPrefab);
                    obj.transform.SetParent(spawnPoint.transform);
                    obj.transform.localPosition = Vector3.zero;
                    Debug.Log(obj.transform.position);
                    break;
                }
            }
        }
    }
}
