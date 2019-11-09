using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
    public float mindamage = 25f;
    public float maxdamage = 100f;
    public float critChance = 5f;

    [Header("Movement")]
    public float movementSpeed;
    public float maxDepth;
    public float mMinDepth;

    [Header("Fancy floaty numbers")]
    public GameObject enemyDamageNumberPrefab;
    public GameObject enemyCriticalDamagePrefab;

    public UnityEvent onDoNormalDamage = new UnityEvent();
    public UnityEvent onDoCriticalDamage = new UnityEvent();
    public UnityEvent onTakeDamage = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();

    private bool died = false;

    void Update()
    {
        //Do movement here

        if(PlayerController.instance.transform.position.x < transform.position.x)
        {
            // Do sum movin' : )
        }
    }

    public void InflictDamage(float amount)
    {
        health -= amount;
        onTakeDamage.Invoke();

        if(health <= 0 && !died)
        {
            Die();
        }
    }

    public void DealDamage(float amount)
    {
        int roll = Random.Range(0, 100);

        if (roll < critChance)
        {
            onDoCriticalDamage.Invoke();
            PlayerController.instance.InflictDamage(amount * 3.5f);

            TextMesh text = Instantiate(enemyCriticalDamagePrefab, transform.position + transform.forward, Quaternion.identity).GetComponent<TextMesh>();
            text.text = Mathf.Ceil(amount * 3.5f).ToString();
        }
        else
        {
            onDoNormalDamage.Invoke();
            PlayerController.instance.InflictDamage(amount);

            TextMesh text = Instantiate(enemyDamageNumberPrefab, transform.position + transform.forward, Quaternion.identity).GetComponent<TextMesh>();
            text.text = Mathf.Ceil(amount * 3.5f).ToString();
        }
    }

    public void Die()
    {
        died = true;
        onDeath.Invoke();
    }

    IEnumerator TextAnimation(TextMesh textObject)
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            textObject.transform.position += new Vector3(0, 0.005f, 0);
        }

        Destroy(textObject.gameObject);
    }
}
