using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAudio : MonoBehaviour
{
    public AudioSource source;
    public Slider slider;

    private void Start()
    {
        UpdateVolume();
    }

    public void UpdateVolume()
    {
        source.volume = slider.value;
    }
}
