using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeObstacle : Obstacle
{
    [SerializeField] private Transform[] checkPoint;
    private Transform spikeObstacleTransform;
    private Sequence sequence;

    public void HandleMovement()
    {
        sequence = DOTween.Sequence();
        sequence
        .Append
        (
             spikeObstacleTransform.transform.DOMoveX(checkPoint[1].position.x, 1f) 
        )
        .Append
        (
             spikeObstacleTransform.transform.DOMoveX(checkPoint[0].position.x, 1f)
        )
        .Append
        (
            spikeObstacleTransform.transform.DOMoveX(checkPoint[2].position.x, 1f)
        )
        .Append
        (
            spikeObstacleTransform.transform.DOMoveX(checkPoint[0].position.x, 1f)
        )
        .SetLoops(-1).SetUpdate(true);
        ;
    }
    
    protected override void Start()
    {
        spikeObstacleTransform = this.transform;
        HandleMovement();
    }
}
