using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCubeEnemy : MonoBehaviour, IDamageable
{
    [SerializeField] private int enemyDamage = 10;
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
            int randomDamageTextJumpPosition = Random.Range(10,20);
            spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"- " + enemyDamage.ToString());

            HandleDamage(enemyDamage, client);
        }
    }
}
