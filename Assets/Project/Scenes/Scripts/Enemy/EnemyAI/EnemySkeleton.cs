using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : AIController
{
    public override void MoveTo(Vector3 destination)
    {
        if(enemyVision.hasDetectTarget == false)
        {
            enemyTest.EnemyWalkState();
        }
        else if(enemyVision.hasDetectTarget == true)
        {
            navMeshAgent.speed = 20;
            enemyTest.EnemyChasingState();
        }

        base.MoveTo(destination);
    }
}
