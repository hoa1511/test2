using UnityEngine;
using UnityEngine.UI;

public class UISkill : MonoBehaviour
{
    [SerializeField] private GameObject[] colorSlowMotionMap;
    [SerializeField] private GameObject[] colorSlowMotionEnemy;
    [SerializeField] private Material[] materialsMap;
    [SerializeField] private Material[] materialsSkinEnemy;
    [SerializeField] private GameObject slider;
    [SerializeField] private float skillTime;


    [HideInInspector] public float timeHold;
    [HideInInspector] public bool isOnSkillTime;
    [HideInInspector] public bool isHoldButton;

    [SerializeField] private Scene4Tutorial scene4Tutorial;

    private float fastMotion = 2.5f;
    private float normalTime = 1f;

    public static UISkill Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start() 
    {
        timeHold = skillTime;
        UpdateSlider();
    }

    private void Update() 
    {
        if(isHoldButton == true && isOnSkillTime == true)
        {
            DoFastMotion(); 
        }
        else
        {
            NormalMotion();
        }
    }

    private void FixedUpdate() 
    {
        if(isHoldButton == true)
        {
            timeHold += Time.unscaledDeltaTime;
            
            if(Mathf.Round(timeHold * 10.0f) * 0.1f >= skillTime)
            {
                changeMaterialMap(1);
                isOnSkillTime = false;
                slider.GetComponent<Image>().fillAmount = 0;
            }
            else
            {
                slider.GetComponent<Image>().fillAmount = (skillTime - timeHold) / skillTime;

                isOnSkillTime = true;
                changeMaterialMap(0);
            }
        }
    }

    public void pointerUp()
    {
        GetComponent<Player>().NormalRun(); 
        changeMaterialMap(1);
        isHoldButton = false;
    }

    public void pointerDown()
    {
        if(timeHold <= skillTime)
        {
            GetComponent<Player>().RunFast();
        }
        if(scene4Tutorial != null)
        {
            scene4Tutorial.ReturnToGame();
            scene4Tutorial.enabled = false;
        }
        isHoldButton = true;  
    }

    public void DoFastMotion()
    {
        fastMotionMovment();
    }

    private void changeMaterialMap(int materialUWant)
    {
        for(int i = 0; i < colorSlowMotionMap.Length; i++)
        {
            colorSlowMotionMap[i].GetComponent<Renderer>().material = materialsMap[materialUWant];
        }

        for(int i = 0; i < colorSlowMotionEnemy.Length; i++)
        {
            colorSlowMotionEnemy[i].GetComponent<Renderer>().material = materialsSkinEnemy[materialUWant];
        }
    }
    private void fastMotionMovment()
    {
        Time.timeScale = fastMotion;
    }

    private void NormalMotion()
    {
        Time.timeScale = normalTime;
    }

    public void UpdateSlider()
    {
        slider.GetComponent<Image>().fillAmount = (skillTime - timeHold) / skillTime;
    }
}
