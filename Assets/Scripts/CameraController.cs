using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float followSpeed = 0.01f;
    public float yAxisOffset = 10f;
    public float DepthOffset = -4.54f;
    public float ShakeIntensity = 1f;
    public float ShakeTimeLeft = 0f;

    private Vector3 Shake;

    private void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (ShakeTimeLeft > 0)
        {
            ShakeTimeLeft -= Time.deltaTime;
            Shake = new Vector3(Random.Range(-ShakeIntensity, ShakeIntensity), Random.Range(-ShakeIntensity, ShakeIntensity), 0);
        }
        else Shake = Vector3.zero;

        transform.position = Vector3.Lerp(transform.position, PlayerController.instance.transform.position + new Vector3(0, yAxisOffset, DepthOffset) + Shake, followSpeed);
    }

    public void ScreenShake(float Time, float Intensity)
    {
        instance.ShakeIntensity = Intensity;
        ShakeTimeLeft = Time;
    }
}
