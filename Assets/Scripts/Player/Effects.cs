using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [Header("SkillEffects")]
    public GameObject Attack1Hit;
    public GameObject TurnAttackHit;
    public GameObject powerUpSkill;
    

    [Header("Skill Transforms")]
    public Transform Attack1Transform;
    public Transform Attack2Transform;
    public Transform TurnAttackTransform;
    public Transform StrongTurnAttackTransform;
    

    void Attack1Cast()
    {
        Instantiate(Attack1Hit, Attack1Transform.position, Quaternion.identity);
    }
  
    void TurnAttackCast()
    {
        Instantiate(TurnAttackHit, StrongTurnAttackTransform.position, Quaternion.identity);
    }
    void PowerUpCast()
    {
        Vector3 pos = transform.position;
        GameObject ShieldClone = Instantiate(powerUpSkill, pos, Quaternion.identity);
        ShieldClone.transform.SetParent(transform);
    }
   
}
