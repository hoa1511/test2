using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordItem : MonoBehaviour, ICollectable
{
    [SerializeField] private GameObject rightHandSword;
    [SerializeField] private GameObject leftHandSword;
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
        
        spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"Pick Up Sword");

        rightHandSword.gameObject.SetActive(true);
        leftHandSword.gameObject.SetActive(true);
        UnableItem();
    }

    private void RotateItem()
    {
        Vector3 rotateAxis = new Vector3(0,0,-1);
        transform.Rotate(rotateAxis * Time.unscaledDeltaTime * 50);
    }

    private void UnableItem()
    {
        gameObject.GetComponent<MeshCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        gameObject.transform.GetChild(1).gameObject.SetActive(false);
    }
}
