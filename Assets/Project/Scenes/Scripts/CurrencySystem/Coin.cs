using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable, ICanPlaySound
{
    
    [SerializeField] private int coinReward;  
    [SerializeField] private AudioClip clip;

    private SpawnCoinTextFactory spawnCoinTextFactory;
    private Vector3 rotateAxis;
    private AudioSource audioSource;

    private float rotateSpeed;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;
        rotateAxis = new Vector3(1,0,0);
        rotateSpeed = 50;
    }

    private void Update()
    {
        RotateCoin();
    }
    private void RotateCoin()
    {
        transform.Rotate(rotateAxis * Time.unscaledDeltaTime * rotateSpeed);
    }

    public void HandleCollectItem()
    {
        PlaySound(clip);

        int randomCoinTextJumpPosition = Random.Range(3,5);

        Bank.Instance.Deposit(coinReward);
        spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,15, 0 ), "+ " +coinReward.ToString());

        //Destroy(gameObject);
        DisableCoin();
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    private void DisableCoin()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
    }
}
