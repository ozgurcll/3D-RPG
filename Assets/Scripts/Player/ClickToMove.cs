using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickToMove : MonoBehaviour
{
    UsingSkill usingSkill;
    int clickcount = 0;
    public float maxSpeed = 5f;
    public float turnSpeed = 15f;
    public float attackRange = 2f;

    public Animator anim;
    public CharacterController controller;
    private CollisionFlags collisionFlags = CollisionFlags.None;

    private Vector3 playerMove = Vector3.zero;
    private Vector3 targetMovePoint = Vector3.zero;
    private Vector3 targetAttackPoint = Vector3.zero;
    public float currentSpeed;
    private float playerToPointDistance;
    private float gravity = 9.8f;
    private float height;

    private bool canMove;
    private bool canAttackMove;
    private bool finishedMovement = true;
    private Vector3 newMovePoint;
    private Vector3 newAttackPoint;

    private GameObject enemy;
    #region propertys
    public bool FinishedMovement
    {
        get
        {
            return finishedMovement;
        }
        set
        {
            finishedMovement = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return canMove;
        }
        set
        {
            canMove = value;
        }
    }

    public Vector3 TargetPosition
    {
        get
        {
            return targetMovePoint;
        }
        set
        {
            targetMovePoint = value;
        }
    }
    #endregion

    private void Awake()
    {
        anim.GetComponent<Animator>();
        controller.GetComponent<CharacterController>();
        currentSpeed = maxSpeed;
        usingSkill = GetComponent<UsingSkill>();
    }

    void Update()
    {
        CalculateHeight();
        CheckIfFinishedMovement();
        AttackMove();
    }

    bool IsGrounded()
    {
        return collisionFlags == CollisionFlags.CollidedBelow ? true : false;
    }

    void AttackMove()
    {
        if (canAttackMove)
        {
            targetAttackPoint = enemy.gameObject.transform.position;
            newAttackPoint = new Vector3(targetAttackPoint.x, transform.position.y, targetAttackPoint.z);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), turnSpeed * 2 * Time.deltaTime);
        }
        if (!anim.IsInTransition(0) && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), turnSpeed * 2 * Time.deltaTime);
        }
    }

    void CalculateHeight()
    {
        if (IsGrounded())
        {
            height = 0f;
        }
        else
        {
            height -= gravity * Time.deltaTime;
        }
    }

    void CheckIfFinishedMovement()
    {
        if (!finishedMovement)
        {
            if (!anim.IsInTransition(0) && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
            {
                finishedMovement = true;
            }
        }
        else
        {
            MovePlayer();
            playerMove.y = height * Time.deltaTime;
            collisionFlags = controller.Move(playerMove);
        }
    }

    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                playerToPointDistance = Vector3.Distance(transform.position, hit.point);
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    if (playerToPointDistance >= 1f)
                    {
                        canMove = true;
                        canAttackMove = false;
                        targetMovePoint = hit.point;
                    }
                }
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Target"))
                {
                    enemy = hit.collider.gameObject.GetComponentInParent<EnemyHealth>().gameObject;
                    canMove = true;
                    canAttackMove = true;
                }
            }
        }

        if (canMove == true)
        {
            WSoftAnimate();
            if (!canAttackMove)
            {
                newMovePoint = new Vector3(targetMovePoint.x, transform.position.y, targetMovePoint.z);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newMovePoint - transform.position), turnSpeed * Time.deltaTime);
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(newAttackPoint - transform.position), turnSpeed * Time.deltaTime);
            }



            playerMove = transform.forward * currentSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, newMovePoint) <= 0.6f && !canAttackMove)
            {
                canMove = false;
                canAttackMove = false;
            }
            else if (canAttackMove)
            {
                if (Vector3.Distance(transform.position, newAttackPoint) <= attackRange)
                {
                    clickcount++;
                    print(clickcount);
                    playerMove.Set(0f, 0f, 0f);
                    ISoftAnimate();
                    targetAttackPoint = Vector3.zero;
                    anim.SetTrigger("Attack1");
                    usingSkill.fadeImages[3] = 1;
                    canAttackMove = false;
                    canMove = false;
                }
            }
        }
        else
        {
            playerMove.Set(0f, 0f, 0f);
            ISoftAnimate();
        }
    }

    void WSoftAnimate()
    {
        float speed = anim.GetFloat("Speed");
        float targetSpeed = 1f;
        float smoothTime = 0.02f;
        speed = Mathf.Lerp(speed, targetSpeed, smoothTime);
        anim.SetFloat("Speed", speed);
    }

    void ISoftAnimate()
    {
        float speed = anim.GetFloat("Speed");
        float targetSpeed = 0f;
        float smoothTime = 0.01f;
        speed = Mathf.Lerp(speed, targetSpeed, smoothTime);
        anim.SetFloat("Speed", speed);
    }


    public void Attack2()
    {
        if (clickcount >= 2)
        {
            print("Attack2 çalışıyr");
            anim.SetTrigger("Attack2");
            usingSkill.fadeImages[5] = 1;

        }
    }

    public void Attack3()
    {
        if (clickcount >= 2)
        {
            print("Attack3 çalışıyor");
            anim.SetTrigger("Attack3");
        }
    }




}
