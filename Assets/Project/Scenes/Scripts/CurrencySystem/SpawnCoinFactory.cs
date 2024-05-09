using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinFactory : Factory
{
    [SerializeField] private SpawnCoin spawnCoinPrefab;
    [SerializeField] private Transform spawnCoinHoldingObject;

    public static SpawnCoinFactory Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        
    }

    public override ISpawnItem GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJumpOut)
    {
        GameObject instance = Instantiate(spawnCoinPrefab.gameObject, spawnPosition, Quaternion.identity, spawnCoinHoldingObject);
        
        SpawnCoin spawnCoin = instance.GetComponent<SpawnCoin>();

        spawnCoin.InitializeItem(positionToJumpOut);

        return spawnCoin;
    }
}
