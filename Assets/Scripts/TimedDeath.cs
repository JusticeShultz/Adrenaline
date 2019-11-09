using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public float time = 1.25f;

    void Start()
    {
        Destroy(gameObject, time);
    }
}
