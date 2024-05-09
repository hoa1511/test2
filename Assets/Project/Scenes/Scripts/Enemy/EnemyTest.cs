using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class EnemyTest : MonoBehaviour, ICanTakeDamage
{
    [SerializeField] private EnemyHealthBar enemyHealthBar;
    [SerializeField] private TextMeshPro enemyDisplayHealthText;
    [SerializeField] private float enemyMaxhealth;
    [SerializeField] public float enemyCurrentHealthValue;

    public int enemyDamage = 5;
    public int enemyFinalDamage = 10;
    private SpawnCoinTextFactory spawnCoinTextFactory;
    private NavMeshAgent navMeshAgent;
    private AIController aiController;

    private StateEnemy currentState;
    public Player player;

    private float checkKey;
        
    private bool isCombated;
    private bool isFinalCombated;
    public bool isDead;

    private void Start()
    {
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //Set Enemy State
        currentState = new EnemyIdleState(gameObject);
        currentState.Enter();
        
        enemyMaxhealth = enemyCurrentHealthValue;
        enemyHealthBar.UpdateHealthBar(enemyCurrentHealthValue,enemyMaxhealth);
        checkKey = enemyCurrentHealthValue;

        navMeshAgent = GetComponent<NavMeshAgent>();
        aiController = GetComponent<AIController>();
    }

    private void Update()
    {
        UpdateHealthBar();
        if(enemyCurrentHealthValue <= 0 && isDead == false)
        {
            isDead = true;

            enemyCurrentHealthValue = 0;

            aiController.isDie = true;
            aiController.enabled = false;

            this.GetComponent<CapsuleCollider>().enabled = false;

            player.enemies.Remove(this);

            if(player.isDead == false &&  player.enemies.Count == 0)
            {
                player.RunAfterKillEnemy();
            }
            
            EnemyDieState();
        }
    }

    private void UpdateHealthBar()
    {
        if(checkKey != enemyCurrentHealthValue)
        {
            enemyHealthBar.UpdateHealthBar(enemyCurrentHealthValue, enemyMaxhealth);
            checkKey = enemyCurrentHealthValue;
        }
        UpdateHealthBarText();
    }

    public void EnemyTakeDamage(int damage)
    {
        enemyCurrentHealthValue -= damage;

        if(enemyCurrentHealthValue <= 30 && enemyCurrentHealthValue > 0)
        {
            EnemyFinalCombatState();
        }
    }

    private void UpdateHealthBarText()
    {
        enemyDisplayHealthText.text = enemyCurrentHealthValue.ToString();
    }

    public void EnemyLookingState()
    {
        currentState.Exit();
        currentState = new EnemyIdleState(gameObject);
        currentState.Enter();
    }

    public void EnemyWalkState()
    {
        currentState.Exit();
        currentState = new EnemyWalkState(gameObject);
        currentState.Enter();
    }

    public void EnemyChasingState()
    {
        currentState.Exit();
        currentState = new EnemyChasingState(gameObject);
        currentState.Enter();
    }
    public void EnemyCombatState()
    {
        if(isDead == false)
        {
            if(isCombated == false)
            {
                currentState.Exit();
                currentState = new EnemyCombatState(gameObject);
                currentState.Enter();
                isCombated = true;
            }
        }
    }

    public void EnemyFinalCombatState()
    {    
        if(isDead == false)
        {
            if(isFinalCombated == false)
            {
                currentState.Exit();
                currentState = new EnemyFinalCombatState(gameObject);
                currentState.Enter();  
                isFinalCombated = true;
            }
        } 
    }

    public void EnemyDieState()
    {
        currentState.Exit();
        currentState = new EnemyDieState(gameObject);
        currentState.Enter();
    }

    public void EnemyDanceState()
    {
        currentState.Exit();
        currentState = new EnemyDance(gameObject);
        currentState.Enter();
    }

    public void HandleDamage(int damage)
    {
        int randomDamageTextJumpPosition = Random.Range(15,20);

        if(player.isDead == false)
        {
            if(enemyCurrentHealthValue > 30)
            {
                player.TakeDamage(damage);
                spawnCoinTextFactory.GetSpawnItem(player.transform.position + new Vector3(0,10, 0 ), player.transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"- " + damage.ToString());
            }
            else
            {
                player.TakeDamage(enemyFinalDamage);
                 spawnCoinTextFactory.GetSpawnItem(player.transform.position + new Vector3(0,5, 0 ), player.transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"- " + enemyFinalDamage.ToString());
            }
            EnemyDance();
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("enemyHit");
        EnemyTakeDamage(damage);
    }

    private void EnemyDance()
    {
        if(player.isDead && isDead == false)
        {
            EnemyDanceState();
        }
    }
}
