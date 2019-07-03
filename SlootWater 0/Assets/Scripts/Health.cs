using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField, Range(0.0f, 100.0f)]
    private float maxHealth = 100.0f;
    private float health;

    // Of we al de OnDeath hebben getriggert
    private bool triggeredDeath = false;

    public event Action OnDeath;

    // Start is called before the first frame update
    void Start() {
        health = maxHealth;
    }

    public float Get() {
        return health;
    }

    public float GetMax() {
        return maxHealth;
    }

    public float Add(float h) {
        health = Mathf.Min(health + h, maxHealth);
        return health;
    }

    // Returns true if the health has depleted to zero
    public float Subtract(float h) {
        health = Mathf.Max(0.0f, health - h);

        if (health <= 0.0f && OnDeath != null && !triggeredDeath) {
            triggeredDeath = true;
            OnDeath.Invoke();
        }

        return health;
    }

    public float AddMax(float h) {
        maxHealth = Mathf.Min(100.0f, maxHealth + h);
        return maxHealth;
    }

    public float SubtractMax(float h) {
        maxHealth = Mathf.Max(0.0f, maxHealth - h);
        health = Mathf.Min(health, maxHealth);

        if (health <= 0.0f && OnDeath != null && !triggeredDeath) {
            triggeredDeath = true;
            OnDeath.Invoke();
        }

        return health;
    }
}
