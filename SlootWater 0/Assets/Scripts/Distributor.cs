using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distributor : MonoBehaviour {

    [System.Serializable]
    struct SpawnChance {
        [Tooltip("Hoeveel health/magazines/radiate/etc de speler minimaal moet hebben voor deze case"), Range(0.0f, 1.0f)]
        public float value;

        [Tooltip("Procent kans om een medkit the spawnen"), Range(0.0f, 100.0f)]
        public float chance;
    }

    GameObject[] spawnPoints;
    Health playerHealth;

    [SerializeField, Header("Tijd wanneer het een nieuwe medkit probeert te spawnen")]
    private float medkitSpawnTime = 60.0f;

    [SerializeField]
    GameObject medkitPrefab;

    [SerializeField, Header("-1 voor geen limiet")]
    private int maxMedkits = -1;

    [SerializeField]
    List<SpawnChance> medkitRates;

    [SerializeField]
    Shoot playerPistol;

    [SerializeField]
    Shoot playerRifle;

    [SerializeField, Header("Tijd wanneer het een nieuwe magazine probeert te spawnen")]
    private float magazineSpawnTime = 60.0f;

    [SerializeField]
    GameObject magazinePrefab;

    [SerializeField, Header("-1 voor geen limiet")]
    private int maxMagazines = -1;

    [SerializeField]
    List<SpawnChance> magazineRates;

    [SerializeField, Header("Tijd wanneer het een nieuwe radkit probeert te spawnen")]
    private float radkitSpawnTime = 60.0f;

    [SerializeField]
    GameObject radkitPrefab;

    [SerializeField, Header("-1 voor geen limiet")]
    private int maxRadkits = -1;

    [SerializeField]
    List<SpawnChance> radkitRates;

    // Start is called before the first frame update
    void Start() {
        spawnPoints  = GameObject.FindGameObjectsWithTag("ItemSpawn");
        playerHealth = GameObject.Find("Player").GetComponent<Health>();

        medkitRates.OrderByDescending(x => x.value);
        magazineRates.OrderByDescending(x => x.value);
        radkitRates.OrderByDescending(x => x.value);

        InvokeRepeating("TrySpawnMedkit", medkitSpawnTime, medkitSpawnTime);
        InvokeRepeating("TrySpawnMagazine", magazineSpawnTime, magazineSpawnTime);
        InvokeRepeating("TrySpawnRadkit", radkitSpawnTime, radkitSpawnTime);
    }

    void TrySpawnMedkit() {
        TrySpawnItem(medkitPrefab, "Medkit", playerHealth.GetMax(), playerHealth.Get(), medkitRates, maxMedkits);
    }

    void TrySpawnMagazine() {
        int totalMags = 0;
        totalMags += playerPistol.GetCurrentMags();
        totalMags += playerRifle.GetCurrentMags();

        // Aantal magazines dat de speler in totaal mag hebben (soft limiet)
        const int MAX_MAGAZINES_TOTAL = 7;

        TrySpawnItem(magazinePrefab, "Magazine", MAX_MAGAZINES_TOTAL, totalMags, magazineRates, maxMagazines);
    }

    void TrySpawnRadkit() {
        TrySpawnItem(radkitPrefab, "Radkit", 100.0f, playerHealth.GetMax(), radkitRates, maxRadkits);
    }

    void TrySpawnItem(GameObject prefab, string tagName, float maxValue, float value,
                        List<SpawnChance> chanceList, int maxItems) {
        // Check hoeveel medkits we hebben
        GameObject[] items = GameObject.FindGameObjectsWithTag(tagName);

        if (items.Length >= maxItems && maxItems != -1) return;

        // Probeer een nieuwe medkit te spawnen
        foreach (SpawnChance sc in chanceList) {
            if (value <= sc.value * maxValue) {
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

                    GameObject obj = Instantiate(prefab);
                    obj.transform.SetParent(spawnPoint.transform);
                    obj.transform.localPosition = Vector3.zero;
                    break;
                }
            }
        }
    }
}
