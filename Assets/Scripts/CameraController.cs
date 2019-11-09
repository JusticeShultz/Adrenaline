using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 0.01f;
    public float yAxisOffset = 10f;
    public float DepthOffset = -4.54f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerController.instance.transform.position + new Vector3(0, yAxisOffset, DepthOffset), followSpeed);
    }
}
