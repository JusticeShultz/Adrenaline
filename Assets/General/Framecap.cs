
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Framecap : MonoBehaviour
{
    void Update()
    {
        Application.targetFrameRate = 60;
        QualitySettings.maxQueuedFrames = 60;
    }
}
