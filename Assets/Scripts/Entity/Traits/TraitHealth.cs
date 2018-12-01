using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitHealth : MonoBehaviour {

    float m_health = 0;

    public void SetHealth(float health)
    {
        m_health = health;
    }
    public void TakeDamage(float damage)
    {
        m_health -= damage;

        //TODO
        //Add Particle effect
        //Add push back

        if (m_health <= 0)
        {
            IsDead();
            GameController.Instance.GameOver();
        }
    }

    public void IsDead()
    {
        //TODO
        //Play Sound
        //Play Animation
        //Play Particle Effects
    }
}
