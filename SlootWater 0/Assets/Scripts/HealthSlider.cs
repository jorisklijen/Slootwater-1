using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{ 
    public Transform healthBar;
    public Transform maxHelthBar;

    private Health health;


    private void Start()
    {
        health = GameObject.Find("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(health == null);
        const float MAX_HEALTH = 100.0f;
        healthBar.GetComponent<Image>().fillAmount = health.Get() / MAX_HEALTH ;
        maxHelthBar.GetComponent<Image>().fillAmount = health.GetMax() / MAX_HEALTH ;

        health.Subtract(0.1f);
        
    }
}
