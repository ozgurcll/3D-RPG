using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    public enum State
    {
        BattlePoze,
        Idle,
        PATROL,
        CHASE,
        ATTACK,
       // SHOOT,
        DEATH
    }
    private Transform playerTarget;
    private State bossState = State.Idle;
    public State state { get { return bossState; } }

    private float distanceToTarget;
    private EnemyHealth enemyHealth;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        enemyHealth = GetComponent<EnemyHealth>();
        bossState = State.Idle;
    }

    private void Update()
    {
        SetState();
    }

    void SetState()
    {
        distanceToTarget = Vector3.Distance(playerTarget.position, transform.position);
        int enemyCount = FindObjectsOfType<EnemyWayPoint>().Length;

        if (bossState == State.Idle)
        {
            if (enemyHealth.currentHealth < enemyHealth.maxHealth)
            {
                bossState = State.BattlePoze;
            }
            else if (distanceToTarget <= 4f)
            {
                bossState = State.BattlePoze;
            }
            else if (enemyCount <= 0)
            {
                bossState = State.BattlePoze;
            }
            else
            {
                bossState = State.Idle;
            }
        }
        else if (bossState != State.DEATH || bossState != State.Idle)
        {
            if (distanceToTarget > 4f && distanceToTarget <= 8f)
            {
                bossState = State.CHASE;
            }
            /*else if (distanceToTarget > 8f && distanceToTarget <= 12f)
            {
                bossState = State.SHOOT;
            }*/
            else if (distanceToTarget > 12f)
            {
                bossState = State.PATROL;
            }
            else if (distanceToTarget <= 4f)
            {
                bossState = State.ATTACK;
            }
            else
            {
                bossState = State.BattlePoze;
            }
        }
        if (enemyHealth.currentHealth <= 0f)
        {
            bossState = State.DEATH;
        }



    }
}
