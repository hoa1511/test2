using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CanvasHaHa;
using SS.View;

public class Scene5Tutorial : MonoBehaviour
{
    [SerializeField] public bool isPlayTutorial = false;
    [SerializeField] TextMeshProUGUI TutorialText;
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
            Manager.Add(TutorialSceneController.TUTORIALSCENE_SCENE_NAME);
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
            player.RunPlayerHoldingSword();
            isPlayTutorial = false;
            isCompletePlayTutorial = true;
        } 
    }
}
