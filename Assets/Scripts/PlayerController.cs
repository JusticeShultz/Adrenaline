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
    public GameObject playerDamageNumberPrefab;

    [Header("Movement")]
    public float movementSpeed = 100f;
    //public float laneOneMaxDepth = 10f;
    //public float laneOneMinDepth = -5f;
    //public float laneTwoMaxDepth = 10f;
    //public float laneTwoMinDepth = -5f;
    //public float laneThreeMaxDepth = 10f;
    //public float laneThreeMinDepth = -5f;
    //public float CurrentLane = 1;
    bool InLane = true;
    public Rigidbody rb;

    [Header("Damage")]
    public float attackCooldown = 0.3f;
    public float currentDamage = 0f;
    public CollisionDetector damageHitbox;

    [Header("Event hookups")]
    public UnityEvent onStandStillRegen = new UnityEvent();
    public UnityEvent onNoDamageRegen = new UnityEvent();
    public UnityEvent onTakeDamage = new UnityEvent();
    public UnityEvent onAttack = new UnityEvent();
    public UnityEvent onHitEnemies = new UnityEvent();
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

        sinceAttackTime += Time.deltaTime;

        if(Input.GetMouseButtonDown(0))
        {
            if (sinceAttackTime >= attackCooldown)
            {
                sinceAttackTime = 0f;
                onAttack.Invoke();

                if (damageHitbox.objectList.Count > 0)
                {
                    for (int i = 0; i < damageHitbox.objectList.Count; i++)
                    {
                        if (!damageHitbox.objectList[i])
                        {
                            damageHitbox.objectList.Remove(damageHitbox.objectList[i]);
                        }
                    }

                    for (int i = 0; i < damageHitbox.objectList.Count; i++)
                    {
                        if (damageHitbox.objectList[i])
                        {
                            EnemyAI ai = damageHitbox.objectList[i].GetComponent<EnemyAI>();

                            if (ai)
                            {
                                onHitEnemies.Invoke();
                                ai.InflictDamage(currentDamage);

                                TextMesh text = Instantiate(playerDamageNumberPrefab, transform.position - (transform.position - ai.transform.position), Quaternion.Euler(44.52f, 0, 0)).GetComponent<TextMesh>();

                                text.text = Mathf.Ceil(currentDamage).ToString();
                                StartCoroutine(TextAnimation(text));
                            }
                        }
                    }
                }
            }
        }

        HealthSlider.fillAmount = Mathf.Lerp(HealthSlider.fillAmount, currentHealth / maxHealth, 0.04f);

        rb.velocity = Vector3.zero;

        Vector3 forwardVector = transform.forward;
        Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if(movementVector.magnitude >= 0.15f)
        {
            transform.LookAt(transform.position - movementVector, transform.up);
        }

        movementVector = Vector3.Normalize(movementVector);
        
        rb.AddForce(movementVector * Time.deltaTime * movementSpeed * (InLane == true ? 1 : 2));
        //if(CurrentLane == 1)
        //    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, laneOneMinDepth, laneOneMaxDepth));
        //else if (CurrentLane == 2)
        //    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, laneTwoMinDepth, laneTwoMaxDepth));
        //else if (CurrentLane == 3)
        //    transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Clamp(transform.position.z, laneThreeMinDepth, laneThreeMaxDepth));

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

    IEnumerator TextAnimation(TextMesh textObject)
    {
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.01f);
            textObject.transform.position += new Vector3(0, 0.01f, 0);
        }

        Destroy(textObject.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LaneSwitch")
            InLane = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "LaneSwitch")
            InLane = true;
    }
}