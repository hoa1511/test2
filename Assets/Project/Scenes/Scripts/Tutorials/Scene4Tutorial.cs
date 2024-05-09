using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CanvasHaHa;

public class Scene4Tutorial : MonoBehaviour
{
   [SerializeField] TextMeshProUGUI TutorialText;
   [SerializeField] UISkill uiSkill;
   public bool isPlayTutorial = false;
   private bool isHitItem = false;
   private Player player;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DispplayGameScene4Tutorials();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isHitItem == false)
            {
                player = other.gameObject.GetComponent<Player>();

                player.IdlePlayerWaitEnemy();

                isHitItem = true;
                isPlayTutorial = true;
            }
        }
    }

    private void DispplayGameScene4Tutorials()
    {
        if(isHitItem)
        {
            TutorialText.gameObject.SetActive(true);
            this.GetComponent<MeshCollider>().enabled = false;
            isPlayTutorial = true;
        }
    }

    public void ReturnToGame()
    {
        if(isPlayTutorial)
        {
            TutorialText.gameObject.SetActive(false);
            player.GetComponent<CarObject>().speed = 10;
            player.RunSkill();
            isPlayTutorial = false;
        } 
    }
}
