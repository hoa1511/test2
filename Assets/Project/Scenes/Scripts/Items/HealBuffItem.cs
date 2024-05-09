using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealBuffItem : MonoBehaviour, ICollectable
{
    [SerializeField] private Player player;
    [SerializeField] private float amountToRestore;
    [SerializeField] GameObject healthEffect;
    
    private SpawnCoinTextFactory spawnCoinTextFactory;


    public void HandleCollectItem()
    {
        StartCoroutine(RestoreHealth());
    }

    private void Start()
    {
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;
    }

    private void Update()
    {
        RotateItem();
    }


    private void RotateItem()
    {
        Vector3 rotateAxis = new Vector3(0,1,0);
        transform.Rotate(rotateAxis * Time.unscaledDeltaTime * 50, Space.World);
    }

    public IEnumerator RestoreHealth()
    {
        DisableItem();
        
        int randomTextJumpPosition = Random.Range(15,20);

        spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,randomTextJumpPosition, 0 ),"+ " + amountToRestore.ToString() + " hp");

       
        healthEffect.SetActive(true);
        

        for(int i = 0; i < amountToRestore; i++)
        {
            player.playerCurrentHealthValue += 1f;

            if(player.playerCurrentHealthValue >= player.playerMaxhealth)
            {
                player.playerCurrentHealthValue = player.playerMaxhealth;
                healthEffect.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=>{
                    healthEffect.SetActive(false);
                });
                yield break;
            }
            yield return new WaitForSeconds(0.1f);    
        }

        healthEffect.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=>{
            healthEffect.SetActive(false);
        });
    }

    private void DisableItem()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.1f);
        gameObject.transform.GetComponent<BoxCollider>().enabled = false;
    }
}
