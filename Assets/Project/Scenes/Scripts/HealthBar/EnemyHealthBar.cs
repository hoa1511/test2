using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : HealthBar
{
    public override void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBar.fillAmount = currentHealth/maxHealth;
        if(currentHealth <= (maxHealth * 50 / 100) && currentHealth >= (maxHealth * 30 / 100))
        {
            healthBar.GetComponent<Image>().color = Color.yellow;

        }

        if(currentHealth < (maxHealth * 30 / 100) && currentHealth >= 0)
        {
            healthBar.GetComponent<Image>().color = Color.red; 
        }
    }

    
}
