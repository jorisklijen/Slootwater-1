using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{ 
    public Transform healthBar;
    public Transform maxHelthBar;

    private void Start()
    {
        float healthValue = gameObject.GetComponent<Health>().health;
        float maaxHealthValue = gameObject.GetComponent<Health>().maxHealth;

        
    }


    // Update is called once per frame
    void Update()
    {
        Health health = gameObject.GetComponent<Health>();
        Debug.Log(health == null);
        healthBar.GetComponent<Image>().fillAmount = health.Get();
        maxHelthBar.GetComponent<Image>().fillAmount = health.GetMax();
    }
}
