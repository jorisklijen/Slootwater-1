using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    [SerializeField]
    private float maxHealth;
    private float health;

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

        if (health <= 0.0f && OnDeath != null) {
            OnDeath.Invoke();
        }

        return health;
    }
}
