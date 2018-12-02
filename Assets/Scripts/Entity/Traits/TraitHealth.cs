using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitHealth : MonoBehaviour {

    public float safeAfterHurtTime = 0.5f;

    float hurtTimeCounter = 0;
    float m_health = 0;

    bool isHurt = false;
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (hurtTimeCounter > 0)
        {
            hurtTimeCounter -= Time.deltaTime;
        } else
        {
            if (isHurt)
            {
                sr.color = new Color(1, 1, 1, 1);
                isHurt = false;
            }

        }
    }

    public int GetHealthAsInt()
    {
        return Mathf.FloorToInt(m_health);
    }

    public void SetHealth(float health)
    {
        m_health = health;
    }
    public void TakeDamage(float damage)
    {
        if (!isHurt)
        {
            isHurt = true;
            hurtTimeCounter = safeAfterHurtTime;
            sr.color = new Color(1, 1, 1, 0.8f);

            m_health -= damage;
            Debug.Log("PLAYER GOT HURT");
            //TODO
            //Add Particle effect
            //Add push back

            if (m_health <= 0)
            {
                TraitDeath td = GetComponent<TraitDeath>();

                if (td)
                {
                    Debug.Log("Found GameObjects Death");
                    td.IsDead();
                }
            }
        }

    }

}
