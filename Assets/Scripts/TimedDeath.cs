using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeath : MonoBehaviour
{
    public float time = 1.25f;
    public Object Target;

    void Start()
    {
        if(!Target)
            Destroy(gameObject, time);
        else Destroy(Target, time);
    }
}
