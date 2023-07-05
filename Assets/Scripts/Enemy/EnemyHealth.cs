using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnemyHealth : MonoBehaviour
{
    Animator anim;
    public float currentHealth;
    [SerializeField] private Image EnemyHealthBar;
    [SerializeField] private SphereCollider targetCollider;

    public float maxHealth = 100f;
   // public Canvas canvas;

    public int expAmount = 10;
    public static event Action<int> onDeath;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        targetCollider = GetComponentInChildren<SphereCollider>();
        currentHealth = maxHealth;
    }
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        EnemyHealthBar.fillAmount = currentHealth / maxHealth;
        if (currentHealth > 0)
        {
            /* if (this.gameObject.tag == "Boss")
             {
                 // AudioManager.instance.PlaySfx(6);
             }
             else if (this.gameObject.tag == "Enemy")
             {
                 //AudioManager.instance.PlaySfx(3);
             }*/
            anim.SetTrigger("Hurt");
        }
        if (currentHealth <= 0)
        {
            Canvas canvas = EnemyHealthBar.gameObject.GetComponentInParent<Canvas>();

            onDeath(expAmount);
            if (targetCollider.gameObject.activeInHierarchy)
            {
                targetCollider.gameObject.SetActive(false);

            }
            if (canvas.gameObject.activeInHierarchy)
            {
                canvas.gameObject.SetActive(false);

            }
        }
    }
}
