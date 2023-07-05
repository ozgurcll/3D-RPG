using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelManager : MonoBehaviour
{
    private int currentExp;
    private int level;
    private int nextLevel;
    private int expToNextLevel;
    public int GetLevel { get { return level + 1; } }
    public static LevelManager instance;

    public Image expBar;
    public GameObject nextLevelEffect;
    private Transform player;
    public Text levelText;
    public Text nextLevelText;

    private void Awake()
    {
        player = GameObject.Find("Player").gameObject.transform;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        level = 0;
        nextLevel = 1;
        currentExp = 0;
        expToNextLevel = 100;
        expBar.fillAmount = 0;
        UpdateLevelText();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) AddExp(25);

    }

    public void AddExp(int amount)
    {
        currentExp += amount;
        expBar.fillAmount = (float)currentExp / expToNextLevel;
        if (currentExp >= expToNextLevel)
        {
            level++;
                       GameObject NextLevelEffect = Instantiate(nextLevelEffect, player.position, Quaternion.identity);
            NextLevelEffect.transform.SetParent(player);

            UpdateLevelText();
            currentExp -= expToNextLevel;
            expBar.fillAmount = 0f;
        }
    }

    void UpdateLevelText()
    {
        nextLevel += 1;
        levelText.text = GetLevel.ToString();
        nextLevelText.text = nextLevel.ToString();
    }
    private void OnEnable()
    {
        EnemyHealth.onDeath += AddExp;
    }
    private void OnDisable()
    {
        EnemyHealth.onDeath -= AddExp;
    }
}
