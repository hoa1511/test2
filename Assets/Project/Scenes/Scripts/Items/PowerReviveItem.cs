using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PowerReviveItem : MonoBehaviour, ICollectable
{
    [SerializeField] private float amountToRevive = 5;
    [SerializeField] GameObject reviveEffect;
    private SpawnCoinTextFactory spawnCoinTextFactory;

    private UISkill skill;
    private void Start()
    {
        skill = UISkill.Instance;
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;    
    }
    public void HandleCollectItem()
    {
        StartCoroutine(ReviveSkill());
    }

    private void Update() 
    {
        RotateItem();
    }

    IEnumerator ReviveSkill()
    {
        DisableItem();
        
        int randomTextJumpPosition = Random.Range(15,20);

        spawnCoinTextFactory.GetSpawnItem(transform.position, transform.position + new Vector3(0,randomTextJumpPosition, 0 ),"+ " + amountToRevive.ToString() + " health");

        reviveEffect.SetActive(true);

        // for(int i = 0; i < amountToRevive * 100; i++)
        // {
        //     skill.timeHold -= 0.01f;
        //     if(skill.timeHold <= 0)
        //     {
        //         skill.timeHold = 0;
        //         yield return null;
        //     }
        //     skill.UpdateSlider();
        //     yield return new WaitForSeconds(0.01f);
        // }
        skill.timeHold -= amountToRevive;
        skill.UpdateSlider();
        yield return new WaitForSeconds(0);

        reviveEffect.transform.DOScale(Vector3.zero, 0.2f).OnComplete(()=>{
            reviveEffect.SetActive(false);
        });
    }
    private void DisableItem()
    {
        gameObject.transform.DOScale(Vector3.zero, 0.1f);
        gameObject.transform.GetComponent<MeshCollider>().enabled = false;
    }

    private void RotateItem()
    {
        Vector3 rotateAxis = new Vector3(0,1,0);
        transform.Rotate(rotateAxis * Time.unscaledDeltaTime * 50, Space.World);
    }
}
