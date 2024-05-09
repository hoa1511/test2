using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : HealthBar
{
    public static PlayerHealthBar Instance;
    protected override void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public override void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth/maxHealth;
        if(currentHealth <= 50 && currentHealth >= 30)
        {
            healthBar.GetComponent<Image>().color = Color.yellow;

        }

        if(currentHealth < 30 && currentHealth >= 0)
        {
            healthBar.GetComponent<Image>().color = Color.red; 
        }
    }
}
