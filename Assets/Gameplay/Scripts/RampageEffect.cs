using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RampageEffect : MonoBehaviour
{
    public bool Raging;

    public PostProcessVolume volumeA;
    public PostProcessVolume volumeB;
    public PostProcessVolume volumeC;
    
    void Update()
    {
        if(PlayerController.instance.Died)
        {
            volumeA.weight = Mathf.Lerp(volumeA.weight, 0, 0.1f);
            volumeB.weight = Mathf.Lerp(volumeB.weight, 0, 0.1f);
            volumeC.weight = Mathf.Lerp(volumeC.weight, 1, 0.02f);
            return;
        }

        if(Raging)
        {
            volumeA.weight = Mathf.Lerp(volumeA.weight, 0, 0.05f);
            volumeB.weight = Mathf.Lerp(volumeB.weight, 1, 0.05f);
        }
        else
        {
            volumeA.weight = Mathf.Lerp(volumeA.weight, 1, 0.05f);
            volumeB.weight = Mathf.Lerp(volumeB.weight, 0, 0.05f);
        }
    }

    public void SetRaging(bool a)
    {
        Raging = a;
    }
}
