using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnCoin : MonoBehaviour, ISpawnItem
{
    private Transform player;
    private Vector3 positionToJumpIn;
    private bool canMoveToGamObject = false;

    private void Update()
    {
        JumpToPlayer();
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;   
    }

    public void InitializeItem(Vector3 positionToJumpOut)
    {
        float randomTime = Random.Range(2f, 3f);
        Vector3 itemPickupScale = new Vector3(80,80,80);

        transform.localScale = new Vector3(0,0,0);
        transform.DOJump(positionToJumpOut,Random.Range(2,6),1,Random.Range(0.4f,0.6f));
        transform.DOScale(itemPickupScale,0.2f).OnComplete(() => {
            transform.DOScale(itemPickupScale, 2).OnComplete(() => {
                canMoveToGamObject = true;
                transform.DOScale(new Vector3(20,20,20), 0.5f);
            });
        });  
    }

    private void JumpToPlayer()
    {
        if(canMoveToGamObject == true)
        {
            positionToJumpIn = player.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, positionToJumpIn, 0.5f);
            Destroy(gameObject, 1);
        }
    }
}
