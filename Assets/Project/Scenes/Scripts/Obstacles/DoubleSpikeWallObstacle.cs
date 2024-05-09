using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoubleSpikeWallObstacle : MonoBehaviour
{
    private Transform spikeWallTransform;
    private Transform leftSpikeWall;
    private Transform rightSpikeWall;
    [SerializeField] Transform endpoint1;
    [SerializeField] Transform[] leftSpikeCheckPoints;
    [SerializeField] Transform[] rightSpikeCheckPoints;
    [SerializeField] float timeComeIn = 0.5f;
    [SerializeField] float timeComeOut = 2f;
    [SerializeField] float startDelayTime = 2f;
    [SerializeField] float delayTime = 2f;


    private Sequence sequence;

    void Start()
    {

        spikeWallTransform = this.transform;

        leftSpikeWall = spikeWallTransform.GetChild(0);
        rightSpikeWall = spikeWallTransform.GetChild(1);

        HandleMovement();
    }

    public void HandleMovement()
    {
        sequence = DOTween.Sequence();
        sequence
        .Append
        (
             leftSpikeWall.transform.DOScaleX(leftSpikeWall.transform.localScale.x, startDelayTime)
        )
        .Join
        (
            rightSpikeWall.transform.DOScaleX(rightSpikeWall.transform.localScale.x, startDelayTime)
        )
        .Append
        (
             leftSpikeWall.transform.DOMoveX(leftSpikeCheckPoints[2].position.x, timeComeIn)
        )
        .Join
        (
            rightSpikeWall.transform.DOMoveX(rightSpikeCheckPoints[2].position.x, timeComeIn)
        )
        .Append
        (
            leftSpikeWall.transform.DOMoveX(leftSpikeCheckPoints[0].position.x, timeComeOut)
        )
        .Join
        (
            rightSpikeWall.transform.DOMoveX(rightSpikeCheckPoints[0].position.x, timeComeOut)
        )

        .Append
        (
             leftSpikeWall.transform.DOScaleX(leftSpikeWall.transform.localScale.x, delayTime)
        )
        .Join
        (
            rightSpikeWall.transform.DOScaleX(rightSpikeWall.transform.localScale.x, delayTime)
        )
        .SetLoops(-1).SetUpdate(true);
        ;
    }
}
