using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoinTextFactory : TextFactory
{
    [SerializeField] private SpawnCoinText spawnCoinTextPrefab;
    [SerializeField] private Transform spawnCoinTextHoldingObject;

    public static SpawnCoinTextFactory Instance;

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

    public override ISpawnText GetSpawnItem(Vector3 spawnPosition, Vector3 positionToJump, string textToTransfer)
    {
        GameObject instance = Instantiate(spawnCoinTextPrefab.gameObject, spawnPosition, Quaternion.Euler(0,180,0), spawnCoinTextHoldingObject);
        
        SpawnCoinText spawnCoinText = instance.GetComponent<SpawnCoinText>();

        spawnCoinText.InitializeItem(positionToJump, textToTransfer);

        return spawnCoinTextPrefab;
    }
}
