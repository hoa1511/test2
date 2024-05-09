using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormBuffItem : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject rightHandEffect;
    [SerializeField] private GameObject leftHandEffect;
    SpawnCoinTextFactory spawnCoinTextFactory;

    public void HandleCollectItem()
    {
        ActivateSword();
    }

    private void Start()
    {
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;
    }

    private void Update()
    {
        RotateItem();
    }

    public void ActivateSword()
    {
        int randomDamageTextJumpPosition = Random.Range(10,20);
        
        spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"Storm Upp");

        rightHandEffect.gameObject.SetActive(true);
        leftHandEffect.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void RotateItem()
    {
        Vector3 rotateAxis = new Vector3(0,0,-1);
        transform.Rotate(rotateAxis * Time.unscaledDeltaTime * 50);
    }
}
