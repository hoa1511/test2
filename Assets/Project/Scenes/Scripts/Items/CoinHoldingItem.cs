using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinHoldingItem : MonoBehaviour, IHoldingItem
{
    [SerializeField] private int numberOfCoinHolding = 10;
    private Transform chestHead;
    private SpawnCoinFactory spawnCoinFactory;
    private void Start()
    {
        spawnCoinFactory = SpawnCoinFactory.Instance;
        chestHead = transform.GetChild(1);
    }
    public void HandleHoldingItem()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        StartCoroutine(OpenChestAnimation());
        for(int i = 0; i < numberOfCoinHolding; i++)
        {
            float randomXPos = Random.Range(-5f, 5f);
            float randomZPos = Random.Range(-5f, 5f);
            StartCoroutine(SpawnDropCoin(transform.position,new Vector3(transform.position.x + randomXPos, 0f, transform.position.z + randomZPos)));
        }
    }

    private IEnumerator SpawnDropCoin(Vector3 spawnItemPosition, Vector3 positionToJumpOut)
    {
        spawnCoinFactory.GetSpawnItem(spawnItemPosition, positionToJumpOut);
        yield return new WaitForSeconds(0);
        
    }
    private IEnumerator OpenChestAnimation()
    { 
        chestHead.DORotate(new Vector3(45,chestHead.transform.rotation.y,chestHead.transform.rotation.z),0f, RotateMode.LocalAxisAdd);
        yield return new WaitForSeconds(1);
    }
}
