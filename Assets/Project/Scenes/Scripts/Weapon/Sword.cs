using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IDamageable
{
    [SerializeField] private int swordDamage = 10;
    SpawnCoinTextFactory spawnCoinTextFactory;
    public void HandleDamage(int damage, ICanTakeDamage client)
    {
        client.TakeDamage(damage);
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.TryGetComponent(out ICanTakeDamage client))
        {
            Debug.Log(other.gameObject.name);
            int randomDamageTextJumpPosition = Random.Range(17,20);
            spawnCoinTextFactory.GetSpawnItem(other.transform.position + new Vector3(0,5, 0 ), other.transform.position + new Vector3(0,randomDamageTextJumpPosition + 5, 0 ),"- " + swordDamage.ToString());

            HandleDamage(swordDamage, client);
        }
    }
}
