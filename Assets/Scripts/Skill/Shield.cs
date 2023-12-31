using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    void OnEnable()
    {
        playerHealth.Shielded = true;
    }

    void OnDisable()
    {
        playerHealth.Shielded = false;
    }
}
