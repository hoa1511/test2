using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SpawnCoinText : MonoBehaviour, ISpawnText
{
    private TextMeshPro coinValueText;
    Vector3 spawnTextScale = new Vector3(1f,1f,1f);
    
    public void InitializeItem(Vector3 positionToJump, string textToTransfer)
    {
        coinValueText = transform.GetComponent<TextMeshPro>();

        coinValueText.text = textToTransfer;

        transform.localScale = new Vector3(0.3f,0.3f,0.3f);
        
        transform.DOMove(positionToJump, 3f).SetUpdate(true);
        transform.DOScale(spawnTextScale,0.5f).OnComplete(()=>{
            Destroy(transform.gameObject,0.1f);
        });  
    }
}
