using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    ClickToMove clickToMove;
    PlayerAttack playerAttack;
    PlayerHealth playerHealth;
    EnemyAttack enemyAttack;

    int time = 15;

    private void Awake()
    {
        clickToMove = GetComponent<ClickToMove>();
        playerHealth = GetComponent<PlayerHealth>();
        playerAttack = GameObject.FindGameObjectWithTag("Sword").GetComponent<PlayerAttack>();
        enemyAttack = GameObject.FindGameObjectWithTag("EnemyAttack").GetComponent<EnemyAttack>();
    }
    private void Update()
    {
        if(time <= Time.time)
        {
            SpeedUp();
            AttackUp();
            PlayerDamage();
        }
    }
    void SpeedUp()
    {
        clickToMove.currentSpeed = 1f;
    }
    void AttackUp()
    {
        playerAttack.damage *= 2;
    }
    void PlayerDamage()
    {
        enemyAttack.damage /= 2;
    }
}
