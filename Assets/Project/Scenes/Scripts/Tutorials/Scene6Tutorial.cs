using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SS.View;
using CanvasHaHa;

public class Scene6Tutorial : MonoBehaviour
{
    [SerializeField] public bool isPlayTutorial = false;

    private bool isHitItem = false;
    private bool isCompletePlayTutorial = false;
    private bool isLoadPopup = false;
    private Player player;

    private void Update()
    {
        DispplayGameScene5Tutorials();

        if(isPlayTutorial)
        {
            if(Input.GetMouseButtonDown(0))
            {
                ReturnToGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(isHitItem == false && isCompletePlayTutorial == false)
            {
                player = other.gameObject.GetComponent<Player>();

                player.IdlePlayerWaitEnemy();

                isHitItem = true;
                isPlayTutorial = true;
            }
        }
    }

    private void DispplayGameScene5Tutorials()
    {
        if(isHitItem && isLoadPopup == false)
        {
            Manager.Add(TutorialScene2Controller.TUTORIALSCENE2_SCENE_NAME);
            this.GetComponent<MeshCollider>().enabled = false;
            isPlayTutorial = true;
            isLoadPopup = true;
        }
    }

    public void ReturnToGame()
    {
        if(isPlayTutorial)
        {
            player.GetComponent<CarObject>().speed = 10;
            player.RunHoldingHand();
            isPlayTutorial = false;
            isCompletePlayTutorial = true;
        } 
    }
}
