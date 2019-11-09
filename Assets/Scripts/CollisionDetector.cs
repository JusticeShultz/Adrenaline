using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// WARNING: Late night code
/// Pools together all collided with objects. For best use, use physics layers.
/// </summary>
public class CollisionDetector : MonoBehaviour
{
    public List<GameObject> objectList = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        objectList.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if(objectList[i] == other.gameObject)
            {
                objectList.Remove(objectList[i]);
            }
        }
    }
}
