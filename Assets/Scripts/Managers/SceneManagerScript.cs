using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    BossState bossState;
    public GameObject bossHealthBar;
    public GameObject levelBar;
    private void Awake()
    {
        bossState = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossState>();
    }
    private void Start()
    {
        // AudioManager.instance.PlayGameMusic();
    }

    void Update()
    {
        if (bossState.state == BossState.State.Idle || bossState.state == BossState.State.DEATH)
        {
            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(false);
                levelBar.SetActive(true);
            }
        }
        else
        {
            if (bossHealthBar != null)
            {
                bossHealthBar.SetActive(true);
                levelBar.SetActive(false);
            }
        }
       /* if (Boss.bossDeath == true)
        {
            Invoke("RestartScene", 5f);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().currentHealth <= 0)
        {
            Invoke("RestartScene", 5f);
        }*/
    }

    void RestartScene()
    {
        SceneManager.LoadScene(0);
    }
}
