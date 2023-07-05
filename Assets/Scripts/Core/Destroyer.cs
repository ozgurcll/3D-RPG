using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public float time = 1f;
    void Update()
    {
        Destroy(this.gameObject , time);
    }
}
