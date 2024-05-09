using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeWallObstacle : MonoBehaviour,  IDamageable
{
    private int damage = 100;
    
    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        client.TakeDamage(damage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            ICanTakeDamage client;
            client = other.GetComponent<ICanTakeDamage>();
            HandleDamage(damage,client);
        }
    }
}
