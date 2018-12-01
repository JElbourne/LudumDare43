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
        currentHealth = health;
        timeSinceLastDirection = 1f;
        if (platformEdgeDistance < transform.localScale.x) platformEdgeDistance = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastDirection += Time.deltaTime;
        Debug.Log(currentSleepingTime);
        if (currentSleepingTime > 0)
        {
            currentSleepingTime -= Time.deltaTime;
        } else
        {
            WakeUp();
        }

        CheckOnPlatform();
        if (onPlatform)
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
            rb.Sleep();
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
        // Particle Blood Effect
        Instantiate(bloodEffect, transform.position, Quaternion.identity);

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            IsSleeping();
        } else
        {
            IsDazed();
        }
    }

    void IsDazed()
    {
        Debug.Log("Enemy Is Dazed");
        currentSpeed = 0;
        currentSleepingTime = dazedTime;
    }

    void IsSleeping()
    {
        Debug.Log("Enemy Is Sleeping");
        currentSpeed = 0;
        currentSleepingTime = sleepingTime;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
    }

    void WakeUp()
    {
        Debug.Log("Enemy Is Waking");
        currentSpeed = speed;
        if (currentHealth <= 0) currentHealth = health;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
