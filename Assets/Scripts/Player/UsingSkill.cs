using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsingSkill : MonoBehaviour
{
    [Header("Mana Settings")]
    public float totalMana = 100f;
    public float manaRegenSpeed = 2f;
    private Image manaBar;

    [Header("CoolDown Icons")]
    public Image[] coolDownIcons;

    [Header("Out Of Mana Icons")]
    public Image[] outOfManaIcons;

    [Header("Cooldown Times")]
    public float coolDownTime1 = 1f;
    public float coolDownTime2 = 1f;
    public float coolDownTime3 = 1f;
    public float AttackTime = 1f;
    public float NextAttackTime = 1f;
    public float ShieldTime = 1f;



    [Header("Mana Amounts")]
    public float skill1ManaAmount = 20f;
    public float skill2ManaAmount = 20f;
    public float skill3ManaAmount = 20f;


    [Header("Required Level")]
    public int skill1 = 2;
    public int skill2 = 3;
    public int skill3 = 4;

    private bool faded = false;

    public int[] fadeImages = new int[] { 0, 0, 0, 0, 0, 0 };
    [HideInInspector] public List<float> CoolDownTimeList = new List<float>();
    private List<float> ManaAmountList = new List<float>();

    private List<int> LevelList = new List<int>();


    private Animator anim;

    private bool CanAttack = true;

    private ClickToMove playerOnClick;

    private LevelManager levelManager;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerOnClick = GetComponent<ClickToMove>();
        manaBar = GameObject.Find("ManaFill").GetComponent<Image>();
        levelManager = FindObjectOfType<LevelManager>();

    }

    void Start()
    {
        AddList();
    }

    void AddList()
    {
        //AddCooldownList
        CoolDownTimeList.Add(coolDownTime1);
        CoolDownTimeList.Add(coolDownTime2);
        CoolDownTimeList.Add(coolDownTime3);
        CoolDownTimeList.Add(AttackTime);
        CoolDownTimeList.Add(NextAttackTime);
        CoolDownTimeList.Add(ShieldTime);

        //AddManaAmountList
        ManaAmountList.Add(skill1ManaAmount);
        ManaAmountList.Add(skill2ManaAmount);
        ManaAmountList.Add(skill3ManaAmount);
        //LevelList
        LevelList.Add(skill1);
        LevelList.Add(skill2);
        LevelList.Add(skill3);
    }

    void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            CanAttack = true;
        }
        else
        {
            CanAttack = false;
        }
        if (anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            TurnThePlayer();
        }
        if (totalMana < 100f)
        {
            totalMana += Time.deltaTime * manaRegenSpeed;
            manaBar.fillAmount = totalMana / 100f;
        }
        CheckLevel();
        CheckMana();
        CheckToFade();
        CheckInput();
    }

    void CheckInput()
    {
        if (anim.GetInteger("Attack") == 0)
        {
            playerOnClick.FinishedMovement = false;
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            {
                playerOnClick.FinishedMovement = true;
            }
        }


        //Skill Input
        if (Input.GetKeyDown(KeyCode.R) && totalMana >= skill1ManaAmount && levelManager.GetLevel >= skill1)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[0] != 1 && CanAttack)
            {
                totalMana -= skill1ManaAmount;
                fadeImages[0] = 1;
                anim.SetTrigger("StrongAttack");
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && totalMana >= skill2ManaAmount && levelManager.GetLevel >= skill2)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[1] != 1 && CanAttack)
            {
                totalMana -= skill2ManaAmount;
                fadeImages[1] = 1;
                anim.SetInteger("Attack", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && totalMana >= skill3ManaAmount && levelManager.GetLevel >= skill3)
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[2] != 1 && CanAttack)
            {
                totalMana -= skill3ManaAmount;
                fadeImages[2] = 1;
                anim.SetInteger("Attack", 3);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            playerOnClick.TargetPosition = transform.position;
            if (playerOnClick.FinishedMovement && fadeImages[4] != 1)
            {
                fadeImages[4] = 1;
                anim.SetBool("Block" , true);
            }
            else
            {
                anim.SetBool("Block" , false);
            }
        }
        else
        {
            anim.SetInteger("Attack", 0);
        }
    }

    private void CheckToFade()
    {
        for (int i = 0; i < coolDownIcons.Length; i++)
        {
            if (fadeImages[i] == 1)
            {
                if (FadeAndWait(coolDownIcons[i], CoolDownTimeList[i]))
                {
                    fadeImages[i] = 0;
                }
            }
        }
    }

    void CheckMana()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (levelManager.GetLevel >= LevelList[i])
            {
                if (totalMana < ManaAmountList[i])
                {
                    outOfManaIcons[i].gameObject.SetActive(true);
                }
                else
                {
                    outOfManaIcons[i].gameObject.SetActive(false);
                }
            }

        }

    }

    void CheckLevel()
    {
        for (int i = 0; i < outOfManaIcons.Length; i++)
        {
            if (levelManager.GetLevel < LevelList[i])
            {
                outOfManaIcons[i].gameObject.SetActive(true);
            }
        }
    }

    bool FadeAndWait(Image fadeImage, float fadeTime)
    {
        faded = false;
        if (fadeImage == null)
        {
            return faded;
        }

        if (!fadeImage.gameObject.activeInHierarchy)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.fillAmount = 1f;
        }

        fadeImage.fillAmount -= fadeTime * Time.deltaTime;

        if (fadeImage.fillAmount <= 0f)
        {
            fadeImage.gameObject.SetActive(false);
            faded = true;
        }
        return faded;
    }

    void TurnThePlayer()
    {
        Vector3 targetPos = Vector3.zero;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            targetPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPos - transform.position), playerOnClick.turnSpeed * Time.deltaTime);
    }
}