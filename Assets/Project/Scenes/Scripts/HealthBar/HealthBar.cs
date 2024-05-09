using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] protected Image healthBar;
    private Quaternion rotation;
    public virtual void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        //Handle Update Health Bar
    }
    protected virtual void Start()
    {
        rotation = transform.rotation;
    }

    protected virtual void Awake()
    {
        
    }

    protected virtual void LateUpdate()
    {
        RotateHealthBar();
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void RotateHealthBar()
    {
        transform.rotation = rotation;
    }
}
