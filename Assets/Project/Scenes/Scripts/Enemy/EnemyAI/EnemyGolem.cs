using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CanvasHaHa;

public class EnemyGolem : AIController
{
    public override void MoveTo(Vector3 destination)
    {
        if(enemyVision.hasDetectTarget == false)
        {
            enemyTest.EnemyWalkState();
        }
        else if(enemyVision.hasDetectTarget == true)
        {
            enemyTest.EnemyChasingState();
        }
        
        base.MoveTo(destination);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    protected override void Update() 
    {
        if(carObject.playerState == State.Draw)
        {
            isWaiting = true;
            EnemyWaiting();
        }
        
        if(carObject.playerState == State.Movement)
        {
            isWaiting = false;
        }

        if(carObject.playerState == State.Start)
        {
            isWaiting = false;
        }

        if(enemyTest.isDead == false && isWaiting == false)
        {
            if(uiSkill.isOnSkillTime == true && uiSkill.isHoldButton == true)
            {
                navMeshAgent.speed = 1.5f;
            }
            else
            {
                
                navMeshAgent.speed = normalSpeed;
            }

            if(InChasingRange() == false && InWeaponRange() == false && hasStartLockingTarget == false && hasStartAttack == false)
            {
                PatrolBehaviour();
            }
            
            if(InChasingRange() == true && InWeaponRange() == false && hasStartAttack == false)
            {
                player.GetComponent<Player>().IdlePlayerWaitEnemy();
                
                MoveTo(player.gameObject.transform.position);
                
            }

            if(InWeaponRange() == true && hasStartAttack == false)
            {            
                navMeshAgent.enabled = false;

                transform.GetChild(0).gameObject.SetActive(false);

                StartAttackBehaviour();
                
                hasStartAttack = true;
            } 
        }
        
        UpdateTimers();
    }

    private bool InChasingRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        return distanceToPlayer <= chaseDistance;
    }
    
}
