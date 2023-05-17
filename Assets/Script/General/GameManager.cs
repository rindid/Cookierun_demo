using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//싱글톤 패턴 참고 : https://glikmakesworld.tistory.com/2
public class GameManager : MonoBehaviour
{
    [Range(0, 300)]
    public float health = 300;
    public float abilityIntervalTime = 30f;
    public float abilityDurationTime = 5f;
    private float maxHealth;
    private static GameManager instance = null;
    private UIManager gameUIManager = null;
    private JellyManager gameJellyManager = null;
    private float gameTimer = 0;
    private float abilityTimer = 0;
    private float oneSecondTimer = 0;
    private int jellySpawnCount = 0;
    private float nextReduceHealth = 0;
    private int gameScore = 0;
    private int jellySpawnInterval = 5;
    private int healtReduceInterval = 1;
    private float healthReduceAmount = 3.2f;
    private float hurtAmount = 20f;
    private bool isGameOver = false;
    private bool isAbilityOn = false;
    void Awake() 
    {
        if(null ==  instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameTimer = 0;
        abilityTimer = 0;
        oneSecondTimer = 0;
        gameScore = 0;
        jellySpawnCount = 0;
        nextReduceHealth = 0;
        maxHealth = health;
        isGameOver = false;
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameOver)
        {
            gameTimer += Time.deltaTime;
            abilityTimer += Time.deltaTime;
            if(abilityTimer >= abilityIntervalTime)
            {
                //ability
                SetAbility(true);
                abilityTimer = 0;
            }
            if(jellySpawnCount*jellySpawnInterval < gameTimer)
            {
                MakeJelly();
                jellySpawnCount += 1;
            }
            nextReduceHealth += Time.deltaTime;
            if(nextReduceHealth > healtReduceInterval)
            {
                health -= healthReduceAmount;
                if(health<=0)
                {
                    health = 0;
                    GameOver();
                }
                nextReduceHealth=0;
            }
            oneSecondTimer += Time.deltaTime;
            if(oneSecondTimer >= 1.0f)
            {
                UpdateUI();
                oneSecondTimer = 0;
            }
            
        }
    }

    public static GameManager Instance
    {
        get
        {
            if(null ==  instance)
            {
                return null;
            }
            return instance;
        }
    }
    public UIManager GameUIManager
    {
        get
        {
            if(null == instance || null ==  instance.gameUIManager)
            {
                return null;
            }
            return instance.gameUIManager;
        }
        set
        {
            if(null != instance && null ==  instance.gameUIManager)
            {
                instance.gameUIManager = value;
            }
        }
    }
    public JellyManager GameJellyManager
    {
        get
        {
            if(null == instance || null ==  instance.gameJellyManager)
            {
                return null;
            }
            return instance.gameJellyManager;
        }
        set
        {
            if(null != instance && null ==  instance.gameJellyManager)
            {
                instance.gameJellyManager = value;
            }
        }
    }
    public void UpdateUI()
    {
        if (instance != null && instance.GameUIManager != null)
        {
            instance.GameUIManager.UpdateUI();
        }
    }
    public float GetTime()
    {
        return gameTimer;
    }
    public int GetScore()
    {
        return gameScore;
    }
    public void AddScore(int addedScore)
    {
        gameScore += addedScore;
        UpdateUI();
    }
    public void MakeJelly()
    {
        int jellyChunkCount = 10;
        instance.GameJellyManager.SpawnJelly(jellySpawnCount*jellyChunkCount, jellyChunkCount);
    }
    public void SetAbility(bool onOff)
    {
        isAbilityOn = onOff;
    }
    public bool IsAbilityOn()
    {
        return isAbilityOn;
    }
    public float GetAbilityDurationTime()
    {
        return abilityDurationTime;
    }
    public void Hurt()
    {
        health -= hurtAmount;
        if(health <= 0)
        {
            GameOver();
        }
        UpdateUI();
    }
    public float HealthPercentage()
    {
        return health / maxHealth;
    }
    public void GameOver()
    {
        isGameOver = true;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }

}
