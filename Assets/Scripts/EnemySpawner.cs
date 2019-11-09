using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRate = 4.0f;
    private float _spawnRate = 4.0f;

    public List<GameObject> enemies = new List<GameObject>();

    void Update()
    {
        _spawnRate += Time.deltaTime;

        if (_spawnRate >= spawnRate)
        {
            _spawnRate = 0f;
            Instantiate(enemies[Random.Range(0, enemies.Count)], RandomNavmeshLocation(80), Quaternion.identity);
        }
    }

    public Vector3 RandomNavmeshLocation(float radius)
    {
        int a = 0;
        Vector3 finalPosition = Vector3.zero;

        while (a == 0 || Vector3.Distance(finalPosition, PlayerController.instance.transform.position) <= 15f)
        {
            a++;
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += transform.position;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
            {
                finalPosition = hit.position;
            }
        }

        return finalPosition;
    }
}
