using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingSawBladeObstacle : MonoBehaviour
{
    [SerializeField] private Transform endPoint;
    private GameObject movingSawBladeObstacle;
    private GameObject sawblade;
    private float firstPostionValue;

    private void Start()
    {
        movingSawBladeObstacle = this.gameObject;

        sawblade = movingSawBladeObstacle.transform.GetChild(0).gameObject;
          
        HandleMovement();

    }

    public void HandleMovement()
    {
        sawblade.transform.DOMoveX(endPoint.position.x, 1f).SetEase(Ease.InOutSine).SetLoops(-1,LoopType.Yoyo).SetUpdate(true);;
    }
}
