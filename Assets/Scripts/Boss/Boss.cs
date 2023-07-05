using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    private Transform playerTarget;
    private BossState bossStateChecker;
    private NavMeshAgent agent;
    private Animator anim;

    private bool finishedAttacking = true;

    public float turnSpeed;
    public float attackRate;
    private float currentAttackTime;
    private SphereCollider targetCollider;
    public static bool bossDeath = false;

    public Transform gate1;
    public Transform gate2;

    private List<GameObject> allWaypointsList = new List<GameObject>();

    //[SerializeField] private GameObject fireBall;
    //[SerializeField] private Transform firePosition;
    private void Awake()
    {
        bossDeath = false;
        targetCollider = GetComponentInChildren<SphereCollider>();
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        bossStateChecker = GetComponent<BossState>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        allWaypointsList.AddRange(GameObject.FindGameObjectsWithTag("Waypoints"));
    }
    private void Update()
    {
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            /* if (!AudioManager.instance.sfx[10].isPlaying)
             {
                 AudioManager.instance.PlaySfx(10);
             }*/
        }
        if (finishedAttacking)
        {
            GetControl();
        }
        else
        {
            anim.SetInteger("Attack", 0);
            if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("BattlePoze"))
            {
                finishedAttacking = true;
            }
        }
    }

    private void GetControl()
    {
        if (bossStateChecker.state == BossState.State.DEATH)
        {
            agent.isStopped = true;
            anim.SetBool("Death", true);
            targetCollider.enabled = false;
            bossDeath = true;
            // AudioManager.instance.PlaySfx(7);
        }
        else
        {
            if (bossStateChecker.state == BossState.State.CHASE)
            {
                agent.isStopped = false;
                anim.SetBool("Run", true);
                anim.SetBool("SeeTarget", true);
                anim.SetBool("Walk", false);
                agent.speed = 5f;
                agent.SetDestination(playerTarget.position);
            }
            else if (bossStateChecker.state == BossState.State.PATROL)
            {
                agent.isStopped = false;
                anim.SetBool("Run", false);
                anim.SetBool("SeeTarget", true);
                anim.SetBool("Walk", true);
                gate1.rotation = Quaternion.Euler(0f, 103f, 0f);
                gate2.rotation = Quaternion.Euler(0f, 90f, 0f);
                if (agent.remainingDistance < 4f || !agent.hasPath)
                {
                    agent.speed = 2f;
                    PickRandomLocation();
                }
            }
            else if (bossStateChecker.state == BossState.State.ATTACK)
            {
                anim.SetBool("Run", false);
                anim.SetBool("SeeTarget", true);
                anim.SetBool("Walk", false);
                LookPlayer();
                if (currentAttackTime >= attackRate)
                {
                    int index = UnityEngine.Random.Range(1, 5);
                    anim.SetInteger("Attack", index);
                    // AudioManager.instance.PlaySfx(9);
                    currentAttackTime = 0f;
                    finishedAttacking = false;
                }
                else
                {
                    currentAttackTime += Time.deltaTime;
                    anim.SetInteger("Attack", 0);
                }
            }
            else
            {
                anim.SetBool("Run", false);
                anim.SetBool("SeeTarget", false);
                anim.SetBool("Walk", false);
                agent.isStopped = true;
                int index = UnityEngine.Random.Range(1, 4);
                anim.SetInteger("Idle", index);
            }
        }
    }

    void PickRandomLocation()
    {
        GameObject pos = GetRandomPoint();
        agent.SetDestination(pos.transform.position);
    }
    private GameObject GetRandomPoint()
    {
        int index = UnityEngine.Random.Range(0, allWaypointsList.Count);
        return allWaypointsList[index];
    }

    void LookPlayer()
    {
        Vector3 targetPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), turnSpeed * Time.deltaTime);
    }
}
