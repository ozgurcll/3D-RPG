using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    GameObject player;
    private float healthAmount = 5f;
    private int time = 5;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (time <= Time.time)
        {
            player.GetComponent<PlayerHealth>().HealPlayer(20f * Time.deltaTime);
        }

    }
}
