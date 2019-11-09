using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [Header("Health")]
    public float maxHealth = 1000;
    public float currentHealth = 250;
    public float regenerationIntensity = 4;
    public float standingStillRegenTime = 2f;
    public float noDamageRegenTime = 2.5f;
    public Image HealthSlider;

    [Header("Movement")]
    public float movementSpeed = 100f;
    public float maxDepth = 10f;
    public float minDepth = -5f;
    public Rigidbody rb;

    [Header("Damage")]
    public float attackCooldown = 0.3f;
    public float currentDamage = 0f;

    [Header("Event hookups")]
    public UnityEvent onStandStillRegen = new UnityEvent();
    public UnityEvent onNoDamageRegen = new UnityEvent();
    public UnityEvent onTakeDamage = new UnityEvent();
    public UnityEvent onDeath = new UnityEvent();

    private Vector3 lastPos;
    private float standStillTime = 0f;
    private float sinceDamageTime = 0f;
    private float sinceAttackTime = 0f;
    private bool Died = false;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        //Get the difference between the max and current health and use that as a power to the value 2.
        //Case examples:
        //2 ^ ((1000 - 1000) * 0.01f) = 1
        //2 ^ ((1000 - 800) * 0.01f) = 4
        //2 ^ ((1000 - 500) * 0.01f) = 32
        //2 ^ ((1000 - 250) * 0.01f) = 181
        //2 ^ ((1000 - 150) * 0.01f) = 362
        //2 ^ ((1000 - 50) * 0.01f) = 724

        //Final results will be multiplied by 8

        currentDamage = Mathf.Pow(2, ((maxHealth - currentHealth) * 0.01f)) * 8;

        if (currentHealth <= 0 && !Died) Die();

        sinceDamageTime += Time.deltaTime;

        if (transform.position == lastPos)
            standStillTime += Time.deltaTime; 
        else standStillTime = 0f;

        if(Input.GetMouseButtonDown(0))
        {

        }

        HealthSlider.fillAmount = Mathf.Lerp(HealthSlider.fillAmount, currentHealth / maxHealth, 0.04f);

        rb.velocity = Vector3.zero;

        Vector3 forwardVector = transform.forward;
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        movementVector = Vector3.Normalize(movementVector);
        rb.AddForce(movementVector * Time.deltaTime * movementSpeed);

        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, minDepth, maxDepth));

        if (standStillTime >= standingStillRegenTime)
        {
            onStandStillRegen.Invoke();
            currentHealth = Mathf.Clamp(currentHealth + regenerationIntensity, 0, maxHealth);
        }

        if (sinceDamageTime >= noDamageRegenTime)
        {
            onNoDamageRegen.Invoke();
            currentHealth = Mathf.Clamp(currentHealth + regenerationIntensity, 0, maxHealth);
        }

        lastPos = transform.position;
    }

    public void InflictDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        sinceDamageTime = 0f;

        onTakeDamage.Invoke();
        //Do hit effect here
        //Update healthbar here
    }

    public void Die()
    {
        Died = true;
        onDeath.Invoke();
    }
}