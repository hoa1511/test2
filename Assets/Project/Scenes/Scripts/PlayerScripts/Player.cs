using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CanvasHaHa;
using DG.Tweening;
using SS.View;
using TMPro;
using System.Linq;

public class Player : MonoBehaviour, ICanTakeDamage, ICanPlaySound
{
    [SerializeField] private GameObject[] skill;
    [SerializeField] private GameObject[] objWantDrawOver;
    [SerializeField] private ParticleSystem pickedKeyParticle;
    [SerializeField] private Animator animatorDoor;
    [SerializeField] private GameObject scrollingTrap;
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshPro playerDisplayHealthText;
    [SerializeField] private GameObject keyUI;
    [SerializeField] private GameObject keyUIBar;
    [SerializeField] private AudioClip doorSFX;


    //Health Module
    [SerializeField] public float playerCurrentHealthValue;
    [SerializeField] public float playerMaxhealth;
    private float checkKey;
    //Health Module

    //Combat
    [SerializeField] private float weaponRange;

    private StatePlayer currentState;
    private PlayerHealthBar playerHealthBar;
    private bool isAniRunPlayed;
    private bool isAniIdlePlayed;
    private bool openDoor;
    private bool isHoldingWeapon;
    public bool isDead = false;
    public bool pickedKey;

    private int handDamage = 5;

    SpawnCoinTextFactory spawnCoinTextFactory;

    //Test Attack
    EnemyTest enemy;
    public List <EnemyTest> enemies;
    AIController enemyAIController;
    Animator playerAnimator;
    AudioSource audioSource;
        
    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        currentState = new IdlePlayerState(gameObject);
        currentState.Enter();

        //Health Module
        playerHealthBar = PlayerHealthBar.Instance;
        spawnCoinTextFactory = SpawnCoinTextFactory.Instance;

        playerMaxhealth = playerCurrentHealthValue;
        playerHealthBar.UpdateHealthBar(playerCurrentHealthValue, playerMaxhealth);

