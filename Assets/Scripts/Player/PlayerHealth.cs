using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float currentHealth;
    public float maxHealth = 100f;
    private bool isShield;
    public bool Shielded { get { return isShield; } set { isShield = value; } }
    private Animator anim;
    private Image healthOrb;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        healthOrb = GameObject.Find("HealthFill").GetComponent<Image>();
    }
    public void TakeDamage(float amount)
    {
        if (!isShield)
        {
            currentHealth -= amount;

            UpdateHealth();

            if (currentHealth <= 0)
            {
                anim.SetBool("Death", true);
            }
        }
    }

    public void HealPlayer(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        healthOrb.fillAmount = currentHealth / maxHealth;
    }
}
