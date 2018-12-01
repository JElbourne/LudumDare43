using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float health = 1;
    public float speed = 1;
    public float sleepingTime;
    public float dazedTime;
    public float platformEdgeDistance;
    public GameObject bloodEffect;
    public LayerMask platformEdge;
    public LayerMask platform;

    private float currentHealth;
    private float currentSpeed;
    private float currentSleepingTime;
    private float currentDazedTime;

    Rigidbody2D rb;
    bool onPlatform = false;
    float direction = 1;
    float timeSinceLastDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
        currentSpeed = speed;
        currentSleepingTime = 0;
        currentDazedTime = 0;
        currentHealth = health;
        timeSinceLastDirection = 1f;
        if (platformEdgeDistance < transform.localScale.x) platformEdgeDistance = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastDirection += Time.deltaTime;

        UpdateSleeping();
        UpdateDazed();

        if (!onPlatform)
        {
            CheckOnPlatform();
        } else
        {
            if(timeSinceLastDirection >= 1f) CheckPlatformEdge();
        }

        transform.Translate(Vector2.left * direction * currentSpeed * Time.deltaTime);
        
	}

    void CheckOnPlatform()
    {
        if (Physics2D.OverlapCircle(transform.position, transform.localScale.x * 0.5f, platform))
        {
            onPlatform = true;
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        } else
        {
            onPlatform = false;
        }
    }

    void CheckPlatformEdge()
    {
        if (Physics2D.OverlapCircle(transform.position, platformEdgeDistance, platformEdge))
        {
            direction *= -1;
            timeSinceLastDirection = 0f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, platformEdgeDistance);
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Enemy Taking Damage");
        currentDazedTime = dazedTime;

        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        currentHealth -= damage;
        if (currentHealth <= 0) IsSleeping();
    }

    void UpdateSleeping()
    {
        if (currentSleepingTime <= 0)
        {
            currentSpeed = speed;
            currentHealth = health;
        }
        else
        {
            Debug.Log("Enemy Is Sleeping");
            currentSpeed = 0;
            currentSleepingTime -= Time.deltaTime;
        }
    }

    void UpdateDazed()
    {
        if (currentDazedTime <= 0)
        {
            Debug.Log("Enemy is NOT Dazzed");
            currentSpeed = speed;
        }
        else
        {
            Debug.Log("Enemy is Dazzed");
            Debug.Log(currentDazedTime);
            currentSpeed = 0;
            currentDazedTime -= Time.deltaTime;
        }
    }

    void IsSleeping()
    {
        Debug.Log("Enemy Is Sleeping");
        currentSleepingTime = sleepingTime;

        Debug.Log("It Worked...the enemy is sleeping");
    }
}