        checkKey = playerCurrentHealthValue;

    }


    void Update()
    {
        StartCombatEnemies();
        
        UpdateHealthBar();
        
        if(this.GetComponent<CarObject>().playerState == State.Finished && openDoor == false)
        {
            if(isAniIdlePlayed == false)
            {
                currentState.Exit();
                currentState = new IdlePlayerState(gameObject);
                currentState.Enter();

                isAniIdlePlayed = true;
                isAniRunPlayed = false;
            }
        }
    }

    public void IdlePlayer()
    {
        if(isAniIdlePlayed == false)
        {
            currentState.Exit();
            currentState = new IdlePlayerState(gameObject);
            currentState.Enter();

            isAniIdlePlayed = true;
            isAniRunPlayed = false;
        }
    }

    public void IdlePlayerWaitEnemy()
    {
        playerSpeed(0);
        currentState.Exit();
        currentState = new IdleWaitEnemy(gameObject);
        currentState.Enter();
    }

    public void RunPlayer()
    {
        if(isAniRunPlayed == false)
        {
            for(int i = 0; i < skill.Length; i++)
            {
                skill[i].SetActive(true);
            }

            turnOnBoxCollider();

            scrollingTrap. GetComponent<MovementObject>().enabled = true;

            currentState.Exit();
            currentState = new RunPlayerState(gameObject);
            currentState.Enter();

            isAniRunPlayed = true;
            isAniIdlePlayed = false;
        }
    }

    public void RunHoldingHand()
    {
        currentState.Exit();
        currentState = new RunPlayerState(gameObject);
        currentState.Enter();
    }

    public void RunPlayerHoldingSword()
    {
        currentState.Exit();
        currentState = new RunPlayerHoldingSword(gameObject);
        currentState.Enter();
    }

    public void RunSkill()
    {
        currentState.Exit();
        currentState = new RunSkillPlayer(gameObject);
        currentState.Enter();
    }

    private void stormPlayerBuff()
    {
        currentState.Exit();
        currentState = new StormBuff(gameObject);
        currentState.Enter();
    }

    private void diePlayerByTrap()
    {
        //skill[1].SetActive(false);
        stopMovingObstacle();
        playerSpeed(0);

        playerCurrentHealthValue -= playerCurrentHealthValue;

        currentState.Exit();
        currentState = new DiePlayerByTrap(gameObject);
        currentState.Enter();
    }


    private void diePlayerByCombat()
    {
        stopMovingObstacle();
        playerSpeed(0);
        GetComponent<CapsuleCollider>().enabled = false;

        currentState.Exit();
        currentState = new DiePlayerCombatState(gameObject);
        currentState.Enter();
        if(enemy != null)
        {
            enemy.EnemyDanceState();
        }
    }

    private void playerSpeed(float speed)
    {
        this.GetComponent<CarObject>().speed = speed;
    }

    private void stopMovingObstacle()
    {
        scrollingTrap.GetComponent<MovementObject>().enabled = false;
    }

    private void moveToNextLevel()
    {
        Vector3 curPos;
        transform.DOMoveX(0, 0.5f).OnComplete(()=>{
            transform.DOMoveZ(transform.position.z - 20, 2f).OnComplete(()=>{
                IdlePlayer();
                curPos = this.transform.position;
                GameManager.Instance.LoadNextLevel();
            });
        });
    }
    
    private void OnTriggerEnter(Collider collisionInfo)
    {
        if(collisionInfo.gameObject.tag == "Key")
        {
            PlaySound(doorSFX);
            pickedKey = true;
            keyUI.SetActive(true);
            keyUIBar.SetActive(true);
            collisionInfo.gameObject.SetActive(false);
            animatorDoor.SetBool("Open", true);
        }

        if(collisionInfo.gameObject.tag == "Door")
        {
            UISkill.Instance.pointerUp();

            this.GetComponent<CarObject>().enabled = false;

            openDoor = true;

            if(pickedKey == true)
            {
                moveToNextLevel();
                collisionInfo.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                if(isAniIdlePlayed == false)
                {
                    currentState.Exit();
                    currentState = new IdlePlayerState(gameObject);
                    currentState.Enter();

                    isAniIdlePlayed = true;
                }

            }
        }

        if(collisionInfo.gameObject.tag == "Trap")
        {
            diePlayerByTrap();
        }

        if(collisionInfo.gameObject.TryGetComponent(out ICollectable clientCollectabe))
        {
            clientCollectabe = collisionInfo.GetComponent<ICollectable>();
            clientCollectabe.HandleCollectItem();

            PlaySkill(collisionInfo.gameObject.tag);
           
        }

        if(collisionInfo.gameObject.TryGetComponent(out IHoldingItem clientHoldingItem))
        {
            clientHoldingItem = collisionInfo.GetComponent<IHoldingItem>();
            clientHoldingItem.HandleHoldingItem();
        }

        if(collisionInfo.gameObject.tag == "Enemy")
        {
            
            Debug.Log(collisionInfo.name);

            enemy = collisionInfo.GetComponent<EnemyTest>();
            enemyAIController = collisionInfo.GetComponent<AIController>(); 

            if(!enemies.Contains(enemy))
            {
                enemies.Add(enemy);
            }

            // if(enemy.isDead == false)
            // {
            //     transform.LookAt(enemy.transform);
            //     PlayerCombatEnemy(); 
            // }

            //StartCombatEnemies();
        }
    }

    public void StartCombatEnemies()
    {
        if(isDead == false)
        {
            if(enemies.Count > 0)
            {
                if(enemies[0].isDead == false)
                {
                    PlayerCombatEnemy(enemies[0]); 
                }   
            }
        }  
    }

    public void PlayerCombatEnemy(EnemyTest enemy)
    {
        playerSpeed(0);
        
        transform.LookAt(enemy.transform);

        enemyAIController.isHitByPlayer = true;

        currentState.Exit();

        if(isHoldingWeapon)
        {
            currentState = new PlayerWeaponCombat(gameObject);
            currentState.Enter();
        }

        if (!isHoldingWeapon)
        {
            currentState = new PlayerCombat(gameObject);
            currentState.Enter();
        }
    }

    private void turnOnBoxCollider()
    {
        for(int i = 0; i < objWantDrawOver.Length; i++)
        {
            if(objWantDrawOver[i].GetComponent<BoxCollider>() != null)
            {
                objWantDrawOver[i].GetComponent<BoxCollider>().enabled = true;
            }

            if(objWantDrawOver[i].GetComponent<MeshCollider>() != null)
            {
                objWantDrawOver[i].GetComponent<MeshCollider>().enabled = true;
            }

            if(objWantDrawOver[i].CompareTag("Door"))
            {
                objWantDrawOver[i].GetComponent<BoxCollider>().enabled = true;
            }

            else if(objWantDrawOver[i].CompareTag("Enemy"))
            {
                objWantDrawOver[i].GetComponent<CapsuleCollider>().enabled = true;
            }
            
        }
    }

    public void TakeDamage(int damage)
    {
        if(isHoldingWeapon == false)
        {
            playerAnimator.SetTrigger("PlayerTakeDamage");
        }
        PlayertakeDamage(damage);  
    }

    private void PlayertakeDamage(int damage)
    {
        playerCurrentHealthValue -= damage;

        if(playerCurrentHealthValue <= 0)
        {
            playerCurrentHealthValue = 0;
            isDead = true;
            diePlayerByCombat();
        }
    }

    private void UpdateHealthBar()
    {
        if(checkKey != playerCurrentHealthValue)
        {
            playerHealthBar.UpdateHealthBar(playerCurrentHealthValue, playerMaxhealth);
            checkKey = playerCurrentHealthValue;
        }
        UpdateHealthBarText();
    }

    private void UpdateHealthBarText()
    {
        playerDisplayHealthText.text = playerCurrentHealthValue.ToString();
    }

    private void PlaySkill(string skill)
    {
        switch(skill)
        {
            case "Sword":
            isHoldingWeapon = true;
            RunPlayerHoldingSword();
            break;
            
            case "Storm":
            stormPlayerBuff();
            break;
        }
    }

    public void HandleDamage(int damage)
    {
        int randomDamageTextJumpPosition = Random.Range(15,20);

        if(enemies.Count > 0)
        {
            enemies[0].TakeDamage(handDamage);
            spawnCoinTextFactory.GetSpawnItem( enemies[0].transform.position + new Vector3(0,5, 0 ),  enemies[0].transform.position + new Vector3(0,randomDamageTextJumpPosition, 0 ),"- " + handDamage.ToString());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, weaponRange);
    }

    public void RunAfterKillEnemy()
    {
        playerSpeed(10);

        if(isHoldingWeapon)
        {
            RunPlayerHoldingSword();
        }

        else
        {
            RunHoldingHand();
        }
    }

    public void RunFast()
    {
        GetComponent<Animator>().SetBool("FastRun", true);
    }

     public void NormalRun()
    {
        GetComponent<Animator>().SetBool("FastRun", false);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}


