using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CanvasHaHa;

public class AIController : MonoBehaviour
{
    [SerializeField] protected PatrolPath patrolPath;

    
    [SerializeField] protected float chaseDistance;
    [SerializeField] protected float weaponRange;
    [SerializeField] protected float waypointTolerance = 0.1f;
    [SerializeField] protected float waypointDwellTime = 3f;
    [SerializeField] protected EnemyVision enemyVision;
    [SerializeField] protected float normalSpeed = 3;

    protected int currentWaypointIndex = 0;
    protected float timeSinceArrivedAtWaypoint = 0;
    protected bool hasSeenTarget = false;
    public bool isDie = false;
    public bool isHitByPlayer = false;
    protected bool hasStartLockingTarget = false;
    protected bool hasStartAttack = false;
    public bool isWaiting = false;
    


    protected NavMeshAgent navMeshAgent;
    protected GameObject player;
    protected EnemyTest enemyTest;
    protected Vector3 enemyPosition;
    protected UISkill uiSkill;

    protected CarObject carObject;


    private void Start()
    {
        enemyTest = GetComponent<EnemyTest>();

        navMeshAgent = GetComponent<NavMeshAgent>();

        player = GameObject.FindWithTag("Player");

        carObject = player.GetComponent<CarObject>();

        enemyPosition = transform.position;

        uiSkill = FindObjectOfType<UISkill>();
    }

    protected virtual void Update()
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

            if(enemyVision.hasDetectTarget == false && InWeaponRange() == false && hasStartLockingTarget == false && hasStartAttack == false)
            {
                PatrolBehaviour();
            }
            
            if(enemyVision.hasDetectTarget == true && InWeaponRange() == false && hasStartLockingTarget == false && hasStartAttack == false|| isHitByPlayer == true && hasStartLockingTarget == false && InWeaponRange() == false && hasStartAttack == false)
            {
                player.GetComponent<Player>().IdlePlayerWaitEnemy();
                MoveTo(player.gameObject.transform.position);
                hasStartLockingTarget = true;
            }

            if(InWeaponRange() == true && hasStartAttack == false)
            {
                enemyVision.gameObject.SetActive(false);
                
                Cancel();

                StartAttackBehaviour();
                
                hasStartAttack = true;
            } 
        }
        

        UpdateTimers();
    }

    public virtual void MoveTo(Vector3 destination)
    {  
        navMeshAgent.destination = destination;
        navMeshAgent.isStopped = false;
    }

    public void Cancel()
    {
        navMeshAgent.enabled = false;
    }

    public void StartAttackBehaviour()
    {
        if(isDie == false)
        {
            transform.LookAt(player.transform);
            enemyTest.EnemyCombatState();
        }
    }

    protected bool InWeaponRange()
    {
        return Vector3.Distance(transform.position, player.transform.position) <= weaponRange;
    }

    protected Vector3 GetCurrentWaypoint()
    {
        return patrolPath.GetWaypoint(currentWaypointIndex);
    }

    protected void CycleWaypoint()
    {
        currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
    }

    protected bool AtWaypoint()
    {
        float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
        return distanceToWaypoint <= waypointTolerance;
    }

    protected void PatrolBehaviour()
    {
        
        Vector3 nextPosition = enemyPosition;

        if (patrolPath != null)
        {
            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                enemyTest.EnemyLookingState();
                CycleWaypoint();
            }

            nextPosition = GetCurrentWaypoint();
        }
        
        if(timeSinceArrivedAtWaypoint > waypointDwellTime)
        {
            MoveTo(nextPosition);
        } 
    }

    protected void EnemyWaiting()
    {
        navMeshAgent.speed = 0;
        enemyTest.EnemyLookingState();
    }

    protected void UpdateTimers()
    {
        timeSinceArrivedAtWaypoint += Time.deltaTime;
    }
}
