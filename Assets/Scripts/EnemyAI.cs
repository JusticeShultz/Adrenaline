using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
    public float mindamage = 25f;
    public float maxdamage = 100f;
    public float critChance = 5f;

    [Header("Movement")]
    public NavMeshAgent agent;

    [Header("Damage")]
    public CollisionDetector hitbox;
    public float attackSpeed = 0.4f;

    [Header("Fancy floaty numbers")]
    public GameObject enemyDamageNumberPrefab;
    public GameObject enemyCriticalDamagePrefab;

    public UnityEvent onDoNormalDamage = new UnityEvent();
    public UnityEvent onDoCriticalDamage = new UnityEvent();
    public UnityEvent onTakeDamage = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();

    private bool died = false;
    private float attackSpeedAhhhhGamejamNaming = 0f;
    private List<TextMesh> textMeshes = new List<TextMesh>();

    void Update()
    {
        if (died) return;

        if(Vector3.Distance(transform.position, PlayerController.instance.transform.position) < 3)
            attackSpeedAhhhhGamejamNaming += Time.deltaTime;

        //Do movement here
        agent.SetDestination(PlayerController.instance.transform.position);

        if(hitbox.objectList.Count > 0 && attackSpeedAhhhhGamejamNaming >= attackSpeed)
        {
            attackSpeedAhhhhGamejamNaming = 0f;
            DealDamage(Random.Range(mindamage, maxdamage));
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
            StartCoroutine(TextAnimation(text));
            textMeshes.Add(text);
        }
        else
        {
            onDoNormalDamage.Invoke();
            PlayerController.instance.InflictDamage(amount);

            TextMesh text = Instantiate(enemyDamageNumberPrefab, transform.position + transform.forward, Quaternion.identity).GetComponent<TextMesh>();
            text.text = Mathf.Ceil(amount).ToString();
            StartCoroutine(TextAnimation(text));
            textMeshes.Add(text);
        }
    }

    public void Die()
    {
        died = true;
        onDeath.Invoke();

        for (int i = 0; i < PlayerController.instance.damageHitbox.objectList.Count; i++)
        {
            if(PlayerController.instance.damageHitbox.objectList[i] == gameObject)
            {
                PlayerController.instance.damageHitbox.objectList.Remove(PlayerController.instance.damageHitbox.objectList[i]);
            }
        }
        
        Destroy(gameObject);
        //Destroy(gameObject, 1f);
    }

    IEnumerator TextAnimation(TextMesh textObject)
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);

            if (textObject) textObject.transform.position += new Vector3(0, 0.005f, 0);
            else yield break;
        }

        for (int i = 0; i < textMeshes.Count; i++)
            if(textMeshes[i]) Destroy(textMeshes[i].gameObject);
        
        if(textObject)
            Destroy(textObject.gameObject);
    }
}