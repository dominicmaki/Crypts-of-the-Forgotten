using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Transform healthBar;

    public float maxHealth = 1f;
    [SerializeField] public float currentHealth;


    void Start()
    {
        currentHealth = 0.5f;
        SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHealth(float health)
    {
        healthBar.localScale = new Vector3(health, healthBar.localScale.y, 1);
    }

}
