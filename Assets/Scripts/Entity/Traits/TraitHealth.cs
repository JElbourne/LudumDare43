using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TraitHealth : MonoBehaviour {

    public float safeAfterHurtTime = 0.5f;
    public TextMeshProUGUI hitPointsTextUI;

    float hurtTimeCounter = 0;
    float m_health = 0;

    bool isHurt = false;
    public SpriteRenderer playerSprite;

    private void Awake()
    {
        
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
                playerSprite.color = new Color(1, 1, 1, 1);
                isHurt = false;
            }

        }
        hitPointsTextUI.text = "Hit Points Left: " + m_health.ToString();
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
            playerSprite.color = new Color(1,0,0,0.5f);

            AudioManager.instance.Play("PlayerHurt");

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
