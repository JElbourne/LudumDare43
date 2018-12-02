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
    public GameObject sleepEffect;
    public LayerMask platformEdge;
    public LayerMask platform;
    public LayerMask playerLayer;

    private float currentHealth;
    private float currentSpeed;
    private float currentSleepingTime;

    Rigidbody2D rb;
    GameObject currentSleepEffect;
    bool onPlatform = false;
    float direction = 1;
    float timeSinceLastDirection;

    Animator animator;
    ShakeCamera shakeCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        shakeCamera = Camera.main.GetComponent<ShakeCamera>();
        animator = GetComponentInChildren<Animator>();
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
        if (currentSleepingTime > 0)
        {
            currentSleepingTime -= Time.deltaTime;
        } else
        {
            WakeUp();
        }

        CheckOnPlatform();

        // Only check if the enemy is not sleeping
        if (currentSleepingTime <= 0) CheckHitPlayer();

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

    void CheckHitPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, transform.localScale.x * 0.51f, playerLayer);
        if (playerCollider)
        {
            TraitHealth ph = playerCollider.gameObject.GetComponent<TraitHealth>();
            if (ph) ph.TakeDamage(1.0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, platformEdgeDistance);
    }

    public void TakeDamage(float damage)
    {
        // Particle Blood Effect
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        shakeCamera.CameraShake();

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
        currentSpeed = 0;
        currentSleepingTime = dazedTime;
    }

    void IsSleeping()
    {
        currentSpeed = 0;
        currentSleepingTime = sleepingTime;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        animator.SetBool("isSleeping", true);
        currentSleepEffect = Instantiate(sleepEffect, transform.position, Quaternion.identity, transform);
        GameController.Instance.AddToScore(1);
    }

    void WakeUp()
    {
        currentSpeed = speed;
        if (currentHealth <= 0) currentHealth = health;
        transform.localScale = new Vector3(1f, 1f, 1f);
        animator.SetBool("isSleeping", false);
        Destroy(currentSleepEffect);
    }

}
